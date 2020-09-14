using IsraelitProTestTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IsraelitProTestTask.DAL.EnFr
{
    public class BooksContext : DbContext
    {
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAutor> BooksAutors { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BookAutor>()
                .HasKey(bc => new { bc.BookId, bc.AutorId });
            modelBuilder.Entity<BookAutor>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookAutor)
                .HasForeignKey(bc => bc.BookId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BookAutor>()
                .HasOne(bc => bc.Autor)
                .WithMany(c => c.BookAutor)
                .HasForeignKey(bc => bc.AutorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
