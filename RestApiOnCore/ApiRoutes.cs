namespace RestApiOnCore;

public class ApiRoutes
{
	public class WeatherApi
	{
		internal const string RelativeUrl = "weatherforecast";

		public static string WeatherUrl
		{
			get { return "/" + RelativeUrl; }
		}
	}
}