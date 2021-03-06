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
    public class AutorService : IDataService<AutorDTO>
    {
        IUnitOfWork Database { get; set; }
        IMapper mapper;
        public AutorService(IUnitOfWork uow)
        {
            Database = uow;
            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<PageParametersDTO, PageParameters>();
                cfg.CreateMap<Autor, AutorDTO>().ForMember(res => res.BookAutor, opt => opt.MapFrom(dto => dto.BookAutor.Select(x => x.Book).ToList()));
                cfg.CreateMap<AutorDTO, Autor>().ForMember(res => res.BookAutor, opt => opt.MapFrom(dto => dto.BookAutor.Select(x => new BookAutor() { BookId = x.Id, AutorId = dto.Id })));
                cfg.CreateMap<Book, BookDTO>().ForMember(res => res.BookAutor, opt => opt.Ignore());
                cfg.CreateMap<BookDTO, Book>().ForMember(res => res.BookAutor, opt => opt.Ignore());
            }).CreateMapper();
        }
        public async Task<bool> AddAsync(AutorDTO item)
        {
            item.Id = 0;
            var db_item = mapper.Map<AutorDTO, Autor>(item);
            var rez = await Database.AutorsRepository.CreateAsync(db_item);
            return rez && await Database.SaveAsync();
        }
        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null || !id.HasValue)
                throw new ValidationException("Empty id", "");
            if (await Database.AutorsRepository.DeleteAsync(id.Value))
            {
                return await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException("Cant delete autor", "");
            }
        }
        public void Dispose()
        {
            Database.Dispose();
        }
        public async Task<AutorDTO> GetAsync(int? id)
        {
            if (id == null || !id.HasValue)
                throw new ValidationException("Empty id", "");
            var item = await Database.AutorsRepository.GetAsync(id.Value);
            if (item == null)
                throw new ValidationException("Not found", "");
            return mapper.Map<Autor, AutorDTO>(item);
        }
        public async Task<IEnumerable<AutorDTO>> GetAllAsync()
        {
            var autors = await Database.AutorsRepository.GetAllAsync();
            return mapper.Map<IEnumerable<Autor>, IEnumerable<AutorDTO>>(autors);
        }
        public async Task<bool> UpdateAsync(AutorDTO item)
        {
            if (item == null)
                throw new ValidationException("Empty data", "");
            var bd_item = mapper.Map<AutorDTO, Autor>(item);
            if (await Database.AutorsRepository.UpdateAsync(bd_item))
            {
                return await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException("Cant update autor", "");
            }
        }
        public async Task<IEnumerable<AutorDTO>> GetPage(PageParametersDTO pageParameters)
        {
            var bd_pageParameters = mapper.Map<PageParametersDTO, PageParameters>(pageParameters);
            var rez = await Database.AutorsRepository.GetPage(bd_pageParameters);
            return mapper.Map<IEnumerable<Autor>, List<AutorDTO>>(rez);
        }
        public async Task<IEnumerable<AutorDTO>> Find(string autorName)
        {
            var items = await Database.AutorsRepository.FindAsync(x => x.Name.Contains(autorName));
            return mapper.Map<IEnumerable<Autor>, List<AutorDTO>>(items);
        }
    }
}
