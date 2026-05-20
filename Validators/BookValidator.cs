using FluentValidation;
using LibraryPZ3.Models;

namespace LibraryPZ3.Validators // Имя пространства имен валидаторов
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название книги обязательно!")
                .MaximumLength(200).WithMessage("Название слишком длинное.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Пожалуйста, выберите автора.");

            RuleFor(x => x.ISBN)
                .MaximumLength(20)  // ← мягкая проверка длины
                .Matches(@"^(\d{3}-\d{3}-\d{3}|\d{10}|\d{13})?$")  // ← допускает пустой, 10 или 13 цифр
                .WithMessage("ISBN должен быть в формате 123-456-789, 10 или 13 цифр");
        }
    }
}