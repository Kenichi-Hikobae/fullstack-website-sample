using MongoDB.Bson;
using MongoDB.Driver;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.DataAccess.Constants;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.DataAccess.Repositories;

/// <summary>
/// Contains data access methods to be executed in the properties collection.
/// </summary>
public class PropertyRepository : MongoDBRepository<Property>, IPropertyRepository
{
    public PropertyRepository(IAppSettings appSettings)
        : base(appSettings)
    {
        SetCollection(DBConstantFields.CollectionPropertyName);
        InitializeIndexes();
    }

    /// <summary>
    /// Initialize the indexes for this collection.
    /// </summary>
    private void InitializeIndexes()
    {
        Dictionary<string, IndexKeysDefinition<BsonDocument>> indexes = new()
        {
            { 
                "_ownerId_index",
                Builders<BsonDocument>.IndexKeys.Ascending(DBConstantFields.OwnerKey)
            },
            {
                "traces._id_index",
                Builders<BsonDocument>.IndexKeys.Ascending($"{DBConstantFields.PropertyTracesName}.{DBConstantFields.IdKey}")
            },
            {
                "traces._propertyId_index",
                Builders<BsonDocument>.IndexKeys.Ascending($"{DBConstantFields.PropertyTracesName}.{DBConstantFields.PropertyKey}")
            },
            {
                "images._propertyId_index",
                Builders<BsonDocument>.IndexKeys.Ascending($"{DBConstantFields.PropertyImageName}.{DBConstantFields.IdKey}")
            },
            {
                "images._id_index",
                Builders<BsonDocument>.IndexKeys.Ascending($"{DBConstantFields.PropertyImageName}.{DBConstantFields.PropertyKey}")
            },
        };

        var nameIndex = new CreateIndexModel<OwnerDTO>(
            Builders<OwnerDTO>.IndexKeys.Ascending(o => o.Name)
        );

        var collection = _database.GetCollection<BsonDocument>(DBConstantFields.CollectionPropertyName);
        var existingIndexes = collection.Indexes.List().ToList();

        foreach (KeyValuePair<string, IndexKeysDefinition<BsonDocument>> index in indexes)
        {
            if (!existingIndexes.Any(existingIndex => existingIndex["name"] == index.Key))
            {
                var indexOptions = new CreateIndexOptions { Name = index.Key };
                collection.Indexes.CreateOne(new CreateIndexModel<BsonDocument>(index.Value, indexOptions));
            }
        }
    }

    /// <inheritdoc/>
    public async Task<List<Property>> GetAllPropertiesAsync()
    {
        IList<Property> result = await GetAllAsync();

        return result.ToList();
    }

    /// <inheritdoc/>
    public async Task<List<Property>> GetPropertiesByFiltersAsync(FilterDefinition<Property> filters, int pageNumber, int pageSize)
    {
        List<Property> results = await _collection
            .Find(filters)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return results;
    }

    /// <inheritdoc/>
    public async Task<long> GetCountByFiltersAsync(FilterDefinition<Property> filters)
    {
        return await _collection
            .Find(filters)
            .CountDocumentsAsync();
    }

    /// <inheritdoc/>
    public async Task<List<Property>> GetPropertiesByOwnerIdAsync(ObjectId ownerId)
    {
        FilterDefinition<Property> filter = Builders<Property>.Filter.And(
            Builders<Property>.Filter.Eq(p => p.OwnerId, ownerId)
        );

        List<Property> results = await _collection.Find(filter).ToListAsync();

        return results;
    }

    /// <inheritdoc/>
    public async Task<List<PropertyTrace>> GetTracesByPropertyIdAsync(ObjectId propertyId)
    {
        FilterDefinition<Property> filter = Builders<Property>.Filter.ElemMatch(x => x.PropertyTraces,
            Builders<PropertyTrace>.Filter.Eq(p => p.PropertyId, propertyId)
        );

        Property result = await GetAsync(propertyId);

        return result.PropertyTraces;
    }
}
