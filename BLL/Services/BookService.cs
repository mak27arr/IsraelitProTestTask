using AutoMapper;
using IsraelitProTestTask.BLL.DTO;
using IsraelitProTestTask.BLL.Infrastructure;
using IsraelitProTestTask.BLL.Interfaces;
using IsraelitProTestTask.DAL.Entities;
using IsraelitProTestTask.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelitProTestTask.BLL.Services
{
    public class BookService : IDataService<BookDTO>
    {
        IUnitOfWork Database { get; set; }
        IMapper mapper;
        public BookService(IUnitOfWork uow)
        {
            Database = uow;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
        }
        public async Task<bool> AddAsync(BookDTO item)
        {
            var objective = mapper.Map<BookDTO, Book>(item);
            var rez = await Database.BooksRepository.CreateAsync(objective);
            return rez && await Database.SaveAsync();
        }
        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null || !id.HasValue)
                throw new ValidationException("Empty id", "");
            if (await Database.BooksRepository.DeleteAsync(id.Value))
            {
                return await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException("Cant delete book", "");
            }
        }
        public void Dispose()
        {
            Database.Dispose();
        }
        public async Task<BookDTO> GetAsync(int? id)
        {
            if (id == null || !id.HasValue)
                throw new ValidationException("Empty id", "");
            var item = await Database.BooksRepository.GetAsync(id.Value);
            if (item == null)
                throw new ValidationException("Not found", "");
            return mapper.Map<Book, BookDTO>(item);
        }
        public async Task<IQueryable<BookDTO>> GetAllAsync()
        {
            var books = await Database.BooksRepository.GetAllAsync();
            return mapper.Map<IQueryable<Book>, IQueryable<BookDTO>>(books);
        }
        public async Task<bool> UpdateAsync(BookDTO item)
        {
            if (item == null)
                throw new ValidationException("Empty data", "");
            var bd_item = mapper.Map<BookDTO, Book>(item);
            if (await Database.BooksRepository.UpdateAsync(bd_item))
            {
                return await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException("Cant update autor", "");
            }
        }
        public async Task<IQueryable<BookDTO>> GetPage(PageParametersDTO pageParameters)
        {
            var bd_pageParameters = mapper.Map<PageParametersDTO, PageParameters>(pageParameters);
            var rez = await Database.BooksRepository.GetPage(bd_pageParameters);
            return mapper.Map<IQueryable<Book>, IQueryable<BookDTO>>(rez);
        }
        public async Task<IQueryable<BookDTO>> Find(Func<BookDTO, bool> predict)
        {
            var predict_db = mapper.Map<Func<BookDTO, bool>, Func<Book, bool>>(predict);
            var items = await Database.BooksRepository.FindAsync(predict_db);
            return mapper.Map<IQueryable<Book>, IQueryable<BookDTO>>(items);
        }
    }
}
