using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;

namespace ElectronicShopping.Api.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public UserRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
