using AutoMapper;

namespace ElectronicShopping.Api.Infrastructure.Mapper
{
    public interface IMapping
    {
        void CreateMappings(IProfileExpression profileExpression);
    }
}
