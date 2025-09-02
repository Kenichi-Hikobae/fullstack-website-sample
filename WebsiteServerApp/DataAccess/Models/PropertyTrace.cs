using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebsiteServerApp.DataAccess.Models.Base;

namespace WebsiteServerApp.DataAccess.Models;

/// <summary>
/// Model that contains all the information about a property trace entity.
/// </summary>
public class PropertyTrace : BaseModel
{
    /// <summary>
    /// The date that the property was sold.
    /// </summary>
    public DateTime DateSale { get; set; }
    /// <summary>
    /// The name of this trace.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The value for this trace.
    /// </summary>
    public float Value { get; set; }
    /// <summary>
    /// The tax value for this trace.
    /// </summary>
    public float Tax { get; set; }

    /// <summary>
    /// The property id that owns this trace.
    /// </summary>
    [BsonElement("_propertyId")]
    public ObjectId PropertyId { get; set; }

    public PropertyTrace() : base()
    {
        DateSale = DateTime.MinValue;
        Name = string.Empty;
        Value = 0;
        Tax = 0;
        PropertyId = ObjectId.Empty;
    }

    public PropertyTrace(DateTime dateSale, string name, float value, float tax, ObjectId propertyId)
        : base()
    {
        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;
        PropertyId = propertyId;
    }
}
