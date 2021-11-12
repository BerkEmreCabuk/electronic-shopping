using ElectronicShopping.Api.Repositories.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> Query();
        Task<IList<T>> GetAllAsync(bool hasTracking = false);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool hasTracking = false);
        Task<bool> ExistAsync(int id);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
