using Microsoft.AspNetCore.Mvc;
using RestApiOnCore.Dto;
using RestApiOnCore.Repository.Instrastucture;

namespace RestApiOnCore.Controllers;

[ApiController]
[Route(ApiRoutes.WeatherApi.RelativeUrl)]
public class WeatherForecastController : ControllerBase
{
	private readonly ILogger<WeatherForecastController> _logger;
	private readonly IDbRepository _dbRepository;

	public WeatherForecastController(ILogger<WeatherForecastController> logger,
		IDbRepository dbRepository)
	{
		_logger = logger;
		_dbRepository = dbRepository;
	}

	// Get /WeatherForecast
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		await Task.Delay(3000);
		var result = await _dbRepository.GetWeather("*");
		if (result.Any())
		{
			// See https://exceptionnotfound.net/asp-net-core-demystified-action-results/
			return Ok(result);
		}

		return NotFound();
	}

	// Get /WeatherForecast/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(string id)
	{
		await Task.Delay(500);
		var result = await _dbRepository.GetWeather(id);
		if (result.Any())
		{
			return Ok(result);
		}

		return NotFound();
	}
}