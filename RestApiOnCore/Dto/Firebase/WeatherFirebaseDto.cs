using RestApiOnCore.Dto.Api;

namespace RestApiOnCore.Dto;

public class WeatherFirebaseDto : IFirebaseEntity
{
	public string Id
	{
		get => _apiDto.Guid;
		set => _apiDto.Guid = value;
	}

	private WeatherForecastDto _apiDto;

	public WeatherFirebaseDto()
	{
	}

	public WeatherFirebaseDto(WeatherForecastDto fromApiDto)
	{
		_apiDto = fromApiDto;
	}

	public WeatherForecastDto GetApiDto()
	{
		return _apiDto;
	}
}