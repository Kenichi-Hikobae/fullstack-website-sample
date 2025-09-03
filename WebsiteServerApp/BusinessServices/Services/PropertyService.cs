using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.BusinessServices.MapperProfiles;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;
using WebsiteServerApp.DataAccess.Repositories;

namespace WebsiteServerApp.BusinessServices.Services;

/// <summary>
/// Service that implements operations within the properties an its relationships.
/// </summary>
public class PropertyService : IPropertyService
{
    private readonly IMapper _mapper;
    private readonly IPropertyRepository _propertyRepository;
    private PropertyRepository _repository => _propertyRepository as PropertyRepository;

    public PropertyService(IMapper mapper, IPropertyRepository propertyRepository)
    {
        _mapper = mapper;
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

        /// Used the parameter FullConversion to execute a custom mapping in some properties.
        return new PropertiesLoadedCountDTO(
            _mapper.Map<List<PropertyDTO>>(
                properties,
                opts => opts.Items[ProfileConstants.FullConversionOptName] = true
            ),
            filterCount
        );
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
                return Builders<Property>.Filter.Gte(property => property.Year, DateTime.Now.Year - 5);
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

        return _mapper.Map<List<PropertyDTO>>(
            properties,
            opts => opts.Items[ProfileConstants.FullConversionOptName] = true
        );
    }

    /// <inheritdoc/>
    public async Task<List<PropertyTraceDTO>> GetTracesByPropertyAsync(string propertyId)
    {
        List<PropertyTrace> traces = await _propertyRepository.GetTracesByPropertyIdAsync(new ObjectId(propertyId));

        return _mapper.Map<List<PropertyTraceDTO>>(traces);
    }

    /// <inheritdoc/>
    public async Task InsertBulkDataAsync(List<PropertyDTO> properties)
    {
        await _repository.InsertBulkAsync(_mapper.Map<List<Property>>(
            properties,
            opts => opts.Items[ProfileConstants.FullConversionOptName] = true
        ));
    }

    /// <inheritdoc/>
    public async Task<long> GetPropertiesCount()
    {
        return await _repository.GetTotalCount();
    }

    /// <inheritdoc/>
    public async Task<PropertyDTO> GetPropertyByIdAsync(string id)
    {
        Property result = await _repository.GetAsync(new ObjectId(id));

        return _mapper.Map<PropertyDTO>(result, opts => opts.Items[ProfileConstants.FullConversionOptName] = true);
    }
}
