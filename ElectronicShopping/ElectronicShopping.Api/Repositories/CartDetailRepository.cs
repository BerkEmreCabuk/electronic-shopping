using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using System.Threading;
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
        public async Task CreateAsync(CartDetailEntity cartDetailEntity, CancellationToken ct = default)
        {
            var currentCartDetail = await GetByCartIdAndItemIdAsync(cartDetailEntity.CartId, cartDetailEntity.ItemId, ct);
            if (currentCartDetail != null)
            {
                currentCartDetail.AddQuantity(cartDetailEntity.Quantity);
            }
            else
            {
                if (cartDetailEntity.CartId == 0)
                    cartDetailEntity.Cart.Add();

                var price = cartDetailEntity.Item == null
                    ? await _itemRepository.GetItemPriceAsync(cartDetailEntity.ItemId, ct)
                    : cartDetailEntity.Item.Price;

                cartDetailEntity.AddAmount(cartDetailEntity.Quantity, price);
                await AddAsync(cartDetailEntity, ct);
            }
        }

        public async Task<CartDetailEntity> GetByCartIdAndItemIdAsync(long cartId, long itemId, CancellationToken ct = default)
        {
            return await GetAsync(
                x =>
                x.CartId == cartId &&
                x.ItemId == itemId &&
                x.Status == RecordStatuses.ACTIVE,
                true, ct);
        }
    }
}
