using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IsraelitProTestTask.BLL.DTO;
using IsraelitProTestTask.BLL.Interfaces;
using IsraelitProTestTask.PL.Model;
using Microsoft.AspNetCore.Mvc;

namespace IsraelitProTestTask.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IDataService<BookDTO> dataService;
        IMapper mapper;
        public BookController(IDataService<BookDTO> service)
        {
            dataService = service;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookView>()).CreateMapper();
        }
        // GET: api/Autor
        [HttpGet]
        public async Task<IEnumerable<BookView>> Get()
        {
            var items = await dataService.GetAllAsync();
            return mapper.Map<IQueryable<BookDTO>, ICollection<BookView>>(items);
        }
        /// <summary>
        /// Get books by page
        /// </summary>
        /// <param name="pageParameters"> page number</param>
        /// <returns>List of books</returns>
        [HttpGet]
        public async Task<ICollection<BookView>> GetPage([FromQuery] PageParametersView pageParameters)
        {
            var page_p = mapper.Map<PageParametersView, PageParametersDTO>(pageParameters);
            var items = await dataService.GetPage(page_p);
            return mapper.Map<IQueryable<BookDTO>, ICollection<BookView>>(items);
        }
        // GET: api/Autor/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<BookView> Get(int id)
        {
            var item = await dataService.GetAsync(id);
            return mapper.Map<BookDTO, BookView>(item);
        }
        // GET: api/Autor/5
        [HttpGet("{id}", Name = "GetByAutor")]
        public async Task<ICollection<BookView>> GetByAutor(string name)
        {
            var items = await dataService.Find(x => x.BookAutor.Select(x => x.Name).Contains(name));
            return mapper.Map<IQueryable<BookDTO>, ICollection<BookView>>(items);
        }
        // POST: api/Autor
        [HttpPost]
        public async Task<bool> Post([FromBody] BookView value)
        {
            var item = mapper.Map<BookView, BookDTO>(value);
            return await dataService.AddAsync(item);
        }
        // PUT: api/Autor/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] BookView value)
        {
            var item = mapper.Map<BookView, BookDTO>(value);
            return await dataService.UpdateAsync(item);
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await dataService.DeleteAsync(id);
        }
    }
}
