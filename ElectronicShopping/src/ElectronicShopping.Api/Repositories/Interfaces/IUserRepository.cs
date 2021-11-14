using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Repositories.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserModel> AuthenticateUser(string userName, string password, CancellationToken ct = default);
    }
}
