using AutoMapper;
using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ElectronicShoppingDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserModel> AuthenticateUser(string userName, string password, CancellationToken ct = default)
        {
            var userEntity = await GetAsync(
                    x =>
                    x.UserName == userName &&
                    x.Password == SecurityHelper.ComputeSha256Hash(password) &&
                    x.Status == RecordStatuses.ACTIVE,
                    false, ct);

            return userEntity == null ? null : _mapper.Map<UserModel>(userEntity);
        }
    }
}
