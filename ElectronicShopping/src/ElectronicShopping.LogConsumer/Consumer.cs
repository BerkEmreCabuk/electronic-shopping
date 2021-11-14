using ElectronicShopping.LogConsumer.ElasticContexts;
using ElectronicShopping.LogConsumer.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopping.LogConsumer
{
    public class Consumer
    {
        private readonly RabbitMqConfigModel _rabbitMqConfig;
        private readonly IElasticContext _elasticContext;

        public Consumer(IOptions<RabbitMqConfigModel> rabbitMqConfig, IElasticContext elasticContext)
        {
            _rabbitMqConfig = rabbitMqConfig?.Value ?? throw new ArgumentNullException(nameof(rabbitMqConfig));
            _elasticContext = elasticContext;
        }

        public async Task Run(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfig.Hostname,
                UserName = _rabbitMqConfig.Username,
                Password = _rabbitMqConfig.Password,
                DispatchConsumersAsync = true
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("LoggerQueue", ExchangeType.Fanout, true, false);
                channel.QueueDeclare(queue: "LoggerQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind("LoggerQueue", "LoggerQueue", "");
                channel.BasicQos(0, 1, false);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var data = Encoding.UTF8.GetString(ea.Body.Span);
                    var log = JsonConvert.DeserializeObject(data);
                    try
                    {
                        var response = await _elasticContext.IndexCustomAsync($"logstash", log);
                        if (response.IsValid)
                            channel.BasicAck(ea.DeliveryTag, false);
                        else
                            channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                    catch (Exception)
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                };
                channel.BasicConsume(queue: "LoggerQueue",
                                     autoAck: false,
                                     consumer: consumer);
                Console.WriteLine(" press X for shotdown.");
                Console.ReadLine();

            }
        }
    }
}
