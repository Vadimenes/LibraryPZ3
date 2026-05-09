using Microsoft.EntityFrameworkCore;
using LibraryPZ3.Models;

namespace LibraryPZ3.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связь 1:N
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Уникальный индекс на ISBN
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            // Тестовые данные
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Лев", LastName = "Толстой", BirthDate = new DateTime(1828, 9, 9) },
                new Author { AuthorId = 2, FirstName = "Фёдор", LastName = "Достоевский", BirthDate = new DateTime(1821, 11, 11) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Война и мир", ISBN = "978-5-17-01", AuthorId = 1 },
                new Book { BookId = 2, Title = "Анна Каренина", ISBN = "978-5-17-02", AuthorId = 1 },
                new Book { BookId = 3, Title = "Преступление и наказание", ISBN = "978-5-17-03", AuthorId = 2 }
            );
        }
    }
}