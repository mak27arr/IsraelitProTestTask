using System;
using System.Collections.Generic;

namespace IsraelitProTestTask.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? YearOfPublication { get; set; }
        public int? NumberOfPages { get; set; }
        public ICollection<BookAutor> BookAutor { get;set;}
    }
}