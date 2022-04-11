namespace RestApiOnCore.Configuration;

public class SetupEnvironments
{
	public const string FireStore_Project_Id = "";
	public const string GOOGLE_FIRESTORE = "GOOGLE_APPLICATION_CREDENTIALS";

	public static void SetGoogleFirestoreServiceAccount(string filePath)
	{
		Environment.SetEnvironmentVariable(GOOGLE_FIRESTORE, filePath);
	}

	public static string GetGoogleFireStoreServiceAccount()
	{
		return Environment.GetEnvironmentVariable(GOOGLE_FIRESTORE);
	}
}