using IsraelitProTestTask.DAL.EnFr;
using IsraelitProTestTask.DAL.Entities;
using IsraelitProTestTask.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IsraelitProTestTask.DAL.Repositories
{
    class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private BooksContext db;
        private IRepository<Book> booksRepository;
        private IRepository<Autor> autorsRepository;
        public EFUnitOfWork(DbContextOptions<BooksContext> options)
        {
            db = new BooksContext(options);
        }
        public IRepository<Book> BooksRepository
        {
            get
            {
                if (booksRepository == null)
                    booksRepository = new BooksRepository(db);
                return booksRepository;
            }
        }
        public IRepository<Autor> AutorsRepository
        {
            get
            {
                if (autorsRepository == null)
                    autorsRepository = new AutorsRepository(db);
                return autorsRepository;
            }
        }
        public async Task<bool> SaveAsync()
        {
            await db.SaveChangesAsync();
            return true;
        }
        public void Dispose()
        {
            if (!this.disposed)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
