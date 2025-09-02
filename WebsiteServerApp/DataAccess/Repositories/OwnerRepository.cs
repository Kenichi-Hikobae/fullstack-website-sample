using MongoDB.Driver;
using WebsiteServerApp.DataAccess.Constants;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.DataAccess.Repositories;

/// <summary>
/// Contains data access methods to be executed in the owner collection.
/// </summary>
public class OwnerRepository : MongoDBRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(IAppSettings appSettings)
        : base(appSettings)
    {
        SetCollection(DBConstantFields.CollectionOwnerName);
    }

    /// <inheritdoc/>
    public async Task<List<Owner>> GetAllOwnersAsync()
    {
        IList<Owner> result = await GetAllAsync();

        return result.ToList();
    }
}
