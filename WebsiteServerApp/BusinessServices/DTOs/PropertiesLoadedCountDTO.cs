namespace WebsiteServerApp.BusinessServices.DTOs;

/// <summary>
/// DTO that contains a list of filtered properties and the total amoun of properties filtered.
/// </summary>
public class PropertiesLoadedCountDTO
{
    /// <summary>
    /// A list of property model,
    /// </summary>
    public List<PropertyDTO> Properties { get; set; }
    /// <summary>
    /// The toal amount of properties query.
    /// </summary>
    public long TotalCount { get; set; }

    public PropertiesLoadedCountDTO()
    {
        Properties = new List<PropertyDTO>();
        TotalCount = 0;
    }

    public PropertiesLoadedCountDTO(List<PropertyDTO> properties, long totalCount)
    {
        Properties = properties;
        TotalCount = totalCount;
    }
}
