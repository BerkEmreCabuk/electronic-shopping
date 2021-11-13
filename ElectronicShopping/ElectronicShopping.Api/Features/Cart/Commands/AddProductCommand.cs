using AutoMapper;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Cart.Commands
{
    public class AddProductCommand : IRequest, IMapping
    {
        public CartEntity Cart { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression
                .CreateMap<AddProductCommand, CartDetailEntity>()
                .ForMember(dest => dest.Cart, opt => opt.MapFrom((src, dest) =>
                {
                    return src.Cart ?? new CartEntity() { UserId = src.UserId };
                }));

            profileExpression.CreateMap<AddProductRequestModel, AddProductCommand>();
        }
    }

    public class AddProductCommandHandler : AsyncRequestHandler<AddProductCommand>
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(
            ICartDetailRepository cartDetailRepository,
            ICartRepository cartRepository,
            IStockRepository stockRepository,
            IMapper mapper)
        {
            _cartDetailRepository = cartDetailRepository;
            _cartRepository = cartRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        protected override async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByItemId(request.ItemId, true);

            if (stock.InsufficientFreeQuantity(request.Quantity))
                throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");

            request.Cart = await _cartRepository.GetByUserIdAsync(request.UserId, true);
            await _cartDetailRepository.CreateAsync(_mapper.Map<CartDetailEntity>(request));

            stock.ReduceFreeQuantity(request.Quantity);
            await _cartDetailRepository.SaveChangeAsync();
        }
    }
}
