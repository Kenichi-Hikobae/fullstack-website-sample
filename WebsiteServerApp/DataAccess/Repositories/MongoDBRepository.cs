using MongoDB.Bson;
using MongoDB.Driver;
using WebsiteServerApp.DataAccess.Constants;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models.Base;

namespace WebsiteServerApp.DataAccess.Repositories;

/// <summary>
/// Repository base class that contains the data access methods for operations on the repository collection.
/// </summary>
public abstract class MongoDBRepository<T> : IMongoDBRepository<T>
    where T : BaseModel
{
    /// <summary>
    /// The MongoDB client.
    /// </summary>
    protected MongoClient _client;

    /// <summary>
    /// The MongoDB database in use.
    /// </summary>
    protected IMongoDatabase _database;

    /// <summary>
    /// The MongoDB collection in use.
    /// </summary>
    protected IMongoCollection<T> _collection;

    public MongoDBRepository(IAppSettings appSettings)
    {
        MongoUrl mongoUrl = MongoUrl.Create(appSettings.GetSetting(AppSettingType.MongoDbConnectionString));
        string url = appSettings.GetSetting(AppSettingType.MongoDbConnectionString);

        MongoClientSettings settings = MongoClientSettings.FromUrl(mongoUrl);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(settings);
        _database = _client.GetDatabase(DBConstantFields.DatabasePropertyName);
        try
        {
            BsonDocument result = _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not connect to MongoDB, review connection URL in appsetting.json");
        }
    }

    /// <summary>
    /// Set the collection to be used on this repository.
    /// </summary>
    /// <param name="collectionName">The collection name.</param>
    protected void SetCollection(string collectionName)
    {
        _collection = _database.GetCollection<T>(collectionName);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(ObjectId id)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(DBConstantFields.IdKey, id);
            DeleteResult result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"Failed to delete document with id:{id}.");
        }
    }

    /// <inheritdoc/>
    public async Task<T> GetAsync(ObjectId id)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(DBConstantFields.IdKey, id);
            IAsyncCursor<T> result = await _collection.FindAsync<T>(filter);

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"Failed to get a document with id:{id}.");
        }
    }

    /// <inheritdoc/>
    public async Task<IList<T>> GetAllAsync()
    {
        try
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Failed to get all documents.");
        }
    }

    /// <inheritdoc/>
    public async Task<T> InsertAsync(T document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
            return document;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Failed to insert one document.");
        }
    }

    /// <inheritdoc/>
    public async Task InsertBulkAsync(List<T> documents)
    {
        try
        {
            await _collection.InsertManyAsync(documents);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Failed to insert one document.");
        }
    }

    /// <inheritdoc/>
    public async Task<T> UpdateAsync(ObjectId id, T document)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(DBConstantFields.IdKey, id);
            return await _collection.FindOneAndReplaceAsync(filter, document);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"Failed to update document with id:{id}.");
        }
    }

    /// <inheritdoc/>
    public async Task<long> GetTotalCount()
    {
        try
        {
            return await _collection.CountDocumentsAsync(Builders<T>.Filter.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"Failed to get total count.");
        }
    }
}
