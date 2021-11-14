using AutoMapper;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Repositories.Entities;

namespace ElectronicShopping.Api.Features.Item.Models
{
    public class ItemModel : IMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<ItemEntity, ItemModel>();
        }
    }
}
