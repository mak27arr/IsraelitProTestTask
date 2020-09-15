using IsraelitProTestTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelitProTestTask.DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> FindAsync(Func<T, Boolean> predicate);
        Task<IEnumerable<T>> GetPage(PageParameters pageParameters);
        Task<bool> CreateAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
    }
}
