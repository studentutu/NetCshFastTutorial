using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestApiOnCore.Controllers;
using RestApiOnCore.Dto.Api;
using RestApiOnCore.Repository;

namespace RestApiOnCore.Integration.Tests;

[TestFixture]
public class WeatherControllerTests : TestBase
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public async Task WeatherApi_ShouldReturn_GoodResult()
	{
		var awaitedResult = await HttpClient.GetAsync(ApiRoutes.WeatherApi.WeatherUrl);
		awaitedResult.IsSuccessStatusCode.Should().BeTrue();
	}

	[Test]
	public async Task WeatherApiAddNew_GoodResult()
	{
		var newWeather = new WeatherForecastDto()
		{
			Guid = "31eec0cd-593a-4fe1-922c-be072d821582",
			Place = "West",
			Date = DateTime.Now.AddDays(3),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = "Rainy"
		};
		var stringContent = new StringContent(
			JsonConvert.SerializeObject(newWeather),
			Encoding.Default,
			"application/json");


		var awaitedResult = await HttpClient.PostAsync(ApiRoutes.WeatherApi.WeatherUrl, stringContent);
		awaitedResult.IsSuccessStatusCode.Should().BeTrue();
	}

	[Test]
	public async Task WeatherApi_GetReturn_GoodResult()
	{
		var awaitedResult =
			await HttpClient.GetAsync(ApiRoutes.WeatherApi.WeatherUrl + "/31eec0cd-593a-4fe1-922c-be072d821582");
		awaitedResult.IsSuccessStatusCode.Should().BeTrue();
	}
}