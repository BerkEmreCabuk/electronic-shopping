using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class CartRepository : GenericRepository<CartEntity>, ICartRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public CartRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartEntity> CreateAsync(long userId)
        {
            CartEntity cartEntity = new()
            {
                UserId = userId,
                Amount = 0
            };
            return await this.AddAsync(cartEntity);
        }
    }
}
