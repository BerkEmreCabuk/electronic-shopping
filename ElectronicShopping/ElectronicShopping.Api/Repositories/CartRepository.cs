using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class CartRepository : GenericRepository<CartEntity>, ICartRepository
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public CartRepository(ElectronicShoppingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true)
        {
            return await GetAsync(
                    x =>
                    x.UserId == userId &&
                    x.Status == RecordStatuses.ACTIVE,
                    hasTracking,
                    x => x.CartDetails);
        }

        public async Task<CartEntity> GetByUserIdAsync(long userId, bool hasTracking = true)
        {
            return await GetAsync(
                    x =>
                    x.UserId == userId &&
                    x.Status == RecordStatuses.ACTIVE,
                    hasTracking);
        }
    }
}
