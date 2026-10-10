using System.Text.Encodings.Web;
using System.Text.Unicode;
using Library.Application.Catalog;
using Library.Application.Lending;
using Library.Domain.Catalog;
using Library.Domain.Common;
using Library.Domain.Lending;
using Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Щоб українські літери у відповідях не перетворювались на коди
var encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Encoder = encoder);
builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.Encoder = encoder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Репозиторії з шару Infrastructure підключаємо через інтерфейси з Domain
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<ILoanRepository, InMemoryLoanRepository>();

// Сервіси шару Application
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<IBookCatalog, BookService>();
builder.Services.AddScoped<LoanService>();

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
