using Microsoft.EntityFrameworkCore;
using LibraryPZ3.Data;
using LibraryPZ3.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключение к БД из appsettings.json
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Регистрация сервиса
builder.Services.AddScoped<ILibraryService, LibraryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    await ctx.Database.EnsureCreatedAsync();
}

// Endpoint 1: Конфигурация
app.MapGet("/api/config", (IConfiguration config) =>
{
    return Results.Ok(new
    {
        AppName = config["AppSettings:AppName"],
        Version = config["AppSettings:Version"],
        MaxItems = config.GetValue<int>("AppSettings:MaxItems")
    });
});

// Endpoint 2: Данные из БД через сервис
app.MapGet("/api/data", async (LibraryContext ctx, ILibraryService service) =>
{
    var books = await ctx.Books.Include(b => b.Author).ToListAsync();
    var formatted = await service.GetFormattedBooksAsync(books);

    return Results.Ok(new
    {
        AppVersion = builder.Configuration["AppSettings:Version"],
        ServiceInfo = service.GetServiceInfo(),
        Data = formatted
    });
});

// Endpoint 3: Книга по ID
app.MapGet("/api/books/{id:int}", async (LibraryContext ctx, int id) =>
{
    var book = await ctx.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);
    if (book == null) return Results.NotFound();

    return Results.Ok(new
    {
        book.BookId,
        book.Title,
        book.ISBN,
        Author = book.Author?.FullName
    });
});

app.Run();