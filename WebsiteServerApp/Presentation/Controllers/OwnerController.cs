using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.Interfaces;

namespace WebsiteServerApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OwnerController : Controller
{
    private readonly IOwnerService _ownerService;

    public OwnerController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    [HttpGet]
    public async Task<List<OwnerDTO>> GetAllOwners()
    {
        return await _ownerService.GetAllOwnersAsync();
    }

    [HttpPost]
    public async Task<IActionResult> InsertBulkOwners([FromBody] List<OwnerDTO> owners)
    {
        /// Uncomment if you wish to insert the sample data, saved as json in the project.
        /// You can use the swagger to make the call.
        //await InsertSampleData();

        /// Comment the next line if you are inserting the sample data
        await _ownerService.InsertBulkDataAsync(owners);

        return Ok();
    }

    /// <summary>
    /// Insert the sample data to the database.
    /// Only use once, next time make sure to clean the collections.
    /// </summary>
    /// <returns></returns>
    private async Task InsertSampleData()
    {
        string file = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/DataAccess/Data/ownersdto.json");
        List<OwnerDTO> serializeValues = JsonConvert.DeserializeObject<List<OwnerDTO>>(file);

        await _ownerService.InsertBulkDataAsync(serializeValues);
    }
}
