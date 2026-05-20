using LibraryPZ3.Models;
using LibraryPZ3.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryPZ3.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает список всех книг с авторами
        /// </summary>
        public async Task<IEnumerable<Book>> GetAllBooksAsync() =>
            await _context.Books.Include(b => b.Author).ToListAsync();

        /// <summary>
        /// Получает список всех авторов
        /// </summary>
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync() =>
            await _context.Authors.ToListAsync();

        /// <summary>
        /// Добавляет новую книгу
        /// </summary>
        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет книгу по ID
        /// </summary>
        public async Task<string?> DeleteBookAsync(int bookId)
        {
            // Проверка: не выдана ли книга
            var isIssued = await _context.BookMovements
                .AnyAsync(m => m.BookId == bookId && !m.IsReturned);

            if (isIssued)
                return "Нельзя удалить книгу, пока она числится в журнале выдачи!";

            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                return "Книга не найдена";

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}