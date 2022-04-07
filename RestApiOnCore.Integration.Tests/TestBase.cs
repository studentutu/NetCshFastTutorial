using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RestApiOnCore.Integration.Tests;

public class TestBase
{
	protected readonly HttpClient HttpClient;

	// private 
	public TestBase()
	{
		var builder = new WebApplicationFactory<Program>();
		HttpClient = builder.CreateClient();
	}
}