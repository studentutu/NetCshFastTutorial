using RestApiOnCore.Dto;

namespace RestApiOnCore.Repository.Instrastucture;

public interface IDbRepository
{
	Task<bool> PutWeather(string query,List<WeatherForecastDto> list);
	/// <summary>
	/// sql query
	/// </summary>
	/// <param name="query"></param>
	/// <returns></returns>
	Task<List<WeatherForecastDto>> GetWeather(string query);
	Task<bool> UpdateWeather(string query,List<WeatherForecastDto> list);
	Task<bool> DeleteWeather(string query);
}