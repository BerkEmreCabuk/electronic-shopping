using ElectronicShopping.Api.Repositories.Entities;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface ICartDetailRepository : IGenericRepository<CartDetailEntity>
    {
        Task CreateAsync(CartDetailEntity cartDetailEntity);
        Task<CartDetailEntity> GetByCartIdAndItemId(long cartId, long itemId);
    }
}
