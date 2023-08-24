using Domain.Context;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class,IEntity
    {
        IQueryable<T> AsQueryable();
        IQueryable<T> Where(List<Expression<Func<T, bool>>> expressions);
        IQueryable<T> WhereIf(bool isValid, Expression<Func<T, bool>> expression);
        Task<List<TResult>> Select<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> exp = null);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> exp = null);
        Task<List<T>> GetAll(Expression<Func<T, bool>> exp = null);

        Task<bool> Any(Expression<Func<T, bool>> exp = null);
        Task<bool> NotExist(Expression<Func<T, bool>> exp = null);
        Task<int> Count(Expression<Func<T, bool>> exp = null);
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> exp = null);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(T entity);
        Task SaveChanges();








    }
}
