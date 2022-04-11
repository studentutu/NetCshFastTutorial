using RestApiOnCore.Dto;
using RestApiOnCore.Dto.Api;

namespace RestApiOnCore.Extensions.Firebase;

public static class FirebaseExtensions
{
	public static WeatherFirebaseDto ToFireBaseDto(this WeatherForecastDto apiDto)
	{
		return new WeatherFirebaseDto(apiDto);
	}
}