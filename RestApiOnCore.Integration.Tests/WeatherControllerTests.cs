using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RestApiOnCore.Controllers;
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
		var awaitedResult = await HttpClient.GetAsync(ApiRoutes.WeatherApi.relativeLink);
		awaitedResult.IsSuccessStatusCode.Should().BeTrue();
	}
}