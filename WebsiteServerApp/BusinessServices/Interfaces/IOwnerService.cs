using WebsiteServerApp.BusinessServices.DTOs;

namespace WebsiteServerApp.BusinessServices.Interfaces;

/// <summary>
/// Interface that contains operations within the owners an its relationships.
/// </summary>
public interface IOwnerService
{
    /// <summary>
    /// Get all the owners in the collection
    /// </summary>
    /// <returns>A list of owners DTOs</returns>
    Task<List<OwnerDTO>> GetAllOwnersAsync();
    /// <summary>
    /// Insert the given owners to the repository.
    /// </summary>
    /// <param name="owners">The owner models to be inserted.</param>
    /// <returns>The task result.</returns>
    Task InsertBulkDataAsync(List<OwnerDTO> owners);
}
