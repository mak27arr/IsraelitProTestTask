using System;
using System.Collections.Generic;
namespace IsraelitProTestTask.BLL.DTO
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Name {get;set;}
        public DateTime? Year { get; set; }
        public ICollection<BookDTO> BookAutor { get; set; }
        public object Created { get; internal set; }
    }
}
