using Library.Application.BookCatalog.EventHandlers;
using Library.Application.BookCatalog.Services;
using Library.Application.Common;
using Library.Application.Contracts;
using Library.Application.Lending.EventHandlers;
using Library.Application.Lending.Services;
using Library.Domain.BookCatalog.Events;
using Library.Domain.BookCatalog.Repositories;
using Library.Domain.Lending.Events;
using Library.Domain.Lending.Repositories;
using Library.Infrastructure.BookCatalog.Repositories;
using Library.Infrastructure.BookCatalog.Services;
using Library.Infrastructure.Events;
using Library.Infrastructure.Lending.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure;

// Клас для реєстрації всіх сервісів інфраструктурного шару в DI-контейнері.
// WebApi викликає лише цей метод — і не знає про деталі реалізацій.
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Репозиторії реєструємо як Singleton, щоб дані зберігалися між запитами (in-memory)
        services.AddSingleton<IBookRepository, InMemoryBookRepository>();
        services.AddSingleton<IAuthorRepository, InMemoryAuthorRepository>();
        services.AddSingleton<IMemberRepository, InMemoryMemberRepository>();
        services.AddSingleton<ILoanRepository, InMemoryLoanRepository>();

        // Cross-context сервіс
        services.AddScoped<IBookAvailabilityService, BookAvailabilityService>();

        // Диспетчер подій — слабке зв'язування через IServiceProvider
        services.AddScoped<IEventDispatcher, InMemoryEventDispatcher>();

        // Обробники доменних подій
        services.AddScoped<IEventHandler<BookBorrowedEvent>, BookBorrowedEventHandler>();
        services.AddScoped<IEventHandler<BookReturnedEvent>, BookReturnedEventHandler>();
        services.AddScoped<IEventHandler<BookCreatedEvent>, BookCreatedEventHandler>();

        // Сервіси прикладного шару
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ILoanService, LoanService>();

        return services;
    }
}
