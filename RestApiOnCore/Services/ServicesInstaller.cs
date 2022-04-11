﻿using Google.Cloud.Firestore;
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
		
		
		_serviceCollection.AddSingleton(_ => new FirestoreDbContext(
			new FirestoreDbBuilder 
			{ 
				ProjectId = SetupEnvironments.FireStore_Project_Id, 
				JsonCredentials = SetupEnvironments.GetGoogleFireStoreServiceAccount() // <-- service account json file
			}.Build()
		));


	}
}