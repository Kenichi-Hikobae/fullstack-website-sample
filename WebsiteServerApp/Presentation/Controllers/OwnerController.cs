using Microsoft.AspNetCore.Mvc;
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
}
