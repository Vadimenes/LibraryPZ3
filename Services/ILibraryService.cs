using LibraryPZ3.Models;

namespace LibraryPZ3.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<BookDto>> GetFormattedBooksAsync(IEnumerable<Book> books);
        string GetServiceInfo();
    }

    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ISBN { get; set; }
        public string AuthorName { get; set; }
        public string DisplayInfo { get; set; }
    }
}