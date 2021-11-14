using AutoMapper;
using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Infrastructure.Cache;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Cart.Queries
{
    public class GetCartQuery : IRequest<CartModel>
    {
        public GetCartQuery(long userId, bool fromCache = true)
        {
            UserId = userId;
            FromCache = fromCache;
        }

        public long UserId { get; set; }
        public bool FromCache { get; set; }
    }

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartModel>
    {
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetCartQueryHandler(
            Lazy<ICartRepository> cartRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<CartModel> Handle(GetCartQuery request, CancellationToken ct)
        {
            if (request.FromCache)
            {
                var shoppingCartModel = await _cacheService.Get<CartModel>($"{CacheKeyConstant.CartInfo}{request.UserId}");
                if (shoppingCartModel == null)
                {
                    var shoppingCart = await _cartRepository.Value.GetWithAllAsync(request.UserId, ct);
                    if (shoppingCart == null)
                        throw new NotFoundException("cart not found");
                    await _cacheService.Add($"{CacheKeyConstant.CartInfo}{request.UserId}", shoppingCart);
                    return _mapper.Map<CartModel>(shoppingCart);
                }
                return shoppingCartModel;
            }
            else
            {
                var shoppingCart = await _cartRepository.Value.GetWithAllAsync(request.UserId, ct);
                if (shoppingCart == null)
                    throw new NotFoundException("cart not found");

                return _mapper.Map<CartModel>(shoppingCart);
            }
        }
    }
}
