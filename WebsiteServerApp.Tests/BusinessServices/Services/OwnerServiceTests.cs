using MongoDB.Bson;
using Moq;
using WebsiteServerApp.BusinessServices.DTOs;
using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.DataAccess.Interfaces;

namespace WebsiteServerApp.Tests.BusinessServices.Services;

public class OwnerServiceTests
{
    private readonly Mock<IOwnerRepository> _ownerRepository;
    private readonly Mock<IOwnerService> _ownerService;

    public OwnerServiceTests()
    {
        _ownerRepository = new Mock<IOwnerRepository>();
        _ownerService = new Mock<IOwnerService>();
    }

    [Fact]
    public async Task GetAllOwnersAsync_NoParameter_ExpectedList()
    {
        /// Arrange
        List<OwnerDTO> list = new() {
            new OwnerDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            }
        };
        _ownerService.Setup(repo => repo.GetAllOwnersAsync()).ReturnsAsync(list);

        /// Act
        List<OwnerDTO> result = await _ownerService.Object.GetAllOwnersAsync();

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task InsertBulkDataAsync_NewModels_ExpectedList()
    {
        /// Arrange
        List<OwnerDTO> list = new() {
            new OwnerDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            },
            new OwnerDTO()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            }
        };
        _ownerService.Setup(repo => repo.InsertBulkDataAsync(It.IsAny<List<OwnerDTO>>())).Returns(Task.CompletedTask);

        /// Act
        Task result = await Task.FromResult(_ownerService.Object.InsertBulkDataAsync(list));

        /// Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
    }
}
