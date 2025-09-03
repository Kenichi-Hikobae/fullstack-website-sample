using WebsiteServerApp.BusinessServices.DTOs.Base;
using WebsiteServerApp.DataAccess.Enums;

namespace WebsiteServerApp.BusinessServices.DTOs;

public class PropertyDTO : BaseModelDTO
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The name of the address.
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// The prices of the property.
    /// </summary>
    public float Price { get; set; }
    /// <summary>
    /// The code internal for the property.
    /// </summary>
    public int CodeInternal { get; set; }
    /// <summary>
    /// The year the property was registered.
    /// </summary>
    public int Year { get; set; }
    /// <summary>
    /// The type of this property.
    /// </summary>
    public PropertyType PropertyType { get; set; }
    /// <summary>
    /// The owner id of this property.
    /// </summary>
    public string? OwnerId { get; set; }
    /// <summary>
    /// The property images for this property.
    /// </summary>
    public List<PropertyImageDTO>? PropertyImages { get; set; }
    /// <summary>
    /// The property traces for this property.
    /// </summary>
    public List<PropertyTraceDTO>? PropertyTraces { get; set; }

    public PropertyDTO() : base()
    {
        Name = string.Empty;
        Address = string.Empty;
        Price = 0;
        CodeInternal = 0;
        Year = 1900;
        PropertyType = PropertyType.Type1;
        OwnerId = string.Empty;
        PropertyImages = new();
        PropertyTraces = new();
    }

    public PropertyDTO(
        string id,
        string name,
        string address,
        float price,
        int codeInternal,
        int year,
        PropertyType type,
        string ownerId,
        List<PropertyImageDTO> propertyImages,
        List<PropertyTraceDTO> propertyTraces
        )
        : base(id)
    {
        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        Year = year;
        PropertyType = type;
        OwnerId = ownerId;
        PropertyImages = propertyImages;
        PropertyTraces = propertyTraces;
    }
}
