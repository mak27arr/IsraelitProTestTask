using IsraelitProTestTask.BLL.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelitProTestTask.BLL.Interfaces
{
    public interface IDataService<T>
    {
        Task<T> GetAsync(int? id);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetPage(PageParametersDTO pageParameters);
        Task<IQueryable<T>> Find(Func<T,bool> predict);
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int? id);
        void Dispose();
    }
}
