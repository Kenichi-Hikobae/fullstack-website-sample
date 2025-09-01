using WebsiteServerApp.DataAccess.Enums;

namespace WebsiteServerApp.BusinessServices.DTOs;

/// <summary>
/// The types for categorize the year filters.
/// </summary>
public enum PropertyYearFilterType
{
    MoreThan20,
    LessThan5,
    LessThan10,
    LessThan20,
}

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
    /// <summary>
    /// The type for the year filtering.
    /// </summary>
    public PropertyYearFilterType? YearFilterType { get; set; }
    /// <summary>
    /// The max price of a property.
    /// </summary>
    public List<PropertyType>? Types { get; set; }
    /// <summary>
    /// The pagination model.
    /// </summary>
    public PaginationDTO Pagination { get; set; }

    public PropertyFiltersDTO()
    {
        Name = string.Empty;
        Address = string.Empty;
        MinPrice = 0;
        MaxPrice = 0;
        YearFilterType = PropertyYearFilterType.MoreThan20;
        Types = new List<PropertyType>();
        Pagination = new PaginationDTO();
    }

    public PropertyFiltersDTO(string name, string address, float minPrice, float maxPrice, 
        PropertyYearFilterType yearFilterType, List<PropertyType> types, PaginationDTO pagination)
    {
        Name = name;
        Address = address;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        YearFilterType = yearFilterType;
        Types = types;
        Pagination = pagination;
    }
}
