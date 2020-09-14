using System;
using System.Collections.Generic;

namespace IsraelitProTestTask.BLL.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? YearOfPublication { get; set; }
        public int? NumberOfPages { get; set; }
        public ICollection<AutorDTO> BookAutor { get;set;}
    }
}