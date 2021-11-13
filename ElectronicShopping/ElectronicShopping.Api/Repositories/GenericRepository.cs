using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Repositories.Entities.Common;
using ElectronicShopping.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ElectronicShoppingDbContext _dbContext;

        public GenericRepository(ElectronicShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<IList<T>> GetAllAsync(bool hasTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbContext.Set<T>().Where(x => x.Status == RecordStatuses.ACTIVE);
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }

            return hasTracking
                ? await query.ToListAsync()
                : await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.Status == RecordStatuses.ACTIVE);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool hasTracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbContext.Set<T>().Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return hasTracking
                ? await query.FirstOrDefaultAsync()
                : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await Query().AnyAsync(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.Add();
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            entity.Update();
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            entity.Delete();
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
