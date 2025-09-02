using MongoDB.Bson;
using Moq;
using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Models;

namespace WebsiteServerApp.Tests;

public class MongoDBRepositoryTests
{
    private readonly Mock<IMongoDBRepository<Owner>> _mongoDBRepository;

    public MongoDBRepositoryTests()
    {
        _mongoDBRepository = new Mock<IMongoDBRepository<Owner>>();
    }

    [Fact]
    public async Task InsertAsync_NewModel_ExpectedModel()
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
        _mongoDBRepository.Setup(repo => repo.InsertAsync(owner)).ReturnsAsync(owner);

        /// Act
        Owner result = await _mongoDBRepository.Object.InsertAsync(owner);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, owner.Id);
    }

    [Fact]
    public async Task UpdateAsync_ExistingModel_ExpectedModel()
    {
        /// Arrange
        ObjectId id = ObjectId.GenerateNewId();
        Owner owner = new Owner()
        {
            Id = id,
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };

        Owner updatedOwner = new Owner()
        {
            Id = id,
            Name = "updated Name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };
        _mongoDBRepository.Setup(repo => repo.UpdateAsync(id, updatedOwner)).ReturnsAsync(updatedOwner);

        /// Act
        Owner result = await _mongoDBRepository.Object.UpdateAsync(id, updatedOwner);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, owner.Id);
        Assert.NotEqual(result.Name, owner.Name);
        Assert.Equal(result.Email, owner.Email);
    }

    [Fact]
    public async Task DeletedAsync_ExistingId_ExpectedBoolean()
    {
        /// Arrange
        ObjectId id = ObjectId.GenerateNewId();
        Owner owner = new Owner()
        {
            Id = id,
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };
        _mongoDBRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

        /// Act
        bool result = await _mongoDBRepository.Object.DeleteAsync(id);

        /// Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetAsync_ExistingId_ExpectedModel()
    {
        /// Arrange
        ObjectId id = ObjectId.GenerateNewId();
        Owner owner = new Owner()
        {
            Id = id,
            Name = "name",
            Email = "email",
            Address = "address",
            Birthday = DateTime.Now,
            Photo = "photo"
        };
        _mongoDBRepository.Setup(repo => repo.GetAsync(id)).ReturnsAsync(owner);

        /// Act
        Owner result = await _mongoDBRepository.Object.GetAsync(id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, owner.Id);
    }

    [Fact]
    public async Task GetAllAsync_NoParameter_ExpectedList()
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
        _mongoDBRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(list);

        /// Act
        IList<Owner> result = await _mongoDBRepository.Object.GetAllAsync();

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count, list.Count);
    }

    [Fact]
    public async Task InsertBulkAsync_NewModels_ExpectedList()
    {
        /// Arrange
        List<Owner> list = new() {
            new Owner()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            },
            new Owner()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            }
        };
        _mongoDBRepository.Setup(repo => repo.InsertBulkAsync(It.IsAny<List<Owner>>())).Returns(Task.CompletedTask);

        /// Act
        Task result = await Task.FromResult(_mongoDBRepository.Object.InsertBulkAsync(list));

        /// Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task GetTotalCountAsync_NoParameter_ExpectedList()
    {
        /// Arrange
        List<Owner> list = new() {
            new Owner()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            },
            new Owner()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "name",
                Email = "email",
                Address = "address",
                Birthday = DateTime.Now,
                Photo = "photo"
            }
        };
        _mongoDBRepository.Setup(repo => repo.GetTotalCount()).ReturnsAsync(list.Count);

        /// Act
        long result = await _mongoDBRepository.Object.GetTotalCount();

        /// Assert
        Assert.True(result > 0);
        Assert.Equal(result, list.Count);
    }
}
