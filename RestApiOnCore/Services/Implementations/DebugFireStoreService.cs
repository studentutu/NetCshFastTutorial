using RestApiOnCore.Configuration;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RestApiOnCore.Services.Implementations;

public class DebugFireStoreService
{
	public static void Initialize(IHostingEnvironment hostingEnvironment)
	{
		var fullPathToWebRoot = hostingEnvironment.WebRootPath;
		
		string filePathToKeys = Path.Combine(fullPathToWebRoot, "Keys");
		string filePathToGoogleKey = Path.Combine(filePathToKeys, "crudfirebase-5b9bb-201d806f863f.json");
		SetupEnvironments.SetGoogleFirestoreServiceAccount(filePathToGoogleKey);
	}
}