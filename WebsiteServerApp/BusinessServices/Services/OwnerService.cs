using Newtonsoft.Json;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.DTOs.Base;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;
using WebsiteServerApp.DataAccess.Repositories;

namespace WebsiteServerApp.BusinessServices.Services;

/// <summary>
/// Service that implements operations within the owners an its relationships.
/// </summary>
public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;
    private OwnerRepository _repository => _ownerRepository as OwnerRepository;

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

    /// <inheritdoc/>
    public async Task InsertBulkDataAsync(List<OwnerDTO> owners)
    {
        List<Owner> ownersModels = owners.Select(owner => owner.ToDatabase()).ToList();

        var serialize = JsonConvert.SerializeObject(ownersModels);
        File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/DataAccess/Data/owners.json", serialize);

        await _repository.InsertBulkAsync(ownersModels);
    }
}
