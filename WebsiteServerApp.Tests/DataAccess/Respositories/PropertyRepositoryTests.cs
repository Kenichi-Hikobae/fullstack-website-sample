using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.Tests.DataAccess.Respositories;

public class PropertyRepositoryTests
{
    private readonly Mock<IPropertyRepository> _propertyRepositoryMock;

    public PropertyRepositoryTests()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
    }

    [Fact]
    public async Task GetPropertiesByOwnerIdAsync_ExistingOwnerId_ExpectedList()
    {
        /// Arrange
        Owner owner = new Owner()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        Property property = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        Property property2 = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = owner.Id,
            Price = 135798462,
            Year = 1900
        };

        List<Property> list = new() { property, property2 };
        _propertyRepositoryMock.Setup(repo => repo.GetPropertiesByOwnerIdAsync(owner.Id)).ReturnsAsync(list);

        /// Act
        List<Property> result = await _propertyRepositoryMock.Object.GetPropertiesByOwnerIdAsync(owner.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task GetAllPropertiesAsync_ExistingId_ExpectedList()
    {
        /// Arrange
        Property property = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = ObjectId.GenerateNewId(),
            Price = 135798462,
            Year = 1900
        };

        Property property2 = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = ObjectId.GenerateNewId(),
            Price = 135798462,
            Year = 1900
        };

        List<Property> list = new() { property, property2 };
        _propertyRepositoryMock.Setup(repo => repo.GetAllPropertiesAsync()).ReturnsAsync(list);

        /// Act
        List<Property> result = await _propertyRepositoryMock.Object.GetAllPropertiesAsync();

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task GetPropertiesByFiltersAsync_ExistingFilters_ExpectedList()
    {
        /// Arrange
        Property property = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = ObjectId.GenerateNewId(),
            Price = 135798462,
            Year = 1900
        };

        Property property2 = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = ObjectId.GenerateNewId(),
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

        List<Property> list = new() { property, property2 };
        _propertyRepositoryMock.Setup(repo => repo.GetPropertiesByFiltersAsync(
            It.IsAny<FilterDefinition<Property>>(), 1, 10)).ReturnsAsync(list);

        /// Act
        List<Property> result = await _propertyRepositoryMock.Object.GetPropertiesByFiltersAsync(
            It.IsAny<FilterDefinition<Property>>(), 1, 10);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task GetTracesByPropertyIdAsync_ExistingModelId_ExpectedList()
    {
        /// Arrange
        Property property = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = ObjectId.GenerateNewId(),
            Price = 135798462,
            Year = 1900
        };

        PropertyTrace propertyTrace = new PropertyTrace()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "name",
            DateSale = DateTime.UtcNow,
            Tax = 9876542,
            Value = 9876542,
            PropertyId = property.Id,
        };

        PropertyTrace propertyTrace1 = new PropertyTrace()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "name",
            DateSale = DateTime.UtcNow,
            Tax = 9876542,
            Value = 9876542,
            PropertyId = property.Id,
        };

        List<PropertyTrace> list = new() { propertyTrace1, propertyTrace };
        _propertyRepositoryMock.Setup(repo => repo.GetTracesByPropertyIdAsync(property.Id)).ReturnsAsync(list);

        /// Act
        List<PropertyTrace> result = await _propertyRepositoryMock.Object.GetTracesByPropertyIdAsync(property.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task GetCountByFiltersAsync_ExistingFilters_ExpectedList()
    {
        /// Arrange
        Property property = new Property()
        {
            Name = "name",
            Address = "address",
            CodeInternal = 12345678,
            OwnerId = ObjectId.GenerateNewId(),
            Price = 135798462,
            Year = 1900
        };

        long values = 5;
        _propertyRepositoryMock.Setup(repo => repo.GetCountByFiltersAsync(
            It.IsAny<FilterDefinition<Property>>())).ReturnsAsync(values);

        /// Act
        long result = await _propertyRepositoryMock.Object.GetCountByFiltersAsync(
            It.IsAny<FilterDefinition<Property>>());

        /// Assert
        Assert.True(result > 0);
        Assert.Equal(result, values);
    }
}
