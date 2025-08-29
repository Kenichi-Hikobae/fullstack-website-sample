using WebsiteServerApp.BusinessServices.DTOs.Base;

namespace WebsiteServerApp.BusinessServices.DTOs;

public class PropertyTraceDTO : BaseModelDTO
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
    public string PropertyId { get; set; }

    public PropertyTraceDTO() : base()
    {
        DateSale = DateTime.MinValue;
        Name = string.Empty;
        Value = 0;
        Tax = 0;
        PropertyId = string.Empty;
    }

    public PropertyTraceDTO(string id, DateTime dateSale, string name, float value, float tax, string propertyId)
        : base(id)
    {
        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;
        PropertyId = propertyId;
    }
}
