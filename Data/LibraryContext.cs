using System;
using Microsoft.EntityFrameworkCore;
using LibraryPZ3.Models;

namespace LibraryPZ3.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<BookMovement> BookMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Настройка модели Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ISBN).HasMaxLength(20);

                entity.HasOne(d => d.Author)
                      .WithMany(p => p.Books)
                      .HasForeignKey(d => d.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 2. Настройка модели Author
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AuthorId);
                entity.Ignore(e => e.FullName);
            });

            // 3. Настройка модели BookMovement
            modelBuilder.Entity<BookMovement>(entity =>
            {
                entity.HasKey(e => e.MovementId);
                entity.HasOne(d => d.Book).WithMany().HasForeignKey(d => d.BookId).OnDelete(DeleteBehavior.Cascade);
            });
            // 4. Настройка модели Reader (добавь в OnModelCreating)
            modelBuilder.Entity<Reader>(entity =>
            {
                entity.HasKey(e => e.ReaderId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ClassNumber).HasMaxLength(20);
                entity.Ignore(e => e.FullName);  // ← computed property, не маппим!
            });
            // --- ДОБАВЛЕНИЕ НАЧАЛЬНЫХ ДАННЫХ ---

            // Добавляем авторов
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Александр", LastName = "Пушкин", BirthDate = new DateTime(1799, 6, 6) },
                new Author { AuthorId = 2, FirstName = "Михаил", LastName = "Лермонтов", BirthDate = new DateTime(1814, 10, 15) }
            );

            // Добавляем книги
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Евгений Онегин", ISBN = "123-456-789", AuthorId = 1 },
                new Book { BookId = 2, Title = "Капитанская дочка", ISBN = "987-654-321", AuthorId = 1 },
                new Book { BookId = 3, Title = "Герой нашего времени", ISBN = "111-222-333", AuthorId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}