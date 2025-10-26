using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Mappers;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Repositories;
using PlayerSponsor.Server.Services.ClubService;
using System.Security.Claims;

namespace PlayerSponsor.UnitTests
{
    [TestFixture]
    public class ClubServiceTests : AuthenticationTestBase
    {
        private IClubRepository ClubRepository;
        private IClubAdminRepository AdminRepository;
        private Mapper Mapper;
        private Mock<IHttpContextAccessor> MockHttpContextAccessor;
        private ClubService _sut;
        private Mock<ILogger<ClubService>> _mockLogger;

        [SetUp]
        public void ClubServiceSetup()
        {
            ClubRepository = new ClubRepository(identityDbContext);
            AdminRepository = new ClubAdminRepository(identityDbContext);

            _mockLogger = new Mock<ILogger<ClubService>>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            Mapper = new Mapper(configuration);
            MockHttpContextAccessor = new Mock<IHttpContextAccessor>();

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
        public async Task GetClubByIdAsync_ClubNotFound_ThrowsException()
        {
            var result = await _sut.GetClubByIdAsync(999);

            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public async Task CreateClub_NullUserId_ThrowsArgumentNullException()
        {
            MockHttpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var request = new Club { Name = "Test Club" };

            var result = await _sut.CreateClub(request);

            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public async Task CreateClub_ClubCreationFails_ReturnsErrorAsync()
        {
            // Arrange
            var user = CreateTestUser();
            identityDbContext.SaveChanges();
            var request = new Club { Name = "Test Club", Bio = "This is a club", PaymentDetails="this is a bad payment" };

            // Act
            var result = await _sut.CreateClub(request);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.Validation));
        }

        [Test]
        public async Task UpdateClubAsync_ClubExists_ReturnsTrue()
        {
            // Arrange
            var user = CreateTestUser();
            var club = CreateTestClub();

            identityDbContext.SaveChanges();

            Club testUpdate = new Club
            {
                Id = club.Id,
                Name = "Updated Club",
                Bio = "Updated Bio",
                Logo = "Updated Logo"
            };

            // Act
            var result = await _sut.UpdateClubAsync(testUpdate);

            var updatedClub = await ClubRepository.GetByIdAsync(club.Id);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.Multiple(() =>
            {
                Assert.That(updatedClub, Is.Not.Null);
                Assert.That(updatedClub.Id, Is.EqualTo(club.Id));
                Assert.That(updatedClub.Name, Is.EqualTo(testUpdate.Name));
                Assert.That(updatedClub.Bio, Is.EqualTo(testUpdate.Bio));
                Assert.That(updatedClub.Logo, Is.EqualTo(testUpdate.Logo));
                // Ensure payment details are not changed if not provided
                Assert.That(updatedClub.PaymentDetails, Is.EqualTo(club.PaymentDetails));
            });
        }

        [Test]
        public async Task UpdateClubAsync_ClubNotFound_ReturnsFalse()
        {
            Club testUpdate = new Club
            {
                Id = 123,
                Name = "Updated Club",
                Bio = "Updated Bio",
                Logo = "Updated Logo"
            };

            var result = await _sut.UpdateClubAsync(testUpdate);

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
}
