using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IsraelitProTestTask.DAL.Entities
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name {get;set;}
        public DateTime? Year { get; set; }
        public ICollection<BookAutor> BookAutor { get; set; }
        public object Created { get; internal set; }
    }
}
