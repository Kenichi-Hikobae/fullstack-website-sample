using WebsiteServerApp.BusinessServices.DTOs.Base;

namespace WebsiteServerApp.BusinessServices.DTOs;

public class PropertyImageDTO : BaseModelDTO
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
    public string PropertyId { get; set; }

    public PropertyImageDTO() : base()
    {
        File = string.Empty;
        Enabled = false;
        PropertyId = string.Empty;
    }

    public PropertyImageDTO(string id, string file, bool enabled, string propertyId)
        : base(id)
    {
        File = file;
        Enabled = enabled;
        PropertyId = propertyId;
    }
}
