using Microsoft.AspNetCore.Mvc;

namespace AArkhipenko.Logging.Test.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("get1")]
    public IEnumerable<WeatherForecast> Get1()
    {
        using (var scope = _logger.BeginScope("Test scope"))
        using (var scope1 = _logger.BeginScope("Test scope2"))
        {
            _logger.LogInformation("Test message");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    [HttpGet("get2")]
    public IEnumerable<WeatherForecast> Get2()
    {
        using (var scope = _logger.BeginScope("Test scope"))
        {
            _logger.LogInformation("Test message");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
