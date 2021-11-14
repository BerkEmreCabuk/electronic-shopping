using ElectronicShopping.Api.Repositories.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartEntity>
    {
        Task<CartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default);
        Task<CartEntity> GetByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default);
        Task<CartEntity> GetWithAllAsync(long userId, CancellationToken ct = default);
    }
}
