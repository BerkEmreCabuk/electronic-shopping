using ElectronicShopping.Api.Repositories.Entities;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartEntity>
    {
        Task<CartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true);
        Task<CartEntity> GetByUserIdAsync(long userId, bool hasTracking = true);
    }
}
