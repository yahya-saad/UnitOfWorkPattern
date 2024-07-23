using System.Linq.Expressions;

namespace UnitOfWork.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T? Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            string[] includes = null);
        IEnumerable<T> FindAllWithOrdering(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            string[] includes = null);
        Task<IEnumerable<T>> FindAllWithOrderingAsync(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            string[] includes = null);

        T Add(T entity);
        Task<T> AddAsync(T entity);

        void Update(T entity);
        void Delete(T entity);
        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    }
}
