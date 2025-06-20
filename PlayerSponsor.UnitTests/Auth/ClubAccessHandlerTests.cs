using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PlayerSponsor.Server.Auth;
using PlayerSponsor.Server.Models;
using System.Security.Claims;

namespace PlayerSponsor.UnitTests.Auth;

internal class ClubAccessHandlerTests: AuthenticationTestBase
{
    private ClubAccessHandler _sut;
    private Mock<IHttpContextAccessor> _httpContextAccessor;

    [SetUp]
    public new void Setup()
    {
        base.Setup();

        _httpContextAccessor = new Mock<IHttpContextAccessor>();

        _sut = new ClubAccessHandler(_httpContextAccessor.Object);
    }

    [Test]
    public async Task HandleRequirementAsync_WhenUserIsClubAdmin_AndRequestsTheirClub_Succeeds()
    {
        // Arrange
        var user = CreateTestUser();
        var club = CreateTestClub(clubId: 123);
        CreateTestClubAdmin(user, club);
        var context = CreateAuthorizationContext(user, new[] { new Claim("ClubAdmin", "123") });

        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["clubId"] = "123";
        _httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        await _sut.HandleAsync(context);

        // Assert
        Assert.That(context.HasSucceeded, Is.True);
    }

    [Test]
    public async Task HandleRequirementAsync_WhenUserIsClubAdmin_ButRequestsDifferentClub_Fails()
    {
        // Arrange
        var user = CreateTestUser();
        var club = CreateTestClub(clubId: 123);
        CreateTestClubAdmin(user, club);
        var context = CreateAuthorizationContext(user, new[] { new Claim("ClubAdmin", "123") });

        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["clubId"] = "456";
        _httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        await _sut.HandleAsync(context);

        // Assert
        Assert.That(context.HasSucceeded, Is.False);
    }

    [Test]
    public async Task HandleRequirementAsync_WhtenUserHasNoClubAdminClaims_Fails()
    {
        // Arrange
        var user = CreateTestUser();
        var context = CreateAuthorizationContext(user, Array.Empty<Claim>());

        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["clubId"] = "123";
        _httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        await _sut.HandleAsync(context);

        // Assert
        Assert.That(context.HasSucceeded, Is.False);
    }

    [Test]
    public async Task HandleRequirementAsync_WhenNoClubIdInRoute_Fails()
    {
        // Arrange
        var user = CreateTestUser();
        var club = CreateTestClub(clubId: 123);
        CreateTestClubAdmin(user, club);
        var context = CreateAuthorizationContext(user, new[] { new Claim("ClubAdmin", "123") });

        var httpContext = new DefaultHttpContext();
        _httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        await _sut.HandleAsync(context);

        // Assert
        Assert.That(context.HasSucceeded, Is.False);
    }

    private static AuthorizationHandlerContext CreateAuthorizationContext(ApplicationUser user, IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        return new AuthorizationHandlerContext(
            new[] { new ClubAccessRequirement() },
            claimsPrincipal,
            null);
    }
}
