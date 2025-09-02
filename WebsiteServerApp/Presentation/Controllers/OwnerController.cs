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
        string file = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/DataAccess/Data/ownersdto.json");
        List<OwnerDTO> serializeValues = JsonConvert.DeserializeObject<List<OwnerDTO>>(file);

        await _ownerService.InsertBulkDataAsync(serializeValues);

        return Ok();
    }
}
