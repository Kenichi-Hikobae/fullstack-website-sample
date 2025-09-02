namespace WebsiteServerApp.BusinessServices.DTOs.Base;

/// <summary>
/// A base model class used for DTOs.
/// </summary>
public class BaseModelDTO
{
    /// <summary>
    /// The id of the DTOs.
    /// </summary>
    public string Id { get; set; }

    public BaseModelDTO()
    {
        Id = string.Empty;
    }

    public BaseModelDTO(string id)
    {
        Id = id;
    }
}
