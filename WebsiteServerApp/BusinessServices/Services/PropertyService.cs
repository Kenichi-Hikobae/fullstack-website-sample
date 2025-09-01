using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.DTOs.Base;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;
using WebsiteServerApp.DataAccess.Repositories;

namespace WebsiteServerApp.BusinessServices.Services;

/// <summary>
/// Service that implements operations within the properties an its relationships.
/// </summary>
public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private PropertyRepository _repository => _propertyRepository as PropertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    /// <inheritdoc/>
    public async Task<PropertiesLoadedCountDTO> GetPropertiesAsync(PropertyFiltersDTO filters)
    {
        FilterDefinition<Property> filter = Builders<Property>.Filter.And(
            Builders<Property>.Filter.Regex(property => property.Name, filters.Name),
            Builders<Property>.Filter.Regex(property => property.Address, filters.Address),
            Builders<Property>.Filter.Gte(property => property.Price, filters.MinPrice),
            Builders<Property>.Filter.Lte(property => property.Price, filters.MaxPrice),
            GenerateYearFilter(filters.YearFilterType),
            filters.Types?.Count <= 0 ? 
                FilterDefinition<Property>.Empty :
                Builders<Property>.Filter.In(property => property.PropertyType, filters.Types)
        );

        List<Property> properties = await _propertyRepository
            .GetPropertiesByFiltersAsync(filter, filters.Pagination.PageNumber, filters.Pagination.PageSize);

        long filterCount = await _propertyRepository.GetCountByFiltersAsync(filter);

        List<PropertyDTO> result = properties.Select(property => property.ToDTO(true)).ToList();

        return new PropertiesLoadedCountDTO(result, filterCount);
    }

    private FilterDefinition<Property> GenerateYearFilter(PropertyYearFilterType? yearFilterType)
    {
        if (yearFilterType == null)
            return FilterDefinition<Property>.Empty;

        switch (yearFilterType)
        {
            case PropertyYearFilterType.MoreThan20:
                return Builders<Property>.Filter.Lt(property => property.Year, DateTime.Now.Year - 20);
            case PropertyYearFilterType.LessThan5:
                return Builders<Property>.Filter.Gte(property => property.Year,DateTime.Now.Year - 5);
            case PropertyYearFilterType.LessThan10:
                return Builders<Property>.Filter.Gte(property => property.Year, DateTime.Now.Year - 10);
            case PropertyYearFilterType.LessThan20:
                return Builders<Property>.Filter.Gte(property => property.Year, DateTime.Now.Year - 20);
            default: 
                return FilterDefinition<Property>.Empty;
        }
    }

    /// <inheritdoc/>
    public async Task<List<PropertyDTO>> GetPropertiesByOwnerAsync(string ownerId)
    {
        List<Property> properties = await _propertyRepository.GetPropertiesByOwnerIdAsync(new ObjectId(ownerId));

        List<PropertyDTO> result = properties.Select(property => property.ToDTO(true)).ToList();

        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PropertyTraceDTO>> GetTracesByPropertyAsync(string propertyId)
    {
        List<PropertyTrace> traces = await _propertyRepository.GetTracesByPropertyIdAsync(new ObjectId(propertyId));

        List<PropertyTraceDTO> result = traces.Select(trace => trace.ToDTO()).ToList();

        return result;
    }

    /// <inheritdoc/>
    public async Task InsertBulkDataAsync(List<PropertyDTO> properties)
    {
        List<Property> propertyModels = properties.Select(property => property.ToDatabase(true)).ToList();

        var serialize = JsonConvert.SerializeObject(propertyModels);
        File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/DataAccess/Data/properties.json", serialize);

        await _repository.InsertBulkAsync(propertyModels);
    }

    /// <inheritdoc/>
    public async Task<long> GetPropertiesCount()
    {
        return await _repository.GetTotalCount();
    }

    /// <inheritdoc/>
    public async Task<PropertyDTO> GetPropertyByIdAsync(string id)
    {
        var result = await _repository.GetAsync(new ObjectId(id));

        return result.ToDTO(true);
    }
}
