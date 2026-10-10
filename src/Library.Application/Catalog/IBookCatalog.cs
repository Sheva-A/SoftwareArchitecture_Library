namespace Library.Application.Catalog;

// Контракт каталогу, через який інші контексти працюють з книгами
public interface IBookCatalog
{
    void BorrowBook(Guid bookId);
    void ReturnBook(Guid bookId);
}
