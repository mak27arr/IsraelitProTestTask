﻿using AutoMapper;
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
            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<PageParametersDTO, PageParameters>();
                cfg.CreateMap<Book, BookDTO>().ForMember(res => res.BookAutor, opt => opt.MapFrom(dto => dto.BookAutor.Select(x => x.Autor).ToList()));
                cfg.CreateMap<BookDTO, Book>().ForMember(res => res.BookAutor, opt => opt.MapFrom(dto => dto.BookAutor.Select(x => new BookAutor() { BookId = dto.Id, AutorId = x.Id })));
                cfg.CreateMap<Autor, AutorDTO>().ForMember(res => res.BookAutor, opt => opt.Ignore());
                cfg.CreateMap<AutorDTO, Autor>().ForMember(res => res.BookAutor, opt => opt.Ignore());
                //cfg.CreateMap<IQueryable<Book>, IQueryable<BookDTO>>().ForMember(res => res.BookAutor, opt => opt.MapFrom(dto => dto.BookAutor.Select(x => x.Autor)));
            }).CreateMapper();
        }
        public async Task<bool> AddAsync(BookDTO item)
        {
            item.Id = 0;
            var db_item = mapper.Map<BookDTO, Book>(item);
            var rez = await Database.BooksRepository.CreateAsync(db_item);
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
        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await Database.BooksRepository.GetAllAsync();
            return mapper.Map<IEnumerable<Book>, List<BookDTO>>(books);
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
        public async Task<IEnumerable<BookDTO>> GetPage(PageParametersDTO pageParameters)
        {
            var bd_pageParameters = mapper.Map<PageParametersDTO, PageParameters>(pageParameters);
            var rez = await Database.BooksRepository.GetPage(bd_pageParameters);
            return mapper.Map<IEnumerable<Book>, List<BookDTO>>(rez);
        }
        public async Task<IEnumerable<BookDTO>> Find(string autorName)
        {
            var items = await Database.BooksRepository.FindAsync(x => x.BookAutor.Select(x => x.Autor.Name).Contains(autorName));
            return mapper.Map<IEnumerable<Book>, List<BookDTO>>(items);
        }
    }
}
