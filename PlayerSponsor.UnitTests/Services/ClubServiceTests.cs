using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlayerSponsor.Server.Mappers;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Repositories;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.Commands;
using PlayerSponsor.UnitTests.Common;

namespace PlayerSponsor.UnitTests.Services;

[TestFixture]
public class ClubServiceTests : TestBase
{
    private IClubRepository ClubRepository;
    private IClubAdminRepository AdminRepository;
    private Mock<ILogger<ClubService>> _mockLogger;
    private Mapper Mapper;
    private ClubService _sut;

    [SetUp]
    public void ClubServiceSetup()
    {
        ClubRepository = new ClubRepository(identityDbContext);
        AdminRepository = new ClubAdminRepository(identityDbContext);

        _mockLogger = new Mock<ILogger<ClubService>>();

        var profile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        Mapper = new Mapper(configuration);

        _sut = new ClubService(
            ClubRepository,
            Mapper,
            AdminRepository,
            _mockLogger.Object
        );
    }

    [Test]
    public async Task GetAllClubsAsync_ReturnsMappedDtos()
    {
        var club1 = CreateTestClub(clubId: 1, name: "Club One");
        var club2 = CreateTestClub(clubId: 2, name: "Club Two");

        await identityDbContext.SaveChangesAsync();

        var result = await _sut.GetAllClubsAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value.Count(), Is.EqualTo(2));

        var resultList = result.Value.ToList();
        Assert.Multiple(() =>
        {
            Assert.That(resultList[0].Id, Is.EqualTo(club1.Id));
            Assert.That(resultList[0].Name, Is.EqualTo(club1.Name));
            Assert.That(resultList[1].Id, Is.EqualTo(club2.Id));
            Assert.That(resultList[1].Name, Is.EqualTo(club2.Name));
        });
    }

    [Test]
    public async Task GetClubByIdAsync_ClubExists_ReturnsClub()
    {
        var club = CreateTestClub();
        identityDbContext.SaveChanges();

        var result = await _sut.GetClubByIdAsync(club.Id);

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.Value.Id, Is.EqualTo(club.Id));
            Assert.That(result.Value.Name, Is.EqualTo(club.Name));
        });
    }

    [Test]
    public async Task GetClubBySlugAsync_ClubExists_ReturnsClub()
    {
        var club = CreateTestClub(addProducts: true);
        identityDbContext.SaveChanges();

        var result = await _sut.GetClubBySlugAsync(club.Slug);

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.Value.Id, Is.EqualTo(club.Id));
            Assert.That(result.Value.Name, Is.EqualTo(club.Name));
            Assert.That(result.Value.Slug, Is.EqualTo(club.Slug));
            Assert.That(result.Value.Products.Count(), Is.EqualTo(1));
        });
    }

    [Test]
    public async Task GetClubByIdAsync_ClubNotFound_ThrowsException()
    {
        var result = await _sut.GetClubByIdAsync(999);

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task UpdateClubAsync_ClubExists_ReturnsTrue()
    {
        // Arrange
        var user = CreateTestUser();
        var club = CreateTestClub();

        identityDbContext.SaveChanges();

        var testUpdate = new UpdateClubDetailsCommand
        {
            Id = club.Id,
            Name = "Updated Club"
        };

        // Act
        var result = await _sut.UpdateClubDetails(testUpdate);

        var updatedClub = await ClubRepository.GetByIdAsync(club.Id);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.Multiple(() =>
        {
            Assert.That(updatedClub, Is.Not.Null);
            Assert.That(updatedClub.Id, Is.EqualTo(club.Id));
            Assert.That(updatedClub.Name, Is.EqualTo(testUpdate.Name));
            Assert.That(updatedClub.Description, Is.EqualTo(club.Description));
        });
    }

    [Test]
    public async Task UpdateClubAsync_ClubNotFound_ReturnsFalse()
    {
        var testUpdate = new UpdateClubDetailsCommand
        {
            Id = 123,
            Name = "Updated Club",
            Description = "Updated Bio"
        };

        var result = await _sut.UpdateClubDetails(testUpdate);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error.ErrorType, Is.EqualTo(ClubServiceError.UpdateFailure.ErrorType));
    }

    [Test]
    public async Task DeleteClubAsync_ClubExists_ReturnsTrue()
    {
        var club = CreateTestClub();

        var deleteClubResult = await _sut.DeleteClubAsync(club.Id);

        Assert.That(deleteClubResult.IsSuccess, Is.True);

        var getClubResult = await _sut.GetClubByIdAsync(club.Id);

        Assert.That(getClubResult.IsSuccess, Is.False);
    }

    [Test]
    public async Task DeleteClubAsync_ClubNotFound_ReturnsFalse()
    {
        var result = await _sut.DeleteClubAsync(999);

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task AddClubAdminAsync_CreatesClubAdmin()
    {
        // Arrange
        var club = CreateTestClub();
        var user = CreateTestUser();

        identityDbContext.SaveChanges();

        var clubAdmin = new ClubAdmin
        {
            UserId = user.Id,
            ClubId = club.Id
        };

        // Act
        var result = await _sut.AddClubAdminAsync(clubAdmin);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Value.UserId, Is.EqualTo(user.Id));
            Assert.That(result.Value.ClubId, Is.EqualTo(club.Id));
        });
    }
}
