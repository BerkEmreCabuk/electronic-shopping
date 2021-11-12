using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Infrastructure.Database;
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
        public async Task CreateAsync(CartDetailEntity cartDetailEntity)
        {
            var currentCartDetail = await GetByCartIdAndItemId(cartDetailEntity.CartId, cartDetailEntity.ItemId);
            if (currentCartDetail != null)
            {
                currentCartDetail.Quantity += cartDetailEntity.Quantity;
            }
            else
            {
                if (cartDetailEntity.Cart != null)
                    cartDetailEntity.Cart.Add();
                await AddAsync(cartDetailEntity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CartDetailEntity> GetByCartIdAndItemId(long cartId, long itemId)
        {
            return await GetAsync(
                x =>
                x.CartId == cartId &&
                x.ItemId == itemId &&
                x.Status == RecordStatuses.ACTIVE,
                true);
        }
    }
}
