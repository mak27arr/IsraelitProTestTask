using IsraelitProTestTask.DAL.EnFr;
using IsraelitProTestTask.DAL.Entities;
using IsraelitProTestTask.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var bd_item = await db.Books.FindAsync(item.Id);
            if (bd_item != null)
            {
                db.Entry(bd_item).State = EntityState.Modified;
                db.Entry(bd_item).CurrentValues.SetValues(item);
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
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await Task<IEnumerable<Book>>.Factory.StartNew(() =>
            {
                return db.Books.AsNoTracking().OrderBy(x => x.Id).Include(x => x.BookAutor).ThenInclude(x => x.Autor).ToList();
            });
        }
        public async Task<IEnumerable<Book>> FindAsync(Func<Book, bool> predicate)
        {
            return await Task<IEnumerable<Book>>.Factory.StartNew(() =>
            {
                return db.Books.AsNoTracking().Where(predicate).OrderBy(x => x.Id).AsQueryable().Include(x => x.BookAutor).ThenInclude(x => x.Autor).ToList();
            });
        }

        public async Task<IEnumerable<Book>> GetPage(PageParameters pageParameters)
        {
                return await Task<IEnumerable<Book>>.Factory.StartNew(() =>
                {
                    return db.Books.AsNoTracking().OrderBy(on => on.Id)
                        .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                        .Take(pageParameters.PageSize).AsQueryable().Include(x => x.BookAutor).ThenInclude(x => x.Autor).ToList();
                });
        }
    }
}