using AutoMapper;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Cart.Commands
{
    public class UpdateProductCommand : IRequest, IMapping
    {
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<UpdateProductRequestModel, UpdateProductCommand>();
        }
    }

    public class UpdateProductCommandHandler : AsyncRequestHandler<UpdateProductCommand>
    {
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<IStockRepository> _stockRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(
            Lazy<ICartRepository> cartRepository,
            Lazy<IStockRepository> stockRepository,
            IMediator mediator,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _cartRepository = cartRepository;
            _stockRepository = stockRepository;
            _mediator = mediator;
            _logger = logger;
        }
        protected override async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.Value.GetWithDetailByUserIdAsync(request.UserId);
            if (cart == null)
                throw new NotFoundException("cart not found");

            var cartDetail = cart.CartDetails.FirstOrDefault(x => x.ItemId == request.ItemId);
            if (cartDetail == null)
                throw new NotFoundException("product not found in cart");

            var stock = await _stockRepository.Value.GetByItemId(request.ItemId);
            var differenceQuantity = request.Quantity - cartDetail.Quantity;
            if (request.Quantity == 0)
            {
                cartDetail.Delete();
                cart.AddAmount(differenceQuantity * cartDetail.Item.Price);
                _logger.LogInformation("Cart detail deleted {@cartDetail}", cartDetail);
            }
            else
            {
                if (differenceQuantity > 0 && stock.InsufficientFreeQuantity(differenceQuantity))
                    throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");

                cartDetail.ChangeQuantity(request.Quantity);
            }
            stock.ReduceFreeQuantity(differenceQuantity);
            await _cartRepository.Value.SaveChangeAsync();
            _logger.LogInformation("Update product {@cartDetail}", cartDetail);

            await _mediator.Send(new UpdateCartCacheCommand(request.UserId), cancellationToken);
        }
    }
}