using MongoDB.Bson;
using WebsiteServerApp.DataAccess.Models;
using YamlDotNet.Core.Tokens;

namespace WebsiteServerApp.BusinessServices.DTOs.Base;

/// <summary>
/// Static class that contains all the conversion between the models in the application.
/// </summary>
public static class DTOMapper
{
    #region Property
    public static PropertyDTO ToDTO(this Property property, bool fullConversion = false)
    {
        var model = new PropertyDTO()
        {
            Id = property.Id.ToString(),
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
            PropertyType = property.PropertyType,
            OwnerId = property.OwnerId is not null ? property.OwnerId.ToString() : null,
        };

        if (fullConversion)
        {
            model.PropertyImages = property.PropertyImages.Select(image => image.ToDTO()).ToList();
            model.PropertyTraces = property.PropertyTraces.Select(traces => traces.ToDTO()).ToList();
        }

        return model;
    }
    public static Property ToDatabase(this PropertyDTO property, bool fullConversion = false)
    {
        var model = new Property()
        {
            Id = new ObjectId(property.Id.ToString()),
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
            PropertyType = property.PropertyType,
            OwnerId = property.OwnerId is not null ? new ObjectId(property.OwnerId) : null,
        };

        if (fullConversion)
        {
            model.PropertyImages = property.PropertyImages.Select(image => image.ToDatabase()).ToList();
            model.PropertyTraces = property.PropertyTraces.Select(traces => traces.ToDatabase()).ToList();
        }

        return model;
    }

    public static PropertyImageDTO ToDTO(this PropertyImage property)
    {
        return new PropertyImageDTO()
        {
            Id = property.Id.ToString(),
            File = property.File,
            Enabled = property.Enabled,
            PropertyId = property.PropertyId.ToString(),
        };
    }
    public static PropertyImage ToDatabase(this PropertyImageDTO property)
    {
        return new PropertyImage()
        {
            Id = new ObjectId(property.Id.ToString()),
            File = property.File,
            Enabled = property.Enabled,
            PropertyId = new ObjectId(property.PropertyId.ToString()),
        };
    }

    public static PropertyTraceDTO ToDTO(this PropertyTrace property)
    {
        return new PropertyTraceDTO()
        {
            Id = property.Id.ToString(),
            Name = property.Name,
            DateSale = property.DateSale,
            Value = property.Value,
            Tax = property.Tax,
            PropertyId = property.PropertyId.ToString(),
        };
    }
    public static PropertyTrace ToDatabase(this PropertyTraceDTO property)
    {
        return new PropertyTrace()
        {
            Id = new ObjectId(property.Id.ToString()),
            Name = property.Name,
            DateSale = property.DateSale,
            Value = property.Value,
            Tax = property.Tax,
            PropertyId = new ObjectId(property.PropertyId.ToString()),
        };
    }
    #endregion

    #region Owner
    public static OwnerDTO ToDTO(this Owner owner)
    {
        return new OwnerDTO()
        {
            Id = owner.Id.ToString(),
            Name = owner.Name,
            Email = owner.Email,
            Address = owner.Address,
            Birthday = owner.Birthday,
            Photo = owner.Photo
        };
    }
    public static Owner ToDatabase(this OwnerDTO owner)
    {
        return new Owner()
        {
            Id = new ObjectId(owner.Id.ToString()),
            Name = owner.Name,
            Email = owner.Email,
            Address = owner.Address,
            Birthday = owner.Birthday,
            Photo = owner.Photo
        };
    }
    #endregion
}
