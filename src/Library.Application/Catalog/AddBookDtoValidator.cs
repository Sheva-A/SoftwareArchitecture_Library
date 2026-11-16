using FluentValidation;

namespace Library.Application.Catalog;

// Правила перевірки даних для додавання книги
public class AddBookDtoValidator : AbstractValidator<AddBookDto>
{
    public AddBookDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Назва книги обов'язкова")
            .MaximumLength(200).WithMessage("Назва книги має містити не більше 200 символів");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Автор книги обов'язковий")
            .MaximumLength(100).WithMessage("Ім'я автора має містити не більше 100 символів");

        RuleFor(x => x.Isbn)
            .NotEmpty().WithMessage("ISBN обов'язковий")
            .MaximumLength(17).WithMessage("ISBN має містити не більше 17 символів");

        // Семантична перевірка: автор і назва книги не можуть збігатися
        RuleFor(x => x.Author)
            .NotEqual(x => x.Title, StringComparer.OrdinalIgnoreCase)
            .WithMessage("Автор не може збігатися з назвою книги")
            .When(x => !string.IsNullOrWhiteSpace(x.Author));
    }
}
