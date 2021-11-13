using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public ItemRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<decimal> GetItemPriceAsync(long itemId)
        {
            return await Query()
                .Where(
                    x =>
                    x.Id == itemId &&
                    x.Status == RecordStatuses.ACTIVE)
                .Select(x => x.Price)
                .FirstOrDefaultAsync();
        }
    }
}
