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
        public long? CartId { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression
                .CreateMap<AddProductCommand, CartDetailEntity>()
                .ForMember(dest => dest.Cart, opt =>
                {
                    opt.PreCondition(src => !src.CartId.HasValue);
                    opt.MapFrom(src => new CartEntity() { UserId = src.UserId });
                })
                .ForMember(dest => dest.CartId, opt => opt.Condition(src => (src.CartId.HasValue)));

            profileExpression.CreateMap<AddProductRequestModel, AddProductCommand>();
        }
    }

    public class AddProductCommandHandler : AsyncRequestHandler<AddProductCommand>
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(
            ICartDetailRepository cartDetailRepository,
            IStockRepository stockRepository,
            IMapper mapper)
        {
            _cartDetailRepository = cartDetailRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        protected override async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByItemId(request.ItemId);

            if (request.Quantity > stock.FreeQuantity)
            {
                throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");
            }

            await _cartDetailRepository.CreateAsync(_mapper.Map<CartDetailEntity>(request));
        }
    }
}
