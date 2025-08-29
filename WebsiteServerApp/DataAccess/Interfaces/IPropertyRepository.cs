using MongoDB.Bson;
using MongoDB.Driver;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.DataAccess.Interfaces;

/// <summary>
/// Interface with data access methods to be executed in the properties collection.
/// </summary>
public interface IPropertyRepository
{
    /// <summary>
    /// Get all the properties given the owner Id.
    /// </summary>
    /// <param name="ownerId">The owner id used to filter the properties.</param>
    /// <returns>A list with all the properties found.</returns>
    Task<List<Property>> GetPropertiesByOwnerIdAsync(ObjectId ownerId);
    /// <summary>
    /// Get all the properties in the collection.
    /// </summary>
    /// <returns>A list with all the properties found.</returns>
    Task<List<Property>> GetAllPropertiesAsync();
    /// <summary>
    /// Get all the properties with a given filter.
    /// </summary>
    /// <param name="filters">The filters to be applied in the collection.</param>
    /// <returns>A list with all the properties found.</returns>
    Task<List<Property>> GetPropertiesByFiltersAsync(FilterDefinition<Property> filters);
    /// <summary>
    /// Get the traces from a property.
    /// </summary>
    /// <param name="propertyId">The property id to be search.</param>
    /// <returns>A list with all the properties traces found.</returns>
    Task<List<PropertyTrace>> GetTracesByPropertyIdAsync(ObjectId propertyId);
}
