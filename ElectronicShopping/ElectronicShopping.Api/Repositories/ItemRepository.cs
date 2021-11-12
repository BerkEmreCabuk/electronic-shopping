using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;

namespace ElectronicShopping.Api.Repositories
{
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public ItemRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
