using IsraelitProTestTask.DAL.EnFr;
using IsraelitProTestTask.DAL.Entities;
using IsraelitProTestTask.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelitProTestTask.DAL.Repositories
{
    internal class BooksRepository : IRepository<Book>
    {
        private BooksContext db;

        public BooksRepository(BooksContext db)
        {
            this.db = db;
        }
        public async Task<bool> CreateAsync(Book item)
        {
            await db.Books.AddAsync(item);
            return true;
        }
        public async Task<bool> UpdateAsync(Book item)
        {
            var objective = await db.Books.FindAsync(item);
            if (objective != null)
            {
                db.Entry(item).State = EntityState.Modified;
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var objective = await db.Books.FindAsync(id);
            if (objective != null)
            {
                db.Books.Remove(objective);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Book> GetAsync(int id)
        {
            return await db.Books.AsNoTracking().Include(x => x.BookAutor).ThenInclude(x=>x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IQueryable<Book>> GetAllAsync()
        {
            return await Task<IQueryable<Book>>.Factory.StartNew(() =>
            {
                return db.Books.AsNoTracking().OrderBy(x => x.Id).Include(x => x.BookAutor).ThenInclude(x => x.Autor);
            });
        }
        public async Task<IQueryable<Book>> FindAsync(Func<Book, bool> predicate)
        {
            return await Task<IQueryable<Book>>.Factory.StartNew(() =>
            {
                return db.Books.AsNoTracking().Where(predicate).OrderBy(x => x.Id).AsQueryable().Include(x => x.BookAutor).ThenInclude(x => x.Autor);
            });
        }

        public async Task<IQueryable<Book>> GetPage(PageParameters pageParameters)
        {
                return await Task<IQueryable<Book>>.Factory.StartNew(() =>
                {
                    return db.Books.AsNoTracking().OrderBy(on => on.Id)
                        .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                        .Take(pageParameters.PageSize).AsQueryable().Include(x => x.BookAutor).ThenInclude(x => x.Autor);
                });
        }
    }
}