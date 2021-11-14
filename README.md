# Electronic Shopping

## Ön Koşullar

* .NET 5 SDK
* Docker
* Postman

## Kullanım

DockerCompose dizini içerisinde ``docker-compose up`` komutu ile projede kullanılan RabbitMq, ElasticSearch, Kibana, Redis ortamını kurabilirsiniz.

src\AutoMarket.Api dizini içerisinde ``dotnet run`` komutu ile projeyi çalıştırabilirsiniz.

[localhost:5000](http://localhost:5000) portundan ayağa kalkacaktır. Postman Collection'ı veya [Swagger](http://localhost:5000/swagger/index.html) ile test edebilirsiniz


``dotnet test`` komutu ile unit testleri çalıştırabilirsiniz.