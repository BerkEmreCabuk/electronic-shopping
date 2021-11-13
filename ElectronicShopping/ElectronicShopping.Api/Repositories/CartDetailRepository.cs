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
        private readonly IItemRepository _itemRepository;

        public CartDetailRepository(
            ElectronicShoppingDbContext dbContext,
            IItemRepository itemRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            _itemRepository = itemRepository;
        }
        public async Task CreateAsync(CartDetailEntity cartDetailEntity)
        {
            var currentCartDetail = await GetByCartIdAndItemId(cartDetailEntity.CartId, cartDetailEntity.ItemId);
            if (currentCartDetail != null)
            {
                currentCartDetail.AddQuantity(cartDetailEntity.Quantity);
            }
            else
            {
                if (cartDetailEntity.CartId == 0)
                    cartDetailEntity.Cart.Add();

                var price = cartDetailEntity.Item == null
                    ? await _itemRepository.GetItemPriceAsync(cartDetailEntity.ItemId)
                    : cartDetailEntity.Item.Price;

                cartDetailEntity.Cart.AddAmount(cartDetailEntity.Quantity * price);
                await AddAsync(cartDetailEntity);
            }
        }

        public async Task UpdateAsync(CartDetailEntity cartDetailEntity)
        {
            var currentCartDetail = await GetByCartIdAndItemId(cartDetailEntity.CartId, cartDetailEntity.ItemId);
            if (currentCartDetail != null)
            {
                currentCartDetail.Quantity += cartDetailEntity.Quantity;
                currentCartDetail.Cart.Amount += cartDetailEntity.Quantity * currentCartDetail.Item.Price;
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
                true,
                x => x.Item);
        }
    }
}
