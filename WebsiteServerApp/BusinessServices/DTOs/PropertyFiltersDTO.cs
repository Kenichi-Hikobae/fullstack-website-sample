namespace WebsiteServerApp.BusinessServices.DTOs;

/// <summary>
/// DTO class that contains filters to be applied to the property collection.
/// </summary>
public class PropertyFiltersDTO
{
    /// <summary>
    /// Name of the property.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Address of the property.
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// The min price of a property.
    /// </summary>
    public float? MinPrice { get; set; }
    /// <summary>
    /// The max price of a property.
    /// </summary>
    public float? MaxPrice { get; set; }

    public PropertyFiltersDTO()
    {
        Name = string.Empty;
        Address = string.Empty;
        MinPrice = 0;
        MaxPrice = 0;
    }

    public PropertyFiltersDTO(string name, string address, float minPrice, float maxPrice)
    {
        Name = name;
        Address = address;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
    }
}
