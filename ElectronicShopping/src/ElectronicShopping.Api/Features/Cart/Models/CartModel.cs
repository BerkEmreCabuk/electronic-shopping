using AutoMapper;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Repositories.Entities;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Features.Cart.Models
{
    public class CartModel : IMapping
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public List<CartDetailsModel> Details { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression
                .CreateMap<CartEntity, CartModel>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom((src, dest) =>
                {
                    return src.CartDetails;
                }));
        }
    }
}
