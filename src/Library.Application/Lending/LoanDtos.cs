namespace Library.Application.Lending;

// Дані для видачі книги
public record BorrowBookDto(Guid BookId, string ReaderName);

// Результат видачі: до якої дати повернути книгу
public record BorrowBookResultDto(Guid Id, Guid BookId, string ReaderName, DateTime DueDate);

// Результат повернення книги
public record ReturnBookResultDto(Guid Id, Guid BookId, DateTime? ReturnDate);

// Видача у списку видач
public record LoanDto(Guid Id, Guid BookId, string ReaderName, DateTime LoanDate, DateTime DueDate,
    DateTime? ReturnDate);
