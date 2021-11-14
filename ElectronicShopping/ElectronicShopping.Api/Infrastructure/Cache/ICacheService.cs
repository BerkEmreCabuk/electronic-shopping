using System;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Infrastructure.Cache
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);
        Task Add(string key, object data, TimeSpan? expireTime = null);
        Task Remove(string key);
        Task<bool> KeyExists(string key);
        void Clear();
        Task<bool> Any(string key);
    }
}
