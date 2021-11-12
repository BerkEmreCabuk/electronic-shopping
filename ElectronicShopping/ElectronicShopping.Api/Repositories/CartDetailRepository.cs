using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class CartDetailRepository : GenericRepository<CartDetailEntity>, ICartDetailRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public CartDetailRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync()
        { 
        }
    }
}
