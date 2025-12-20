using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlayerSponsor.Server.Controllers;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;
using PlayerSponsor.Server.Mappers;
using PlayerSponsor.Server.Repositories;
using PlayerSponsor.Server.Services.AccountService;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.DTOs;
using PlayerSponsor.UnitTests.Common;
using System.Security.Claims;

namespace PlayerSponsor.UnitTests.Controllers;

public class ClubControllerTests : TestBase
{
    private ClubService _clubService;
    private AccountService _accountService;
    private ClubController _controller;

    private Mock<IHttpContextAccessor> MockHttpContextAccessor;

    [SetUp]
    public void SetUp()
    {
        Setup();

        MockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        _clubService = new ClubService(
            new ClubRepository(identityDbContext),
            mapper,
            new ClubAdminRepository(identityDbContext),
            new LoggerFactory().CreateLogger<ClubService>()
        );

        _accountService = new AccountService(
            userManager,
            _clubService
        );

        _controller = new ClubController(_clubService, mapper, _accountService);
    }

    [Test]
    public async Task GetAllClubs_ReturnsOkResult_WithListOfClubs()
    {
        // Arrange
        var testClub = CreateTestClub();
        identityDbContext.Clubs.Add(testClub);
        await identityDbContext.SaveChangesAsync();

        // Act
        var result = await _controller.GetAllClubs();

        // Assert
        var resultObject = AssertionHelper.GetValueFromActionResult<IEnumerable<ClubDto>>(result);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        Assert.That(resultObject, Is.Not.Null);
        Assert.That(resultObject.Any(c => c.Name == testClub.Name), Is.True);
    }

    [Test]
    public async Task GetClubById_ClubExists_ReturnsOkResult_WithClub()
    {
        // Arrange
        var testClub = CreateTestClub(addProducts: true);
        identityDbContext.Clubs.Add(testClub);
        await identityDbContext.SaveChangesAsync();

        // Act
        var result = await _controller.GetClubById(testClub.Id);

        // Assert
        var resultObject = AssertionHelper.GetValueFromActionResult<ClubDto>(result);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        Assert.That(resultObject, Is.Not.Null);
        Assert.That(resultObject.Name, Is.EqualTo(testClub.Name));
        Assert.That(resultObject.Products, Is.Not.Empty);
    }

    [Test]
    public async Task GetClubById_ClubDoesNotExist_ReturnsNotFoundResult()
    {
        // Act
        var result = await _controller.GetClubById(999);

        // Assert
        var resultObject = AssertionHelper.GetValueFromActionResult<ProblemDetails>(result);
        Assert.That(resultObject, Is.InstanceOf<ProblemDetails>());
    }

    [Test]
    public async Task CreateClub_ValidRequest_ReturnsCreatedAtActionResult()
    {
        // Arrange
        MockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1231") })));

        var createRequest = new CreateClubRequest
        {
            AdminAccountDetails = new AccountDetails
            {
                ConfirmPassword = "Testauto123!",
                Email = "admin@gmail.com",
                Password = "Testauto123!",
            },
            ClubDetails = new ClubDetails
            {
                Name = "Test Club",
                Description = "Test Description",
                Email = "test@gmail.com",
                Slug = "test-club",
                PrimaryColour = "#FFFFFF",
                SecondaryColour = "#000000",
                HeroImageId = "hero-image-id"
            }
        };

        // Act
        var result = await _controller.Register(createRequest);

        // Assert
        var resultObject = AssertionHelper.GetValueFromActionResult<CreateClubResponse>(result);
        Assert.That(resultObject, Is.Not.Null);
        Assert.That(resultObject.ClubId, Is.Not.Null);
    }

    [Test]
    public async Task CreateClub_InValidRequest_Returns_Correct_Error_Code()
    {
        // Arrange
        MockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1231") })));

        _controller.ModelState.AddModelError("Club.PaymentDetails", "Payment details are invalid.");

        var createRequest = new CreateClubRequest
        {
            AdminAccountDetails = new AccountDetails
            {
                ConfirmPassword = "Testauto123!",
                Email = "admin@gmail.com",
                Password = "Testauto123!",
            },
            ClubDetails = new ClubDetails
            {
                Name = "Test Club",
                Description = "Test Description",
                Email = "invalid Email" // Invalid email format
            }
        };

        // Act
        var result = await _controller.Register(createRequest);

        // Assert
        AssertionHelper.AssertModelStateError(result, "Club.PaymentDetails", "Payment details are invalid.");
    }

    [Test]
    public async Task UpdateClub_ValidRequest_ReturnsNoContentResult()
    {
        // Arrange
        var testClub = CreateTestClub();
        identityDbContext.Clubs.Add(testClub);
        await identityDbContext.SaveChangesAsync();

        var updateRequest = new UpdateClubDetailsRequest { Name = "Updated Club", Email = "test@Gmail.com" };

        // Act
        var result = await _controller.UpdateClub(testClub.Id, updateRequest);

        // Assert
        var resultNoContent = result as NoContentResult;

        Assert.That(resultNoContent, Is.Not.Null);
        Assert.That(resultNoContent.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
    }

    [Test]
    public async Task DeleteClub_ClubExists_ReturnsNoContentResult()
    {
        // Arrange
        var testClub = CreateTestClub();
        identityDbContext.Clubs.Add(testClub);
        await identityDbContext.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteClub(testClub.Id);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteClub_ClubDoesNotExist_ReturnsNotFoundResult()
    {
        // Act
        var result = await _controller.DeleteClub(999);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public void User_ShouldPassValidation_WhenModelIsValid()
    {
        var model = new CreateClubRequest
        {
            AdminAccountDetails = new AccountDetails
            {
                Email = "admin@gmail.com",
                ConfirmPassword = "Testauto123!",
                Password = "Testauto123!",
            },
            ClubDetails = new ClubDetails
            {
                Name = "Test Club",
                Description = "Test Description",
                Email = "invalid Email" // Invalid email format
            }
        };

        var validationResult = ModelValidationHelper.ValidateModel(model.AdminAccountDetails);
        Assert.That(validationResult, Is.Empty);
    }
}
