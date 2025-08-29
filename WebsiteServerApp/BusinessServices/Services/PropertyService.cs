using MongoDB.Bson;
using MongoDB.Driver;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.DTOs.Base;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.BusinessServices.Services;

/// <summary>
/// Service that implements operations within the properties an its relationships.
/// </summary>
public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    /// <inheritdoc/>
    public async Task<List<PropertyDTO>> GetAllPropertiesAsync()
    {
        List<Property> properties = await _propertyRepository.GetAllPropertiesAsync();

        List<PropertyDTO> result = properties.Select(property => property.ToDTO()).ToList();

        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PropertyDTO>> FilterPropertiesAsync(PropertyFiltersDTO filters)
    {
        FilterDefinition<Property> filter = Builders<Property>.Filter.And(
            Builders<Property>.Filter.Regex(property => property.Name, filters.Name),
            Builders<Property>.Filter.Gte(property => property.Price, filters.MinPrice),
            Builders<Property>.Filter.Lte(property => property.Price, filters.MaxPrice)
        );

        List<Property> properties = await _propertyRepository.GetPropertiesByFiltersAsync(filter);

        List<PropertyDTO> result = properties.Select(property => property.ToDTO()).ToList();

        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PropertyDTO>> GetPropertiesByOwnerAsync(string ownerId)
    {
        List<Property> properties = await _propertyRepository.GetPropertiesByOwnerIdAsync(new ObjectId(ownerId));

        List<PropertyDTO> result = properties.Select(property => property.ToDTO()).ToList();

        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PropertyTraceDTO>> GetTracesByPropertyAsync(string propertyId)
    {
        List<PropertyTrace> traces = await _propertyRepository.GetTracesByPropertyIdAsync(new ObjectId(propertyId));

        List<PropertyTraceDTO> result = traces.Select(trace => trace.ToDTO()).ToList();

        return result;
    }
}
