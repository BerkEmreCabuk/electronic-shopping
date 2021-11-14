using ElectronicShopping.Api.Repositories.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface ICartDetailRepository : IGenericRepository<CartDetailEntity>
    {
        Task CreateAsync(CartDetailEntity cartDetailEntity, CancellationToken ct = default);
        Task<CartDetailEntity> GetByCartIdAndItemIdAsync(long cartId, long itemId, CancellationToken ct = default);
    }
}
