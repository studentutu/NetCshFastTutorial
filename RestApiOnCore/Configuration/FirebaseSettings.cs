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
			var dictionary = new Dictionary<string, Object>();
			if (value == null)
			{
				return dictionary;
			}

			var nameOfField = nameof(WeatherFirebaseDto._apiDto);
			var asDictionary = JsonConvert.DeserializeObject<Dictionary<string, Object>>(
				JsonConvert.SerializeObject(value._apiDto));
			dictionary.Add(nameOfField, asDictionary);
			return dictionary;
		}

		public WeatherFirebaseDto? FromFirestore(object value)
		{
			var dictionary = (Dictionary<string, object>) value;
			if (dictionary.Keys.Count == 0)
			{
				return null;
			}

			var nameOfField = nameof(WeatherFirebaseDto._apiDto);

			var list = dictionary[nameOfField];
			var asDict = list as Dictionary<string, object>;
			var asDateButFrom = asDict[nameof(WeatherForecastDto.Date)];
			var firebaseDeserializer = new DefaultNullableDatetimeConverter();
			asDict[nameof(WeatherForecastDto.Date)] = firebaseDeserializer.FromFirestore(asDateButFrom);
			var toJson = JsonConvert.SerializeObject(asDict);
			var deserialized = JsonConvert.DeserializeObject<WeatherForecastDto>(toJson);
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