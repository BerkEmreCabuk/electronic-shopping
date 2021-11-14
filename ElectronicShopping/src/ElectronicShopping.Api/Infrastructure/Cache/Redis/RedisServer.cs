using ElectronicShopping.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace ElectronicShopping.Api.Infrastructure.Cache.Redis
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string _configurationString;
        private int _currentDatabaseId = 0;

        public RedisServer(IOptions<CacheConfigModel> cacheConfigModel)
        {
            if (cacheConfigModel.Value == null)
                throw new ArgumentNullException(nameof(CacheConfigModel));

            _configurationString = $"{cacheConfigModel.Value.Host}:{cacheConfigModel.Value.Port}";
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public IDatabase Database => _database;

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(_configurationString).FlushDatabase(_currentDatabaseId);
        }
    }
}
