namespace WebsiteServerApp.BusinessServices.DTOs;

/// <summary>
/// DTO class that contains the information to filter the data by apges.
/// </summary>
public class PaginationDTO
{
    /// <summary>
    /// The page size to filter.
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// The current page to request.
    /// </summary>
    public int PageNumber { get; set; }

    public PaginationDTO()
    {
        PageSize = 10;
        PageNumber = 1;
    }

    public PaginationDTO(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}
