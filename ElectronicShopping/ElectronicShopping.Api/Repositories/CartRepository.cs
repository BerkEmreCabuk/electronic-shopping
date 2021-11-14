using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities;
using ElectronicShopping.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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

        public async Task<CartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default)
        {
            return await GetAsync(
                    x =>
                    x.UserId == userId &&
                    x.Status == RecordStatuses.ACTIVE,
                    hasTracking,
                    ct,
                    x => x.CartDetails);
        }

        public async Task<CartEntity> GetByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default)
        {
            return await GetAsync(
                    x =>
                    x.UserId == userId &&
                    x.Status == RecordStatuses.ACTIVE,
                    hasTracking, ct);
        }

        public async Task<CartEntity> GetWithAllAsync(long userId, CancellationToken ct = default)
        {
            return await Query()
                    .Include(e => e.CartDetails)
                    .ThenInclude(e => e.Item)
                    .Where(e => e.UserId == userId && e.Status == RecordStatuses.ACTIVE)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ct);
        }
    }
}
