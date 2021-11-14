using AutoMapper;
using ElectronicShopping.Api.Features.Item.Models;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Repositories.Entities;

namespace ElectronicShopping.Api.Features.Cart.Models
{
    public class CartDetailsModel : IMapping
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public ItemModel Item { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<CartDetailEntity, CartDetailsModel>();
        }
    }
}
