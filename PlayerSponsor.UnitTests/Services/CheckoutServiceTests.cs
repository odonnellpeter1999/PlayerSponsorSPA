using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.CheckoutService;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.Commands;
using PlayerSponsor.Server.Services.DTOs;
using PlayerSponsor.Server.Services.SessionService;
using Stripe.Checkout;

namespace PlayerSponsor.UnitTests.Services;

public class CheckoutServiceTests
{
    private CheckoutService _sut;
    private Mock<IClubService> _mockClubService;
    private Mock<ISessionService> _mockSessionService;
    private Mock<IConfiguration> _mockConfiguration;
    private SessionCreateOptions capturedOptions;

    [SetUp]
    public void ProductServiceSetUp()
    {
        _mockClubService = new Mock<IClubService>();
        _mockSessionService = new Mock<ISessionService>();
        _mockConfiguration = new Mock<IConfiguration>();

        _mockConfiguration.Setup(c => c["Host"]).Returns("https://playersponsor.com");

        _mockSessionService.Setup(s => s.Create(It.IsAny<SessionCreateOptions>()))
            .Callback<SessionCreateOptions>(options => capturedOptions = options)
            .Returns(new Session()
            {
                Id = "cs_test_1234567890",
                Url = "https://checkout.stripe.com/pay/cs_test_1234567890"
            });

        _mockClubService.Setup(c => c.GetClubBySlugAsync(It.IsAny<string>()))
            .ReturnsAsync(ResultT<ClubDto>.Success(new ClubDto
            {
                Id = 1,
                Name = "Test Club",
                Slug = "test-club",
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Product 1",
                        Description = "Description 1",
                        PriceUnit = 1000
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Product 2",
                        Description = "Description 2",
                        PriceUnit = 2000
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Product 3",
                        Description = "Description 3",
                        PriceUnit = 3000
                    }
                },
            }));

        _mockSessionService.Setup(s => s.Create(
           It.IsAny<SessionCreateOptions>()
        )).Returns(new Session()
        {
            Id = "cs_test_1234567890",
            Url = "https://checkout.stripe.com/pay/cs_test_1234567890"
        });

        _sut = new CheckoutService(
            _mockConfiguration.Object,
            _mockClubService.Object,
            _mockSessionService.Object
        );
    }

    [Test]
    public async Task CheckoutService_Returns_Valid_Results()
    {
        // Arrange
        var command = new CreateCheckoutSessionCommand
        {
            ClubSlug = "test-club",
            ProductIds = [1, 2, 3]
        };

        // Act
        var result = await _sut.CreateCheckoutSessionAsync(command);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Empty);
        Assert.That(result.Value, Is.EqualTo("https://checkout.stripe.com/pay/cs_test_1234567890"));
        _mockSessionService.Verify(s => s.Create(It.IsAny<SessionCreateOptions>()), Times.Once);

        Assert.Multiple(() =>
        {
            Assert.That(capturedOptions, Is.Not.Null, "SessionCreateOptions should not be null");
            Assert.That(capturedOptions.LineItems, Is.Not.Null, "LineItems should not be null");
            Assert.That(capturedOptions.LineItems.Count, Is.EqualTo(3), $"Expected 3 line items, but got {capturedOptions.LineItems.Count}");
            Assert.That(capturedOptions.Mode, Is.EqualTo("payment"), $"Expected mode 'payment', but got '{capturedOptions.Mode}'");
            Assert.That(capturedOptions.SuccessUrl, Is.EqualTo("https://playersponsor.com/test-club/checkout/success"), $"Expected success URL 'https://playersponsor.com/test-club/checkout/success', but got '{capturedOptions.SuccessUrl}'");
            Assert.That(capturedOptions.CancelUrl, Is.EqualTo("https://playersponsor.com/test-club/checkout/cancel"), $"Expected cancel URL 'https://playersponsor.com/test-club/checkout/cancel', but got '{capturedOptions.CancelUrl}'");
        });
    }

    [Test]
    public async Task CheckoutService_Returns_Error_When_Missing_ProductId()
    {
        // Arrange
        var command = new CreateCheckoutSessionCommand
        {
            ClubSlug = "test-club",
            ProductIds = [1, 2, 3, 4]
        };

        // Act
        var result = await _sut.CreateCheckoutSessionAsync(command);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error.Description, Is.EqualTo("Invalid productIds included in request"));
        _mockSessionService.Verify(s => s.Create(It.IsAny<SessionCreateOptions>()), Times.Never);
    }

    [Test]
    public async Task CheckoutService_Returns_Error_When_Missing_No_Valid_ProductId()
    {
        // Arrange
        var command = new CreateCheckoutSessionCommand
        {
            ClubSlug = "test-club",
            ProductIds = [10, 20, 30, 40]
        };

        // Act
        var result = await _sut.CreateCheckoutSessionAsync(command);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error.Description, Is.EqualTo("Invalid productIds included in request"));
        _mockSessionService.Verify(s => s.Create(It.IsAny<SessionCreateOptions>()), Times.Never);
    }

    [Test]
    public async Task CheckoutService_Returns_Error_When_Club_Not_Found()
    {
        // Arrange
        var command = new CreateCheckoutSessionCommand
        {
            ClubSlug = "non-existent-club",
            ProductIds = [1, 2, 3, 4]
        };

        _mockClubService.Setup(c => c.GetClubBySlugAsync(It.IsAny<string>()))
            .ReturnsAsync(ResultT<ClubDto>.Failure(Error.NotFound("ClubNotFound", "The specified club was not found.")));

        // Act
        var result = await _sut.CreateCheckoutSessionAsync(command);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error.Description, Is.EqualTo("The specified club was not found."));
        _mockSessionService.Verify(s => s.Create(It.IsAny<SessionCreateOptions>()), Times.Never);
    }
}
