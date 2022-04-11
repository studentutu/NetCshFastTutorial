using Google.Cloud.Firestore;
using MediatR;
using RestApiOnCore.Configuration;
using RestApiOnCore.Repository.Implementations;
using RestApiOnCore.Repository.Instrastucture;
using RestApiOnCore.Services.Implementations;
using RestApiOnCore.Services.Instrastucture;

namespace RestApiOnCore.Services;

public class ServicesInstaller
{
	private readonly IServiceCollection _serviceCollection;
	private readonly IWebHostEnvironment _hostingEnvironment;

	public ServicesInstaller(
		IServiceCollection serviceCollection,
		IWebHostEnvironment hostingEnvironment)
	{
		_serviceCollection = serviceCollection;
		_hostingEnvironment = hostingEnvironment;
	}

	public void AddServices()
	{
		//Mediatr
		_serviceCollection.AddMediatR(typeof(Program.ResAPIOnCore).Assembly);

		_serviceCollection.AddSingleton<IDateTimeService>(new DateTimeService());

		_serviceCollection.AddFireStoreCustomSettings(_hostingEnvironment);

		_serviceCollection.AddSingleton<IDbRepository, FireStoreWeatherDbRepository>();
	}
}