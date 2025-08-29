using MongoDB.Bson;
using MongoDB.Driver;
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
    }

    /// <inheritdoc/>
    public async Task<List<Property>> GetAllPropertiesAsync()
    {
        IList<Property> result = await GetAllAsync();

        return result.ToList();
    }

    /// <inheritdoc/>
    public async Task<List<Property>> GetPropertiesByFiltersAsync(FilterDefinition<Property> filters)
    {
        List<Property> results = await _collection.Find(filters).ToListAsync();

        return results;
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
