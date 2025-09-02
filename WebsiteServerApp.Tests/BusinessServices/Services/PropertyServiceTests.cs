using MongoDB.Bson;
using Moq;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.DataAccess.Interfaces;

namespace WebsiteServerApp.Tests.BusinessServices.Services;

public class PropertyServiceTests
{
    private readonly Mock<IPropertyService> _propertyServiceMock;
    private readonly Mock<IPropertyRepository> _mongoDbRepository;

    public PropertyServiceTests()
    {
        _mongoDbRepository = new Mock<IPropertyRepository>();
        _propertyServiceMock = new Mock<IPropertyService>();
    }

    [Fact]
    public async Task GetPropertiesByOwnerAsync_ExistingOwnerId_ExpectedListDTO()
    {
        /// Arrange
        OwnerDTO owner = new OwnerDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        PropertyDTO property = new PropertyDTO()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        PropertyDTO property2 = new PropertyDTO()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        List<PropertyDTO> list = new() { property, property2 };
        _propertyServiceMock.Setup(repo => repo.GetPropertiesByOwnerAsync(owner.Id)).ReturnsAsync(list);

        /// Act
        List<PropertyDTO> result = await _propertyServiceMock.Object.GetPropertiesByOwnerAsync(owner.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task GetPropertiesAsync_ExistingOwnerId_ExpectedListDTO()
    {
        /// Arrange
        OwnerDTO owner = new OwnerDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        PropertyDTO property = new PropertyDTO()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        PropertyDTO property2 = new PropertyDTO()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        PropertyFiltersDTO filters = new PropertyFiltersDTO()
        {
            Name = "name",
            Address = string.Empty,
            MaxPrice = 1000000,
            MinPrice = 1000,
            YearFilterType = PropertyYearFilterType.MoreThan20
        };

        PropertiesLoadedCountDTO propertiesLoadedCount = new PropertiesLoadedCountDTO()
        {
            Properties = new List<PropertyDTO> { property, property2 },
            TotalCount = 2
        };

        _propertyServiceMock.Setup(repo => repo.GetPropertiesAsync(filters)).ReturnsAsync(propertiesLoadedCount);

        /// Act
        PropertiesLoadedCountDTO result = await _propertyServiceMock.Object.GetPropertiesAsync(filters);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.TotalCount, propertiesLoadedCount.TotalCount);
        Assert.Equal(result.Properties.Count, propertiesLoadedCount.Properties.Count);
    }

    [Fact]
    public async Task GetTracesByPropertyAsync_ExistingOwnerId_ExpectedListDTO()
    {
        /// Arrange
        OwnerDTO owner = new OwnerDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        PropertyDTO property = new PropertyDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        PropertyTraceDTO propertyTrace = new PropertyTraceDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            DateSale = DateTime.Now,
            PropertyId = property.Id,
            Tax = 159357,
            Value = 35741258
        };

        PropertyTraceDTO propertyTrace2 = new PropertyTraceDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            DateSale = DateTime.Now,
            PropertyId = property.Id,
            Tax = 159357,
            Value = 35741258
        };

        List<PropertyTraceDTO> list = new() { propertyTrace, propertyTrace2 };
        property.PropertyTraces = list;

        _propertyServiceMock.Setup(repo => repo.GetTracesByPropertyAsync(property.Id)).ReturnsAsync(list);

        /// Act
        List<PropertyTraceDTO> result = await _propertyServiceMock.Object.GetTracesByPropertyAsync(property.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task InsertBulkDataAsync_NewModels_ExpectedList()
    {
        /// Arrange
        OwnerDTO owner = new OwnerDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        List<PropertyDTO> list = new() {
            new PropertyDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Address = "address",
                CodeInternal = 12345678,
                OwnerId = owner.Id,
                Price = 135798462,
                Year = 1900
            },
            new PropertyDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Address = "address",
                CodeInternal = 12345678,
                OwnerId = owner.Id,
                Price = 135798462,
                Year = 1900
            },
        };
        _propertyServiceMock.Setup(repo => repo.InsertBulkDataAsync(list)).Returns(Task.CompletedTask);

        /// Act
        Task result = await Task.FromResult(_propertyServiceMock.Object.InsertBulkDataAsync(list));

        /// Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
    }


    [Fact]
    public async Task GetPropertiesCount_NoParameter_ExpectedList()
    {
        /// Arrange
        OwnerDTO owner = new OwnerDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        List<PropertyDTO> list = new() {
            new PropertyDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Address = "address",
                CodeInternal = 12345678,
                OwnerId = owner.Id,
                Price = 135798462,
                Year = 1900
            },
            new PropertyDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Address = "address",
                CodeInternal = 12345678,
                OwnerId = owner.Id,
                Price = 135798462,
                Year = 1900
            },
        };
        _propertyServiceMock.Setup(repo => repo.GetPropertiesCount()).ReturnsAsync(list.Count);

        /// Act
        long result = await _propertyServiceMock.Object.GetPropertiesCount();

        /// Assert
        Assert.True(result > 0);
        Assert.Equal(result, list.Count);
    }


    [Fact]
    public async Task GetPropertyByIdAsync_NoParameter_ExpectedList()
    {
        /// Arrange
        OwnerDTO owner = new OwnerDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        PropertyDTO property = new PropertyDTO()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };
        _propertyServiceMock.Setup(repo => repo.GetPropertyByIdAsync(property.Id)).ReturnsAsync(property);

        /// Act
        PropertyDTO result = await _propertyServiceMock.Object.GetPropertyByIdAsync(property.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result, property);
    }
}
