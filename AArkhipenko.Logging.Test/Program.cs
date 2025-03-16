using AArkhipenko.Logging;
using Serilog;
using Serilog.Enrichers.AspNetCore.HttpContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Logging.AddConsoleLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    // Добавление в заголовок запроса RequestId, если его нет
    if (!context.Request.Headers.TryGetValue(Consts.RequestChainId, out var requestId))
    {
        context.Request.Headers.Add(Consts.RequestChainId, Guid.NewGuid().ToString());
    }
    // Замена заголовка запроса, если это не гуид
    else if (!Guid.TryParse(requestId, out var requestId1))
    {
        context.Request.Headers.Remove(Consts.RequestChainId);
        context.Request.Headers.Add(Consts.RequestChainId, Guid.NewGuid().ToString());
    }

    await next.Invoke();
});

app.UseLoggingMiddleware();

app.Run();
