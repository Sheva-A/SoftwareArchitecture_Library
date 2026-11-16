using System.Text.Encodings.Web;
using System.Text.Unicode;
using Library.Application.Catalog;
using Library.Application.Events;
using Library.Application.Lending;
using Library.Application.Notifications;
using Library.Application.Statistics;
using Library.Domain.Catalog;
using Library.Domain.Common;
using Library.Domain.Lending;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Time;
using Microsoft.AspNetCore.Diagnostics;

// Щоб українські літери в консолі відображались правильно
Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// Щоб українські літери у відповідях не перетворювались на коди
var encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Encoder = encoder);
builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.Encoder = encoder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Singleton: репозиторії зберігають дані в пам'яті, тому вони одні на весь застосунок
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<ILoanRepository, InMemoryLoanRepository>();

// Singleton: годинник не має стану, достатньо одного екземпляра
builder.Services.AddSingleton<IClock, SystemClock>();

// Scoped: сервіси шару Application створюються один раз на запит
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookCatalog, BookAvailabilityService>();
builder.Services.AddScoped<ILoanService, LoanService>();

// Scoped: публікація подій у межах запиту
builder.Services.AddScoped<IEventBus, InProcessEventBus>();

// Transient: обробники сповіщень не мають стану і створюються щоразу заново
builder.Services.AddTransient<IEventHandler, NewBookNotificationHandler>();
builder.Services.AddTransient<IEventHandler, ReturnReminderHandler>();

// Singleton: обробник статистики зберігає лічильник видач
builder.Services.AddSingleton<IEventHandler, LendingStatisticsHandler>();

var app = builder.Build();

// Помилки домену повертаємо як 400, а "не знайдено" як 404
app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    context.Response.StatusCode = error switch
    {
        DomainException => StatusCodes.Status400BadRequest,
        KeyNotFoundException => StatusCodes.Status404NotFound,
        _ => StatusCodes.Status500InternalServerError
    };

    await context.Response.WriteAsJsonAsync(new { error = error?.Message });
}));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
