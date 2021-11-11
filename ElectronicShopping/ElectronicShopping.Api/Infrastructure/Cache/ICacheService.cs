using System.Threading.Tasks;

namespace ElectronicShopping.Api.Infrastructure.Cache
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);
        Task Add(string key, object data);
        Task Remove(string key);
        void Clear();
        Task<bool> Any(string key);
    }
}
