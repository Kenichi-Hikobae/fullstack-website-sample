using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.Interfaces;

namespace WebsiteServerApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost]
    public async Task<PropertiesLoadedCountDTO> GetProperties([FromBody] PropertyFiltersDTO filters)
    {
        return await _propertyService.GetPropertiesAsync(filters);
    }

    [HttpGet]
    [Route("{ownerId}")]
    public async Task<List<PropertyDTO>> GetPropertiesByOwner(string ownerId)
    {
        return await _propertyService.GetPropertiesByOwnerAsync(ownerId);
    }

    [HttpGet]
    [Route("{propertyId}")]
    public async Task<PropertyDTO> GetProperty(string propertyId)
    {
        return await _propertyService.GetPropertyByIdAsync(propertyId);
    }

    [HttpGet]
    [Route("{propertyId}")]
    public async Task<List<PropertyTraceDTO>> GetTracesByProperty(string propertyId)
    {
        return await _propertyService.GetTracesByPropertyAsync(propertyId);
    }


    [HttpPost]
    public async Task<IActionResult> InsertBulkProperties([FromBody] List<PropertyDTO> properties)
    {
        string file = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/DataAccess/Data/propertiesdto.json");
        List<PropertyDTO> serializeValues = JsonConvert.DeserializeObject<List<PropertyDTO>>(file);

        await _propertyService.InsertBulkDataAsync(serializeValues);

        return Ok();
    }

    [HttpGet]
    public async Task<long> GetTotalCount()
    {
        return await _propertyService.GetPropertiesCount();
    }
}
