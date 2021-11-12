using ElectronicShopping.Api.Repositories.Entities;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartEntity>
    {
        Task<CartEntity> CreateAsync(long userId);
    }
}
