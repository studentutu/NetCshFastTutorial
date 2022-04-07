using RestApiOnCore.Services.Instrastucture;

namespace RestApiOnCore.Services.Implementations;

public class DateTimeService : IDateTimeService
{
	public DateTime GetDateTimeNow()
	{
		return DateTime.UtcNow;
	}
}