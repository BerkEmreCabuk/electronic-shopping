using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class StockRepository : GenericRepository<StockEntity>, IStockRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public StockRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StockEntity> GetByItemId(long itemId, bool hasTracking = false)
        {
            return await this.GetAsync(
                x =>
                x.ItemId == itemId &&
                x.Status == RecordStatuses.ACTIVE, hasTracking);
        }
    }
}
