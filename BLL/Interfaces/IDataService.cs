using IsraelitProTestTask.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsraelitProTestTask.BLL.Interfaces
{
    public interface IDataService<T>
    {
        Task<T> GetAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPage(PageParametersDTO pageParameters);
        Task<IEnumerable<T>> Find(string autorName);
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int? id);
        void Dispose();
    }
}
