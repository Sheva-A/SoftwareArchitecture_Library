using Library.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Реєстрація всіх сервісів через єдиний метод розширення з Infrastructure
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
