namespace Library.Application.Lending.Events;

// Подія: книгу видали читачу
public record BookBorrowed(Guid LoanId, Guid BookId, string ReaderName, DateTime DueDate);
