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
    /// Get the properties in the collection.
    /// </summary>
    /// <param name="filters">The filters to be applied in the collection.</param>
    /// <returns>A model containing the list of properties and the total amount of documents.</returns>
    Task<PropertiesLoadedCountDTO> GetPropertiesAsync(PropertyFiltersDTO filters);
    /// <summary>
    /// Get the traces from a property.
    /// </summary>
    /// <param name="propertyId">The property id to be search.</param>
    /// <returns>A list with all the properties traces DTOs.</returns>
    Task<List<PropertyTraceDTO>> GetTracesByPropertyAsync(string propertyId);
    /// <summary>
    /// Insert many document to the repository.
    /// </summary>
    /// <param name="properties">The properties to be inserted.</param>
    /// <returns>The task result.</returns>
    Task InsertBulkDataAsync(List<PropertyDTO> properties);
    /// <summary>
    /// Get the count for all the properties in the collection.
    /// </summary>
    /// <returns></returns>
    Task<long> GetPropertiesCount();
    /// <summary>
    /// Get the property by the given Id.
    /// </summary>
    /// <param name="id">the id of the property to be searched..</param>
    /// <returns>The model found with the given id.</returns>
    Task<PropertyDTO> GetPropertyByIdAsync(string id);
}
