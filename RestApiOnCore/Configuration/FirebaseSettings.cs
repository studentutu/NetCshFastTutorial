using Google.Cloud.Firestore;
using Newtonsoft.Json;
using RestApiOnCore.Dto;
using RestApiOnCore.Dto.Api;

namespace RestApiOnCore.Configuration;

public static class FirebaseSettings
{
	public static void AddFireStoreCustomSettings(this IServiceCollection serviceCollection,
		IWebHostEnvironment hostingEnvironment)
	{
		Initialize(hostingEnvironment);
		var FireStoreBuilder =
			new FirestoreDbBuilder
			{
				ProjectId = SetupEnvironments.FireStore_Project_Id,
				JsonCredentials = SetupEnvironments.GetFireStoreServiceAsJson() // <-- service account json file
			};
		FireStoreBuilder.ConverterRegistry = new ConverterRegistry();
		FireStoreBuilder.ConverterRegistry.Add(new DefaultNullableDatetimeConverter());
		FireStoreBuilder.ConverterRegistry.Add(new WeatherFirebaseDtoConverter());

		var firestorDb = FireStoreBuilder.Build();
		serviceCollection.AddSingleton<FirestoreDbContext>(_ => new FirestoreDbContext(firestorDb));
	}


	internal class DefaultNullableDatetimeConverter : IFirestoreConverter<DateTime?>
	{
		public object ToFirestore(DateTime? value)
			=> value.HasValue ? Timestamp.FromDateTime(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)) : null;

		public DateTime? FromFirestore(object value)
			=> value is null ? null : ((Timestamp) value).ToDateTime();
	}

	internal class WeatherFirebaseDtoConverter : IFirestoreConverter<WeatherFirebaseDto?>
	{
		public object ToFirestore(WeatherFirebaseDto? value)
		{
			var dictionary = new Dictionary<string, string>();
			if (value == null)
			{
				return dictionary;
			}

			dictionary.Add(nameof(value._apiDto), JsonConvert.SerializeObject(value._apiDto));
			return dictionary;
		}

		public WeatherFirebaseDto? FromFirestore(object value)
		{
			var dictionary = (Dictionary<string, object>) value;
			if (dictionary.Keys.Count == 0)
			{
				return null;
			}

			var list = dictionary.Values.First();
			var asString = list as string;
			var deserialized = JsonConvert.DeserializeObject<WeatherForecastDto>(asString);
			return new WeatherFirebaseDto(deserialized);
		}
	}

	private static void Initialize(IWebHostEnvironment hostingEnvironment)
	{
		var fullPathToWebRoot = hostingEnvironment.WebRootPath;

		string filePathToKeys = Path.Combine(fullPathToWebRoot, "Keys");
		string filePathToGoogleKey = Path.Combine(filePathToKeys, "crudfirebase-5b9bb-201d806f863f.json");
		SetupEnvironments.SetGoogleFirestoreServiceAccount(filePathToGoogleKey);
	}
}