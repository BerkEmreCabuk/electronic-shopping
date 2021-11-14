using AutoMapper;
using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Cart.Queries
{
    public class GetCartQuery : IRequest<GetCartResponseModel>
    {
        public GetCartQuery(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; set; }
    }

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, GetCartResponseModel>
    {
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly IMapper _mapper;

        public GetCartQueryHandler(
            Lazy<ICartRepository> cartRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<GetCartResponseModel> Handle(GetCartQuery request, CancellationToken ct)
        {
            var cart = await _cartRepository.Value.GetWithAllAsync(request.UserId);

            if (cart == null)
                throw new NotFoundException("Cart not found.");

            return _mapper.Map<GetCartResponseModel>(cart);
        }
    }
}
