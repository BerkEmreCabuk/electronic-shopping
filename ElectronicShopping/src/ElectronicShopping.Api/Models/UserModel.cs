using AutoMapper;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Repositories.Entities;

namespace ElectronicShopping.Api.Models
{
    public class UserModel: IMapping
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<UserEntity, UserModel>();
        }
    }
}
