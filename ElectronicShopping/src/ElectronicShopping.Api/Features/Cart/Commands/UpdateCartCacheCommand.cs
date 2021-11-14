using AutoMapper;
using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Infrastructure.Cache;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Cart.Commands
{
    public class UpdateCartCacheCommand : IRequest
    {
        public UpdateCartCacheCommand(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; set; }
    }

    public class UpdateShoppingCartCacheCommandHandler : AsyncRequestHandler<UpdateCartCacheCommand>
    {
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateShoppingCartCacheCommandHandler(
            Lazy<ICartRepository> cartRepository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateCartCacheCommand request, CancellationToken ct)
        {
            var shoppingCart = await _cartRepository.Value.GetWithDetailByUserIdAsync(request.UserId, false, ct);

            var shoppingCartModel = _mapper.Map<CartModel>(shoppingCart);

            await _cacheService.Add($"{CacheKeyConstant.CART_INFO}{request.UserId}", shoppingCartModel);
        }
    }
}