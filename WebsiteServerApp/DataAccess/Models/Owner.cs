using WebsiteServerApp.DataAccess.Models.Base;

namespace WebsiteServerApp.DataAccess.Models;

/// <summary>
/// Model that contains all the information about an owner entity.
/// </summary>
public class Owner : BaseModel
{
    /// <summary>
    /// The name of the owner.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The email of the owner.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// The address of the owner.
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// The photo value of the owner.
    /// </summary>
    public string? Photo { get; set; }
    /// <summary>
    /// The birthday of the owner.
    /// </summary>
    public DateTime? Birthday { get; set; }

    public Owner() : base()
    {
        Name = string.Empty;
        Address = string.Empty;
        Email = string.Empty;
        Photo = string.Empty;
        Birthday = DateTime.MinValue;
    }

    public Owner(string name, string email, string address, string photo, DateTime birthday)
        : base()
    {
        Name = name;
        Email = email;
        Address = address;
        Photo = photo;
        Birthday = birthday;
    }
}
