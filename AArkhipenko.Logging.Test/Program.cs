using AArkhipenko.Core;
using AArkhipenko.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Добавление логирования в консоль
builder.Logging.AddConsoleLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Использование прослойки, которая добавляет ИД запроса в заголоки запроса
app.UseRequestChainMiddleware();
// Использование прослойки, которая обогащает логирование доп информацией
app.UseLoggingMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
