using UnitOfWork.Core.Interfaces;
using UnitOfWork.Core.Models;
using UnitOfWork.EF.Repositories;

namespace UnitOfWork.EF
{

    public class UUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IBaseRepository<Author> Authors { get; private set; }

        public IBooksRepository Books { get; private set; }

        public UUnitOfWork(AppDbContext context)
        {
            _context = context;
            Authors = new BaseRepository<Author>(_context);
            Books = new BooksRepository(_context);
        }

        public int SaveChages() => _context.SaveChanges();
        public async Task<int> SaveChagesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

    }
}
