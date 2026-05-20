using Microsoft.Extensions.Configuration;
using LibraryPZ3.Models;

namespace LibraryPZ3.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IConfiguration _config;

        public LibraryService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task<IEnumerable<BookDto>> GetFormattedBooksAsync(IEnumerable<Book> books)
        {
            var maxItems = _config.GetValue<int>("AppSettings:MaxItems", 10);

            var formatted = books
                .OrderBy(b => b.Title)
                .Take(maxItems)
                .Select(b => new BookDto
                {
                    Id = b.BookId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    AuthorName = b.Author?.FullName ?? "Неизвестный автор",
                    DisplayInfo = $"{b.BookId}|{b.Title} [{b.Author?.LastName}]"
                });

            return Task.FromResult(formatted);
        }

        public string GetServiceInfo()
        {
            return $"LibraryService v{_config["AppSettings:Version"]} | App: {_config["AppSettings:AppName"]}";
        }
    }
}