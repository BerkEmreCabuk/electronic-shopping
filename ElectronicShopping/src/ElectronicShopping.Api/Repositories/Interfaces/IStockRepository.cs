using ElectronicShopping.Api.Repositories.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface IStockRepository : IGenericRepository<StockEntity>
    {
        Task<StockEntity> GetByItemId(long itemId, bool hasTracking = true, CancellationToken ct = default);
    }
}
