using Microsoft.AspNetCore.Mvc;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.Interfaces;

namespace WebsiteServerApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IOwnerService _ownerService;

    public PropertyController(IPropertyService propertyService, IOwnerService ownerService)
    {
        _propertyService = propertyService;
        _ownerService = ownerService;
    }

    [HttpGet]
    public async Task<List<PropertyDTO>> GetAllProperties()
    {
        return await _propertyService.GetAllPropertiesAsync();
    }

    [HttpGet]
    [Route("{ownerId}")]
    public async Task<List<PropertyDTO>> GetPropertiesByOwner(string ownerId)
    {
        return await _propertyService.GetPropertiesByOwnerAsync(ownerId);
    }

    [HttpPost]
    public async Task<List<PropertyDTO>> GetFilteredProperties(PropertyFiltersDTO filters)
    {
        return await _propertyService.FilterPropertiesAsync(filters);
    }

    [HttpGet]
    [Route("{propertyId}")]
    public async Task<List<PropertyTraceDTO>> GetTracesByProperty(string propertyId)
    {
        return await _propertyService.GetTracesByPropertyAsync(propertyId);
    }
}
