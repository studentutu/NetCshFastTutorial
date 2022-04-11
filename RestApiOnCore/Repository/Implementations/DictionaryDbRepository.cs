using RestApiOnCore.Dto;
using RestApiOnCore.Dto.Api;
using RestApiOnCore.Repository.Instrastucture;

namespace RestApiOnCore.Repository.Implementations;

public class DictionaryDbRepository : IDbRepository
{
	private Dictionary<string, List<WeatherForecastDto>> _dictionary = new();

	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	private static readonly string[] Places = new[]
	{
		"East", "West", "North", "South"
	};

	public DictionaryDbRepository()
	{
		var list = Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
		{
			Guid = Guid.NewGuid().ToString(),
			Place = Places[Random.Shared.Next(Places.Length)],
			Date = DateTime.Now.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		});
		foreach (var item in list)
		{
			EnsureExists(item.Guid);
			_dictionary[item.Guid].Add(item);
		}
	}

	private void EnsureExists(string query)
	{
		if (!_dictionary.ContainsKey(query))
		{
			_dictionary.Add(query, new List<WeatherForecastDto>());
		}
	}

	public Task<bool> PutWeather(string query, IEnumerable<WeatherForecastDto> list)
	{
		EnsureExists(query);
		_dictionary[query].AddRange(list);
		return Task.FromResult(true);
	}

	public Task<IEnumerable<WeatherForecastDto>> GetWeather(string query)
	{
		if (query.Equals("*"))
		{
			var list = new List<WeatherForecastDto>();
			foreach (var keyAndValue in _dictionary)
			{
				list.AddRange(keyAndValue.Value);
			}

			return Task.FromResult(list as IEnumerable<WeatherForecastDto>);
		}

		if (_dictionary.TryGetValue(query, out var listTemp))
		{
			return Task.FromResult(listTemp as IEnumerable<WeatherForecastDto>);
		}

		return Task.FromResult(new List<WeatherForecastDto>() as IEnumerable<WeatherForecastDto>);
	}

	public Task<bool> UpdateWeather(string query, IEnumerable<WeatherForecastDto> listToUpdate)
	{
		EnsureExists(query);
		var toDict = _dictionary[query].ToDictionary(x => x.Guid);
		foreach (var weatherItem in listToUpdate)
		{
			toDict[weatherItem.Guid] = weatherItem;
		}

		_dictionary[query].Clear();
		_dictionary[query].AddRange(toDict.Values);
		return Task.FromResult(true);
	}

	public Task<bool> DeleteWeather(string query)
	{
		if (query.Equals("*"))
		{
			_dictionary.Clear();
			return Task.FromResult(true);
		}

		return Task.FromResult(_dictionary.Remove(query));
	}
}