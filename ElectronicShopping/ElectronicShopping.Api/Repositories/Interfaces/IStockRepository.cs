using ElectronicShopping.Api.Repositories.Entities;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface IStockRepository : IGenericRepository<StockEntity>
    {
        Task<StockEntity> GetByItemId(long itemId, bool hasTracking = false);
    }
}
