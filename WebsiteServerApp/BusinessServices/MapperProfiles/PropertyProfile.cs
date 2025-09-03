using AutoMapper;
using MongoDB.Bson;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.BusinessServices.MapperProfiles;

/// <summary>
/// Constants for definig special options for the profiling conversions.
/// </summary>
public static class ProfileConstants
{
    public const string FullConversionOptName = "FullConversion";
}

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyDTO>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.OwnerId, member => member.MapFrom(src => src.OwnerId != null ? src.OwnerId.ToString() : null))
            .ForMember(dest => dest.PropertyImages, opt =>
                opt.MapFrom((src, dest, destMember, context) =>
                    context.Items.ContainsKey(ProfileConstants.FullConversionOptName) && (bool)context.Items[ProfileConstants.FullConversionOptName]
                        ? src.PropertyImages
                        : new List<PropertyImage>()))
            .ForMember(dest => dest.PropertyTraces, opt =>
                opt.MapFrom((src, dest, destMember, context) =>
                    context.Items.ContainsKey(ProfileConstants.FullConversionOptName) && (bool)context.Items[ProfileConstants.FullConversionOptName]
                        ? src.PropertyTraces
                        : new List<PropertyTrace>()));

        CreateMap<PropertyDTO, Property>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => new ObjectId(src.Id)))
            .ForMember(dest => dest.OwnerId, member => member.MapFrom(src => new ObjectId(src.OwnerId)))
            .ForMember(dest => dest.PropertyImages, member => member.MapFrom(src => src.PropertyImages))
            .ForMember(dest => dest.PropertyTraces, member => member.MapFrom(src => src.PropertyTraces));

        CreateMap<PropertyImage, PropertyImageDTO>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.PropertyId, member => member.MapFrom(src => src.PropertyId.ToString()));
        CreateMap<PropertyImageDTO, PropertyImage>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => new ObjectId(src.Id)))
            .ForMember(dest => dest.PropertyId, member => member.MapFrom(src => new ObjectId(src.PropertyId)));

        CreateMap<PropertyTrace, PropertyTraceDTO>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.PropertyId, member => member.MapFrom(src => src.PropertyId.ToString()));
        CreateMap<PropertyTraceDTO, PropertyTrace>()
            .ForMember(dest => dest.Id, member => member.MapFrom(src => new ObjectId(src.Id)))
            .ForMember(dest => dest.PropertyId, member => member.MapFrom(src => new ObjectId(src.PropertyId)));
    }
}
