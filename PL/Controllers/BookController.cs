using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            mapper = new MapperConfiguration(cfg => { cfg.CreateMap<BookDTO, BookView>();
                cfg.CreateMap<BookView, BookDTO>();
                cfg.CreateMap<AutorDTO, AutorView>().ForMember(x=>x.BookAutor,y=>y.Ignore());
                cfg.CreateMap<AutorView, AutorDTO>().ForMember(x => x.BookAutor, y => y.Ignore());
            }).CreateMapper();
        }
        // GET: api/Autor
        [HttpGet, Route("All")]
        public async Task<IEnumerable<BookView>> Get()
        {
            var items = await dataService.GetAllAsync();
            return mapper.Map<IEnumerable<BookDTO>, IEnumerable<BookView>>(items);
        }
        /// <summary>
        /// Get books by page
        /// </summary>
        /// <param name="pageParameters"> page number</param>
        /// <returns>List of books</returns>
        [HttpGet,Route("Page")]
        public async Task<IEnumerable<BookView>> GetPage(int id)
        {
            var pageParameters = new PageParametersDTO() { PageNumber = id };
            var items = await dataService.GetPage(pageParameters);
            return mapper.Map<IEnumerable<BookDTO>, IEnumerable<BookView>>(items);
        }
        // GET: api/Autor/5
        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BookView> Get(int id)
        {
            var item = await dataService.GetAsync(id);
            return mapper.Map<BookDTO, BookView>(item);
        }
        // GET: api/Autor/5
        /// <summary>
        /// Returns books where autor name is autorName
        /// </summary>
        /// <param name="autorName">Autor name</param>
        /// <returns></returns>
        [HttpGet, Route("ByAutor")]
        public async Task<IEnumerable<BookView>> GetByAutor(string autorName)
        {
            var items = await dataService.Find(autorName);
            return mapper.Map<IEnumerable<BookDTO>, IEnumerable<BookView>>(items);
        }
        // POST: api/Autor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookView value)
        {
            var item = mapper.Map<BookView, BookDTO>(value);
            if (await dataService.AddAsync(item))
                return Ok();
            else
                return NotFound();
        }
        // PUT: api/Autor/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookView value)
        {
            var item = mapper.Map<BookView, BookDTO>(value);
            if (await dataService.UpdateAsync(item))
                return Ok();
            else
                return NotFound();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await dataService.DeleteAsync(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
