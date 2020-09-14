namespace IsraelitProTestTask.DAL.Entities
{
    public class BookAutor
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
