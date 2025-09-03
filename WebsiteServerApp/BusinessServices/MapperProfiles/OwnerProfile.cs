using AutoMapper;
using MongoDB.Bson;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.BusinessServices.MapperProfiles;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        /// Owner mapping
        CreateMap<Owner, OwnerDTO>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => src.Id.ToString()));
        CreateMap<OwnerDTO, Owner>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => new ObjectId(src.Id)));
    }
}
