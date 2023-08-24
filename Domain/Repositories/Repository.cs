using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {

        private readonly NewsFeedContext _context;

        public Repository(NewsFeedContext context)
        {
            _context = context;
        }

        public IQueryable<T> AsQueryable()
        {
            var query = _context.Set<T>().AsQueryable();

            if (typeof(T).GetInterfaces().Any(id => id.Name == nameof(ISoftDelete)))
                query = query.Where(e => EF.Property<bool>(e, nameof(ISoftDelete.IsDeleted)) == false);
            return query;
        }

        public IQueryable<T> Where(List<Expression<Func<T, bool>>> expressions)
        {
            var query = AsQueryable();
            if (expressions?.Any() is true)
                foreach (var expression in expressions)
                    query = query.Where(expression);


            return query;
        }

        public IQueryable<T> WhereIf(bool isValid, Expression<Func<T, bool>> expression)
        {
            expression ??= x => true;

            var query = AsQueryable();
            if (isValid)
                query = query.Where(expression);


            return query;
        }

        public async Task<List<TResult>> Select<TResult>(Expression<Func<T, TResult>> selector,
                                                         Expression<Func<T, bool>> exp = null)
        {

            exp ??= x => true;
            return await WhereIf(true, exp).Select(selector).ToListAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> exp = null)
        {
            exp ??= x => true;
            return await AsQueryable().FirstOrDefaultAsync(exp);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> exp = null)
        {
            exp ??= x => true;
            return await WhereIf(true, exp).ToListAsync();
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector,Expression < Func<T, bool>> exp = null)
        {
            exp ??= x => true;
            if (selector == null)
                return default;
            return await WhereIf(true, exp).MaxAsync(selector);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> exp = null)
        {
            exp ??= x => true;
            return await AsQueryable().AnyAsync(exp);
        }
        public async Task<bool> NotExist(Expression<Func<T, bool>> exp = null)
        {
            return !(await Any(exp));
        }
        public async Task<int> Count(Expression<Func<T, bool>> exp = null)
        {
            exp ??= x => true;
            return await AsQueryable().CountAsync(exp);
        }

        public async Task Insert(T entity)
        {
            if (entity == null)
                return;
            if (typeof(T).GetInterfaces().Any(id => id.Name == nameof(ITrack)))
            {
                entity.GetType().GetProperty(nameof(ITrack.CreatedAt))?.SetValue(entity, DateTime.Now, null);
            }
            await _context.AddAsync(entity);
        }

        public async Task Update(T entity)
        {

            if (entity == null)
                return;
            if (typeof(T).GetInterfaces().Any(id => id.Name == nameof(ITrack)))
            {
                entity.GetType().GetProperty(nameof(ITrack.UpdatedAt))?.SetValue(entity, DateTime.Now, null);
            }
            _context.Update(entity);
            await Task.CompletedTask;
        }

        public async Task Delete(T entity)
        {

            if (entity == null)
                return;
            if (typeof(T).GetInterfaces().Any(id => id.Name == nameof(ISoftDelete)))
            {
                entity.GetType().GetProperty(nameof(ISoftDelete.IsDeleted))?.SetValue(entity, true, null);
               await Update(entity);
                return;
            }
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
    }
}
