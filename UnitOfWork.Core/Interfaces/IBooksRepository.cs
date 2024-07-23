using UnitOfWork.Core.Models;

namespace UnitOfWork.Core.Interfaces
{
    public interface IBooksRepository : IBaseRepository<Book>
    {
        string SpecialMethod();
    }
}
