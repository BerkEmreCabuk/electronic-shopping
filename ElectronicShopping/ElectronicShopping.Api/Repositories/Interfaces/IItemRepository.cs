using ElectronicShopping.Api.Repositories.Entities;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface IItemRepository : IGenericRepository<ItemEntity>
    {
        Task<decimal> GetItemPriceAsync(long itemId);
    }
}
