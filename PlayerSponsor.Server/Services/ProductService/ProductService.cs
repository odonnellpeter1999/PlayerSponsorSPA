using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Repositories;

namespace PlayerSponsor.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private IProductRepository _productRepository;

    public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsByIdsAsync(List<int> ids)
    {
        return await _productRepository.GetByIdsAsync(ids);
    }
}
