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
    public class AutorController : ControllerBase
    {
        IDataService<AutorDTO> dataService;
        IMapper mapper;
        public AutorController(IDataService<AutorDTO> service)
        {
            dataService = service;
            mapper = new MapperConfiguration(cfg => { cfg.CreateMap<AutorDTO, AutorView>(); 
                cfg.CreateMap<BookDTO, BookView>().ForMember(x => x.BookAutor, y => y.Ignore());
                cfg.CreateMap<AutorView, AutorDTO>();
                cfg.CreateMap<BookView, BookDTO>().ForMember(x => x.BookAutor, y => y.Ignore());
            }).CreateMapper();
        }
        // GET: api/Autor
        /// <summary>
        /// Return all autor
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("All")]
        public async Task<ICollection<AutorView>> Get()
        {
            var items = await dataService.GetAllAsync();
            return mapper.Map<IQueryable<AutorDTO>, ICollection<AutorView>>(items);
        }
        /// <summary>
        /// Get autor by page
        /// </summary>
        /// <param name="pageParameters"> page number</param>
        /// <returns>List of books</returns>
        [HttpGet, Route("Page")]
        public async Task<ICollection<AutorView>> GetPage([FromBody] PageParametersView pageParameters)
        {
            var page_p = mapper.Map<PageParametersView, PageParametersDTO>(pageParameters);
            var items = await dataService.GetPage(page_p);
            return mapper.Map<IQueryable<AutorDTO>, ICollection<AutorView>>(items);
        }
        // GET: api/Autor/5
        /// <summary>
        /// Return single record
        /// </summary>
        /// <param name="id">autor id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AutorView> Get(int id)
        {
            var item = await dataService.GetAsync(id);
            return mapper.Map<AutorDTO, AutorView>(item);
        }
        // GET: api/Autor/5
        /// <summary>
        /// Get an author by name who has at least one book in the library
        /// </summary>
        /// <param name="autorName">Autor name</param>
        /// <returns></returns>
        [HttpGet, Route("ByAutor")]
        public async Task<ICollection<AutorView>> GetByAutor(string autorName)
        {
            var items = await dataService.Find(x=>x.BookAutor.Select(x=>x.Name).Contains(autorName));
            return mapper.Map<IQueryable<AutorDTO>, ICollection<AutorView>>(items);
        }
        // POST: api/Autor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AutorView value)
        {
            var item = mapper.Map<AutorView, AutorDTO>(value);
            if (await dataService.AddAsync(item))
                return Ok();
            else
                return NotFound();
        }
        // PUT: api/Autor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AutorView value)
        {
            var item = mapper.Map<AutorView, AutorDTO>(value);
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
