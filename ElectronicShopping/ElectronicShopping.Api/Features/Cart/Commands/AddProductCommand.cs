using AutoMapper;
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
        public long ItemId { get; set; }
        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<AddProductCommand, CartDetailEntity>();
        }
    }

    public class AddProductCommandHandler : AsyncRequestHandler<AddProductCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserModel _userModel;
        public AddProductCommandHandler(
            ICartRepository cartRepository,
            ICartDetailRepository cartDetailRepository,
            IStockRepository stockRepository,
            UserModel userModel)
        {
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _userModel = userModel;
        }
        protected override async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByItemId(request.ItemId);
            
            if(request.Quantity>stock.FreeQuantity)
            {
                throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");
            }
            
            if (!request.CartId.HasValue)
            {
                await _cartRepository.CreateAsync(_userModel.Id);
            }
        }
    }
}
