using AArkhipenko.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Logging.AddLoggingMiddleware();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    var requestIdName = "RequestId";
    // ���������� � ��������� ������� RequestId, ���� ��� ���
    if (!context.Request.Headers.TryGetValue(requestIdName, out var requestId))
    {
        context.Request.Headers.Add(requestIdName, Guid.NewGuid().ToString());
    }
    // ������ ��������� �������, ���� ��� �� ����
    else if (!Guid.TryParse(requestId, out var requestId1))
    {
        context.Request.Headers.Remove(requestIdName);
        context.Request.Headers.Add(requestIdName, Guid.NewGuid().ToString());
    }

    await next.Invoke();
});

app.UseLoggingMiddleware();

app.Run();
