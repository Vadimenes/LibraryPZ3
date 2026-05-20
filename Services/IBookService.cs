using LibraryPZ3.Models;

namespace LibraryPZ3.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Получает список всех книг
        /// </summary>
        Task<IEnumerable<Book>> GetAllBooksAsync();

        /// <summary>
        /// Получает список всех авторов
        /// </summary>
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        /// <summary>
        /// Добавляет новую книгу
        /// </summary>
        Task AddBookAsync(Book book);

        /// <summary>
        /// Удаляет книгу по ID
        /// </summary>
        /// <returns>Сообщение об ошибке или null при успехе</returns>
        Task<string?> DeleteBookAsync(int bookId);
    }
}