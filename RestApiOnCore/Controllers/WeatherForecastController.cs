using Microsoft.AspNetCore.Mvc;
using RestApiOnCore.Dto;
using RestApiOnCore.Repository.Instrastucture;

namespace RestApiOnCore.Controllers;

[ApiController]
[Route("[controller]")]
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

	[HttpGet(Name = "GetWeatherForecast")]
	public async Task<IEnumerable<WeatherForecastDto>> Get()
	{
		await Task.Delay(3000);

		return await _dbRepository.GetWeather("*");
	}
}