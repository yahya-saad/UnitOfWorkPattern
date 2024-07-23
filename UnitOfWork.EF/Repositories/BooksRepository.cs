using UnitOfWork.Core.Interfaces;
using UnitOfWork.Core.Models;

namespace UnitOfWork.EF.Repositories
{
    public class BooksRepository : BaseRepository<Book>, IBooksRepository
    {
        private readonly AppDbContext _context;

        public BooksRepository(AppDbContext context) : base(context) { }

        public string SpecialMethod() => "Rick Rolled";
    }
}
