using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsByIdsAsync(List<int> ids);
    }
}