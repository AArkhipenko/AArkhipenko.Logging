# AArkhipenko.Logging

Nuget-проект с настройками логирования сервисов

В лог пишуются записи формата:
```json
{
  "Timestamp": "2025-04-12T12:57:53.737355+05:00",
  "RequestId": "0cfcebf8-12a8-4321-ae67-11bd4ceea6d0",
  "LogLevel": "Information",
  "Method": "\"GET\"",
  "Path": "\"/weatherforecast\"",
  "Scope": "WeatherForecastController.Get -> WeatherForecastController.GetArray",
  "Message": "Завершение логирования раздела"
}
```


## Методы расширения

Все методы расширения находятся [здесь](./AArkhipenko.Logging/LoggingExtension.cs)

### Логирование в консоль

Добавление возможности логирования в консоль

Подключение:
```C#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Добавление логирования в консоль
builder.Logging.AddConsoleLogging();
...
var app = builder.Build();
...
app.MapControllers();

app.Run();
```

### Логирование в файл

Добавление возможности логирования в файл stdout

Подключение:
```C#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Добавление логирования в файл
builder.Logging.AddFileLogging();
...
var app = builder.Build();
...
app.MapControllers();

app.Run();
```

### Обогащение логирования

Каждая запись в лог обогащается информацией из запроса:
* ИД запроса из загловка **Request-Chain-Id**
* Тип запроса из самого запроса
* АПИ из запроса

Подключение:
```C#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
...
var app = builder.Build();
// Использование прослойки, которая обогащает логирование доп информацией
app.UseLoggingMiddleware();
...
app.MapControllers();

app.Run();
```