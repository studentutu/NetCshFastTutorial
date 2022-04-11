using Google.Cloud.Firestore;
using RestApiOnCore.Dto;

namespace RestApiOnCore.Configuration;

/// <summary>
/// https://dev.to/kedzior_io/simple-net-core-and-cloud-firestore-setup-1pf9
/// </summary>
public class FirestoreDbContext
{
	private readonly FirestoreDb _fireStoreDb;
	private const string DbName = "root";

	public FirestoreDbContext(FirestoreDb fireStoreDb)
	{
		_fireStoreDb = fireStoreDb;
	}

	public FireStoreCollection GetCollection(string collection)
	{
		return new FireStoreCollection(_fireStoreDb.Collection(collection));
	}

	public class FireStoreCollection
	{
		private readonly CollectionReference _collection;

		public FireStoreCollection(CollectionReference collection)
		{
			_collection = collection;
		}

		public async Task<bool> AddOrUpdate<T>(T entity, CancellationToken ct) where T : class, IFirebaseEntity
		{
			var document = _collection.Document(entity.Id);
			var result = await document.SetAsync(entity, cancellationToken: ct, options: SetOptions.MergeAll);
			return result != null;
		}

		public async Task<T> Get<T>(string id, CancellationToken ct) where T : class, IFirebaseEntity
		{
			var document = _collection.Document(id);
			var snapshot = await document.GetSnapshotAsync(ct);
			if (snapshot.Exists)
			{
				return snapshot.ConvertTo<T>();
			}

			return null;
		}

		public async Task<bool> Delete<T>(string id)
		{
			var document = _collection.Document(id);
			var result = await document.DeleteAsync(Precondition.None);

			return result != null && result.UpdateTime.ToDateTime() != default;
		}

		public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : IFirebaseEntity
		{
			return await GetList<T>(_collection, ct);
		}

		public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct)
			where T : IFirebaseEntity
		{
			return await GetList<T>(_collection.WhereEqualTo(fieldPath, value), ct);
		}

		// just add here any method you need here WhereGreaterThan, WhereIn etc ...

		private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct)
			where T : IFirebaseEntity
		{
			var snapshot = await query.GetSnapshotAsync(ct);
			return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
		}
	}
}