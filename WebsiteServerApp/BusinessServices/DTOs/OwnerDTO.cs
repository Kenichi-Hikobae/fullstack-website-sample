using WebsiteServerApp.BusinessServices.DTOs.Base;

namespace WebsiteServerApp.BusinessServices.DTOs;

public class OwnerDTO : BaseModelDTO
{
    /// <summary>
    /// The name of the owner.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The address of the owner.
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// The photo value of the owner.
    /// </summary>
    public string Photo { get; set; }
    /// <summary>
    /// The birthday of the owner.
    /// </summary>
    public DateTime Birthday { get; set; }

    public OwnerDTO() : base()
    {
        Name = string.Empty;
        Address = string.Empty;
        Photo = string.Empty;
        Birthday = DateTime.MinValue;
    }

    public OwnerDTO(string id, string name, string address, string photo, DateTime birthday)
        : base(id)
    {
        Name = name;
        Address = address;
        Photo = photo;
        Birthday = birthday;
    }
}
