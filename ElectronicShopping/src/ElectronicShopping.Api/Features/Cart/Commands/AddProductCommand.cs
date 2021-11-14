using AutoMapper;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly Lazy<ICartDetailRepository> _cartDetailRepository;
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<IStockRepository> _stockRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<AddProductCommandHandler> _logger;
        public AddProductCommandHandler(
            Lazy<ICartDetailRepository> cartDetailRepository,
            Lazy<ICartRepository> cartRepository,
            Lazy<IStockRepository> stockRepository,
            IMapper mapper,
            IMediator mediator,
            ILogger<AddProductCommandHandler> logger)
        {
            _cartDetailRepository = cartDetailRepository;
            _cartRepository = cartRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        protected override async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.Value.GetByItemId(request.ItemId, true);

            if (stock.InsufficientFreeQuantity(request.Quantity))
                throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");

            request.Cart = await _cartRepository.Value.GetByUserIdAsync(request.UserId, true);
            await _cartDetailRepository.Value.CreateAsync(_mapper.Map<CartDetailEntity>(request));

            stock.ReduceFreeQuantity(request.Quantity);
            await _cartDetailRepository.Value.SaveChangeAsync();
            _logger.LogInformation("Add product {@request}", request);

            await _mediator.Send(new UpdateCartCacheCommand(request.UserId), cancellationToken);
        }
    }
}
