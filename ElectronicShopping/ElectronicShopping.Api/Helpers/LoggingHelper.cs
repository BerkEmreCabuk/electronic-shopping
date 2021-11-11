using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using Serilog.Core;
using Microsoft.Extensions.Configuration;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Constants;

namespace ElectronicShopping.Api.Helpers
{
    public static class LoggingHelper
    {
        public static Logger CustomLoggerConfiguration(IConfiguration configuration)
        {
            RabbitMqConfigModel rabbitMqConfigModel = new ();
            configuration.Bind(CommonKeyConstant.RABBITMQ_CONFIG_KEY, rabbitMqConfigModel);

            return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
            {
                clientConfiguration.Username = rabbitMqConfigModel.Username;
                clientConfiguration.Password = rabbitMqConfigModel.Password;
                clientConfiguration.Exchange = rabbitMqConfigModel.Exchange;
                clientConfiguration.ExchangeType = rabbitMqConfigModel.ExchangeType;
                clientConfiguration.DeliveryMode = RabbitMQDeliveryMode.Durable;
                clientConfiguration.Port = rabbitMqConfigModel.Port;
                clientConfiguration.VHost = "/";
                clientConfiguration.Hostnames.Add("localhost");
                sinkConfiguration.RestrictedToMinimumLevel = LogEventLevel.Information;
                sinkConfiguration.TextFormatter = new JsonFormatter();
            })
            .CreateLogger();
        }
    }
}
