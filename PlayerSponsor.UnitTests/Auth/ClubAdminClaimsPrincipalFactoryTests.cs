using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using PlayerSponsor.Server.Auth;
using PlayerSponsor.UnitTests.Common;

namespace PlayerSponsor.UnitTests.Auth
{
    internal class ClubAdminClaimsPrincipalFactoryTests : TestBase
    {
        private ClubAdminClaimsPrincipalFactory _sut;

        [SetUp]
        public void SetUp()
        {
            base.Setup();
            _sut = new ClubAdminClaimsPrincipalFactory(userManager, Options.Create(new IdentityOptions()), identityDbContext);
        }

        [Test]
        public async Task GenerateClaimsAsync_ShouldAddClubAdminClaims_WhenUserIsClubAdminAsync()
        {
            // Arrange
            var clubId = 12345;
            var userId = "test-user-id";

            var user = CreateTestUser(userId);

            var club = CreateTestClub(clubId: clubId);

            var clubAdmin = CreateTestClubAdmin(user, club);

            await identityDbContext.SaveChangesAsync();

            // Act
            var claimsIdentity = await _sut.CreateAsync(user);

            // Assert
            Assert.That(claimsIdentity, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(claimsIdentity.Claims.Any(c => c.Type == "ClubAdmin" && c.Value == clubId.ToString()), Is.True);
                Assert.That(claimsIdentity.HasClaim("ClubAdmin", clubId.ToString()), Is.True);
            });
        }

        [Test]
        public async Task GenerateClaimsAsync_ShouldAddClubAdminClaims_WhenUserIsInMultipleClubs()
        {
            // Arrange
            var userId = "test-user-id";

            var user = CreateTestUser(userId);

            var club1 = CreateTestClub();

            var club2 = CreateTestClub();

            CreateTestClubAdmin(user, club1);
            CreateTestClubAdmin(user, club2);

            await identityDbContext.SaveChangesAsync();

            // Act
            var claimsIdentity = await _sut.CreateAsync(user);

            // Assert
            Assert.That(claimsIdentity, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(claimsIdentity.HasClaim("ClubAdmin", club1.Id.ToString()), Is.True);
                Assert.That(claimsIdentity.HasClaim("ClubAdmin", club2.Id.ToString()), Is.True);
            });
        }

        [Test]
        public async Task GenerateClaimsAsync_ShouldAddClubAdminClaims_WhenUserIsInNoClubs()
        {
            // Arrange
            var userId = "test-user-id";

            var user = CreateTestUser(userId);

            await identityDbContext.SaveChangesAsync();

            // Act
            var claimsIdentity = await _sut.CreateAsync(user);

            // Assert
            Assert.That(claimsIdentity, Is.Not.Null);
            Assert.That(claimsIdentity.Claims.Where(c => c.Type == "ClubAdmin"), Is.Empty);
        }
    }
}
