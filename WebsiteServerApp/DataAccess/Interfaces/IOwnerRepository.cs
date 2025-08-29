using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.DataAccess.Interfaces;

/// <summary>
/// Interface with data access methods to be executed in the owners collection.
/// </summary>
public interface IOwnerRepository
{
    /// <summary>
    /// Get all the owners in the collection.
    /// </summary>
    /// <returns>A list with all the owners found.</returns>
    Task<List<Owner>> GetAllOwnersAsync();
}
