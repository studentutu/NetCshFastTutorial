using Microsoft.AspNetCore.Mvc;
using RestApiOnCore.Dto;
using RestApiOnCore.Repository.Instrastucture;
using RestApiOnCore.Services.Instrastucture;

namespace RestApiOnCore.Controllers;

[ApiController]
[Route(ApiRoutes.WeatherApi.RelativeUrl)]
public class WeatherForecastController : ControllerBase
{
	private readonly ILogger<WeatherForecastController> _logger;
	private readonly IDbRepository _dbRepository;
	private readonly IDateTimeService _dateTimeService;

	public WeatherForecastController(ILogger<WeatherForecastController> logger,
		IDbRepository dbRepository,
		IDateTimeService dateTimeService)
	{
		_logger = logger;
		_dbRepository = dbRepository;
		_dateTimeService = dateTimeService;
	}

	// Get /WeatherForecast
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		await Task.Delay(300);
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

	// Get /WeatherForecast/{id}
	[HttpPost]
	public async Task<IActionResult> CreateNewWeatherDto(WeatherForecastDto newWeatherDto)
	{
		await Task.Delay(500);
		newWeatherDto.Date = _dateTimeService.GetDateTimeNow();
		var result =
			await _dbRepository.PutWeather(newWeatherDto.Place, new List<WeatherForecastDto>() {newWeatherDto});
		if (result)
		{
			return Ok(newWeatherDto);
		}

		return BadRequest();
	}

	// Get /WeatherForecast/{id}
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteWeatherDto(string id)
	{
		await Task.Delay(500);
		var result = await _dbRepository.DeleteWeather(id);
		if (result)
		{
			return Ok(new ApiResultDto() {success = true, message = $"Removed {id}"});
		}

		return BadRequest();
	}
}