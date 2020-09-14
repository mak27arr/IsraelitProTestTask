using IsraelitProTestTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelitProTestTask.DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> BooksRepository { get; }
        IRepository<Autor> AutorsRepository { get; }
        Task<bool> SaveAsync();
    }
}
