using FluentValidation;

namespace Library.Application.Lending;

// Правила перевірки даних для видачі книги
public class BorrowBookDtoValidator : AbstractValidator<BorrowBookDto>
{
    public BorrowBookDtoValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Id книги обов'язковий");

        RuleFor(x => x.ReaderName)
            .NotEmpty().WithMessage("Ім'я читача обов'язкове")
            .MaximumLength(100).WithMessage("Ім'я читача має містити не більше 100 символів");
    }
}
