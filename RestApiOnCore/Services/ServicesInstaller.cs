using MediatR;
using RestApiOnCore.Repository.Implementations;
using RestApiOnCore.Repository.Instrastucture;
using RestApiOnCore.Services.Implementations;
using RestApiOnCore.Services.Instrastucture;

namespace RestApiOnCore.Services;

public class ServicesInstaller
{
	private readonly IServiceCollection _serviceCollection;

	public ServicesInstaller(IServiceCollection serviceCollection)
	{
		_serviceCollection = serviceCollection;
	}

	public void AddServices()
	{
		//Mediatr
		_serviceCollection.AddMediatR(typeof(Program.ResAPIOnCore).Assembly);

		_serviceCollection.AddSingleton<IDateTimeService>(new DateTimeService());

		_serviceCollection.AddSingleton<IDbRepository>(new DictionaryDbRepository());
	}
}