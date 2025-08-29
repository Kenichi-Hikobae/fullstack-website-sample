using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.DTOs.Base;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.BusinessServices.Services;

/// <summary>
/// Service that implements operations within the owners an its relationships.
/// </summary>
public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    /// <inheritdoc/>
    public async Task<List<OwnerDTO>> GetAllOwnersAsync()
    {
        List<Owner> owners = await _ownerRepository.GetAllOwnersAsync();

        List<OwnerDTO> result = owners.Select(owner => owner.ToDTO()).ToList();

        return result;
    }
}
