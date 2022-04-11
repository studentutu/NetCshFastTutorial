namespace RestApiOnCore.Dto.Api;

[Serializable]
public class WeatherForecastDto
{
	public string Guid { get; set; }
	public string Place { get; set; }
	public DateTime Date { get; set; }

	public int TemperatureC { get; set; }

	public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

	public string? Summary { get; set; }
}