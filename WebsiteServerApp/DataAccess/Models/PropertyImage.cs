using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebsiteServerApp.DataAccess.Models.Base;

namespace WebsiteServerApp.DataAccess.Models;

/// <summary>
/// Model that contains all the information about a property image entity.
/// </summary>
public class PropertyImage : BaseModel
{
    /// <summary>
    /// The file of this image.
    /// </summary>
    public string File { get; set; }
    /// <summary>
    /// Whether the image is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// The property if that owns this image.
    /// </summary>
    [BsonElement("_propertyId")]
    public ObjectId PropertyId { get; set; }

    public PropertyImage() : base()
    {
        File = string.Empty;
        Enabled = false;
        PropertyId = ObjectId.Empty;
    }

    public PropertyImage(string file, bool enabled, ObjectId propertyId)
        : base()
    {
        File = file;
        Enabled = enabled;
        PropertyId = propertyId;
    }
}
