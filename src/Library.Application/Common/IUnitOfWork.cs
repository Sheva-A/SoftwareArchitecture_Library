namespace Library.Application.Common;

// Unit of Work: зберігає всі зміни однієї бізнес-операції разом
public interface IUnitOfWork
{
    void SaveChanges();
}
