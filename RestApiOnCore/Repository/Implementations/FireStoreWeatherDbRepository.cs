using RestApiOnCore.Configuration;
using RestApiOnCore.Dto;
using RestApiOnCore.Dto.Api;
using RestApiOnCore.Extensions.Firebase;
using RestApiOnCore.Repository.Instrastucture;

namespace RestApiOnCore.Repository.Implementations;

public class FireStoreWeatherDbRepository : IDbRepository
{
	private FirestoreDbContext.FireStoreCollection _firestore;

	public FireStoreWeatherDbRepository(FirestoreDbContext dbContext)
	{
		_firestore = dbContext.GetCollection("weatherapi");
	}


	public async Task<bool> PutWeather(string query, IEnumerable<WeatherForecastDto> list)
	{
		var tasks = new List<Task>();
		foreach (var apiDto in list)
		{
			tasks.Add(_firestore.AddOrUpdate(apiDto.ToFireBaseDto(), CancellationToken.None));
		}

		await Task.WhenAll(tasks);
		return true;
	}

	public async Task<IEnumerable<WeatherForecastDto>> GetWeather(string query)
	{
		if (query.Equals("*"))
		{
			var allDtos = await _firestore.GetAll<WeatherFirebaseDto>(CancellationToken.None);
			return allDtos.Select(x => x.GetApiDto());
		}

		var result = await _firestore.Get<WeatherFirebaseDto>(query, CancellationToken.None);
		if (result == null)
		{
			return new WeatherForecastDto[0];
		}

		return new List<WeatherForecastDto>() {result.GetApiDto()};
	}

	public async Task<bool> UpdateWeather(string query, IEnumerable<WeatherForecastDto> list)
	{
		var tasks = new List<Task>();
		foreach (var apiDto in list)
		{
			tasks.Add(_firestore.AddOrUpdate(apiDto.ToFireBaseDto(), CancellationToken.None));
		}

		await Task.WhenAll(tasks);
		return true;
	}

	public async Task<bool> DeleteWeather(string query)
	{
		return await _firestore.Delete<WeatherFirebaseDto>(query);
	}
}