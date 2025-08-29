using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebsiteServerApp.DataAccess.Models.Base;

namespace WebsiteServerApp.DataAccess.Models;

/// <summary>
/// Model that contains all the information about a property entity.
/// </summary>
public class Property : BaseModel
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
    /// The owner id of this property.
    /// </summary>
    [BsonElement("_ownerId")]
    public ObjectId? OwnerId { get; set; }
    /// <summary>
    /// The property images for this property.
    /// </summary>
    public List<PropertyImage> PropertyImages { get; set; }
    /// <summary>
    /// The property traces for this property.
    /// </summary>
    public List<PropertyTrace> PropertyTraces { get; set; }

    public Property() : base()
    {
        Name = string.Empty;
        Address = string.Empty;
        Price = 0;
        CodeInternal = 0;
        Year = 1900;
        OwnerId = null;
        PropertyImages = new();
        PropertyTraces = new();
    }

    public Property(
        string name,
        string address,
        float price,
        int codeInternal,
        int year,
        ObjectId ownerId
        )
        : base()
    {
        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        Year = year;
        OwnerId = ownerId;
        PropertyImages = new();
        PropertyTraces = new();
    }

    public Property(
        string name,
        string address,
        float price,
        int codeInternal,
        int year,
        ObjectId ownerId,
        List<PropertyImage> propertyImages,
        List<PropertyTrace> propertyTraces
        )
        : base()
    {
        Name = name;
        Address = address;
        Price = price;
        CodeInternal = codeInternal;
        Year = year;
        OwnerId = ownerId;
        PropertyImages = propertyImages;
        PropertyTraces = propertyTraces;
    }
}
