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
    internal class AutorsRepository : IRepository<Autor>
    {
        private BooksContext db;
        public AutorsRepository(BooksContext db)
        {
            this.db = db;
        }
        public async Task<bool> CreateAsync(Autor item)
        {
            await db.Autors.AddAsync(item);
            return true;
        }
        public async Task<bool> UpdateAsync(Autor item)
        {
            var bd_data = await db.Autors.FindAsync(item.Id);
            if (bd_data != null)
            {
                db.Entry(bd_data).State = EntityState.Modified;
                db.Entry(bd_data).CurrentValues.SetValues(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var objective = await db.Autors.FindAsync(id);
            if (objective != null)
            {
                db.Autors.Remove(objective);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Autor> GetAsync(int id)
        {
            return await db.Autors.AsNoTracking().Include(x => x.BookAutor).ThenInclude(x => x.Book).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await Task<IEnumerable<Autor>>.Factory.StartNew(() =>
            {
                return db.Autors.AsNoTracking().OrderBy(x=>x.Id).Include(x => x.BookAutor).ThenInclude(x => x.Book).ToList();
            });
        }
        public async Task<IEnumerable<Autor>> FindAsync(Func<Autor, bool> predicate)
        {
            return await Task<IEnumerable<Autor>>.Factory.StartNew(() =>
            {
                return db.Autors.AsNoTracking().Where(predicate).OrderBy(x => x.Id).AsQueryable().Include(x => x.BookAutor).ThenInclude(x => x.Book).ToList();
            });
        }

        public async Task<IEnumerable<Autor>> GetPage(PageParameters pageParameters)
        {
            return await Task<IEnumerable<Autor>>.Factory.StartNew(() =>
            {
                return db.Autors.AsNoTracking().OrderBy(on => on.Id)
                    .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                    .Take(pageParameters.PageSize).AsQueryable().Include(x=>x.BookAutor).ThenInclude(x => x.Book).ToList();
            });
        }
    }
}