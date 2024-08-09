using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

public class TempReq
{
    public int Min { get; set; }
    public int Max { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromQuery]int count, [FromBody] TempReq request )
    {
        if (count < 0 || request.Max < request.Min)
        {
            return BadRequest("Count must be greater than zero and max temp cannot be less than min temp");
        }

        var result = _weatherForecastService.Get(count, request.Min, request.Max);
        return Ok(result);
    }
}
