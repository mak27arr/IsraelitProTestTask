using System;
using System.Collections.Generic;
namespace IsraelitProTestTask.PL.Model
{
    public class AutorView
    {
        public int Id { get; set; }
        public string Name {get;set;}
        public DateTime? Year { get; set; }
        public ICollection<BookView> BookAutor { get; set; }
        public object Created { get; internal set; }
    }
}
