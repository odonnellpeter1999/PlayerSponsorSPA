using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlayerSponsor.Server.Repositories;
using PlayerSponsor.Server.Services.ProductService;
using PlayerSponsor.UnitTests.Common;

namespace PlayerSponsor.UnitTests.Services;

internal class ProductServiceTests: TestBase
{
    public ProductRepository ProductRepository { get; private set; }

    private Mock<ILogger<ProductService>> _mockLogger;
    private IProductService _sut;

    [SetUp]
    public void ProductServiceSetUp()
    {
        ProductRepository = new ProductRepository(identityDbContext);

        _mockLogger = new Mock<ILogger<ProductService>>();

        _sut = new ProductService(
            _mockLogger.Object,
            ProductRepository
        );
    }

    [Test]
    public async Task GetProductsByIdAsync_Returns_Saved_Product()
    {
        // Arrange
        var product = CreateTestProduct();

        await identityDbContext.SaveChangesAsync();


        // Act
        var result = await _sut.GetProductsByIdsAsync([product.Id]);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        var retrievedProduct = result.First();
        Assert.That(retrievedProduct.Id, Is.EqualTo(product.Id));
        Assert.That(retrievedProduct.Name, Is.EqualTo(product.Name));
        Assert.That(retrievedProduct.PriceUnit, Is.EqualTo(product.PriceUnit));
        Assert.That(retrievedProduct.Description, Is.EqualTo(product.Description));
        Assert.That(retrievedProduct.IconWord, Is.EqualTo(product.IconWord));
    }

    [Test]
    public async Task GetProductsByIdsAsync_Returns_Multiple_Saved_Product()
    {
        // Arrange
        var product1 = CreateTestProduct(productId:1);
        var product2 = CreateTestProduct(productId:2);

        await identityDbContext.SaveChangesAsync();

        // Act
        var result = await _sut.GetProductsByIdsAsync([product1.Id, product2.Id]);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        var retrievedProducts = result.ToList();
        Assert.That(retrievedProducts[0].Id, Is.EqualTo(product1.Id));
        Assert.That(retrievedProducts[1].Id, Is.EqualTo(product2.Id));
    }
}
