using UnitOfWork.Core.Models;

namespace UnitOfWork.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Author> Authors { get; }
        IBooksRepository Books { get; }

        int SaveChages();
        Task<int> SaveChagesAsync();
    }
}
