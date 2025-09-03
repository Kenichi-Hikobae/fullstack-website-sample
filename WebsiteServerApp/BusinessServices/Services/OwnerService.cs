using AutoMapper;
using WebsiteServerApp.BusinessServices.DTOs;
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
    private readonly IMapper _mapper;
    private readonly IOwnerRepository _ownerRepository;
    private OwnerRepository _repository => _ownerRepository as OwnerRepository;

    public OwnerService(IMapper mapper, IOwnerRepository ownerRepository)
    {
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }

    /// <inheritdoc/>
    public async Task<List<OwnerDTO>> GetAllOwnersAsync()
    {
        List<Owner> owners = await _ownerRepository.GetAllOwnersAsync();

        return _mapper.Map<List<OwnerDTO>>(owners);
    }

    /// <inheritdoc/>
    public async Task InsertBulkDataAsync(List<OwnerDTO> owners)
    {
        await _repository.InsertBulkAsync(_mapper.Map<List<Owner>>(owners));
    }
}
