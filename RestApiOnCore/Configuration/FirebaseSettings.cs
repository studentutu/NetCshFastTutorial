namespace RestApiOnCore.Configuration;

public class FirebaseSettings
{
	public static void Initialize(IWebHostEnvironment hostingEnvironment)
	{
		var fullPathToWebRoot = hostingEnvironment.WebRootPath;
		
		string filePathToKeys = Path.Combine(fullPathToWebRoot, "Keys");
		string filePathToGoogleKey = Path.Combine(filePathToKeys, "crudfirebase-5b9bb-201d806f863f.json");
		SetupEnvironments.SetGoogleFirestoreServiceAccount(filePathToGoogleKey);
	}
}