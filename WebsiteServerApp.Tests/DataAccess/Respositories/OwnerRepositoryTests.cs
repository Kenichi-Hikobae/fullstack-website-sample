using MongoDB.Bson;
using Moq;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.Tests.DataAccess.Respositories;

public class OwnerRepositoryTests
{
    private readonly Mock<IOwnerRepository> _ownerRepository;

    public OwnerRepositoryTests()
    {
        _ownerRepository = new Mock<IOwnerRepository>();
    }

    [Fact]
    public async Task GetAllOwnersAsync_NoParameters_ExpectedModel()
    {
        /// Arrange
        ObjectId id = ObjectId.GenerateNewId();
        List<Owner> list = new() {
            new Owner()
            {
                Id = id,
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            }
        };
        _ownerRepository.Setup(repo => repo.GetAllOwnersAsync()).ReturnsAsync(list);

        /// Act
        IList<Owner> result = await _ownerRepository.Object.GetAllOwnersAsync();

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }
}
