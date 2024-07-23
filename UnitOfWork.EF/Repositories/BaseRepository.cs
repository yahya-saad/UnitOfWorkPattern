using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UnitOfWork.Core.Interfaces;

namespace UnitOfWork.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll() => context.Set<T>().ToList();
        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();
        public T? GetById(int id) => context.Set<T>().Find(id);
        public async Task<T?> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);

        public T? Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var result = query.FirstOrDefault(criteria);

            return result is null ? null : result;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var result = await query.FirstOrDefaultAsync(criteria);
            return result is null ? null : result;
        }

        public IEnumerable<T> FindAll(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().Where(criteria);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);


            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().Where(criteria);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);


            return await query.ToListAsync();
        }

        public IEnumerable<T> FindAllWithOrdering(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().Where(criteria);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }


            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllWithOrderingAsync(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>().Where(criteria);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }


            return await query.ToListAsync();
        }

        public T Add(T entity)
        {
            var entry = context.Set<T>().Add(entity);
            return entry.Entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entry = await context.Set<T>().AddAsync(entity);
            return entry.Entity;
        }


        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public int Count() => context.Set<T>().Count();
        public int Count(Expression<Func<T, bool>> criteria) => context.Set<T>().Count(criteria);

        public async Task<int> CountAsync() => await context.Set<T>().CountAsync();

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria) => await context.Set<T>().CountAsync(criteria);

    }


}
