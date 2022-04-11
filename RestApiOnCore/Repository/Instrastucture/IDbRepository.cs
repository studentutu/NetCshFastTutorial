using RestApiOnCore.Dto;
using RestApiOnCore.Dto.Api;

namespace RestApiOnCore.Repository.Instrastucture;

public interface IDbRepository
{
	Task<bool> PutWeather(string query,IEnumerable<WeatherForecastDto> list);
	/// <summary>
	/// sql query
	/// </summary>
	/// <param name="query"></param>
	/// <returns></returns>
	Task<IEnumerable<WeatherForecastDto>> GetWeather(string query);
	Task<bool> UpdateWeather(string query,IEnumerable<WeatherForecastDto> list);
	Task<bool> DeleteWeather(string query);
}