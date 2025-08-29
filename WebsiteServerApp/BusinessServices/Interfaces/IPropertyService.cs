using WebsiteServerApp.BusinessServices.DTOs;

namespace WebsiteServerApp.BusinessServices.Interfaces;

/// <summary>
/// Interface that contains operations within the properties an its relationships.
/// </summary>
public interface IPropertyService
{
    /// <summary>
    /// Get all the properties given the owner Id.
    /// </summary>
    /// <param name="ownerId">The owner id used to filter the properties.</param>
    /// <returns>A list with all the properties DTOs.</returns>
    Task<List<PropertyDTO>> GetPropertiesByOwnerAsync(string ownerId);
    /// <summary>
    /// Get all the properties in the collection.
    /// </summary>
    /// <returns>A list with all the properties DTOs.</returns>
    Task<List<PropertyDTO>> GetAllPropertiesAsync();
    /// <summary>
    /// Filter the properties by the given parameters.
    /// </summary>
    /// <param name="filters">The filters to be applied in the collection.</param>
    /// <returns>A list with all the properties DTOs.</returns>
    Task<List<PropertyDTO>> FilterPropertiesAsync(PropertyFiltersDTO filters);
    /// <summary>
    /// Get the traces from a property.
    /// </summary>
    /// <param name="propertyId">The property id to be search.</param>
    /// <returns>A list with all the properties traces DTOs.</returns>
    Task<List<PropertyTraceDTO>> GetTracesByPropertyAsync(string propertyId);
}
