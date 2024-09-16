using Ecommerce.ProductCatalog.Models;

namespace Ecommerce.ProductCatalog.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync();

    public Task<Product?> GetProductByIdAsync(Guid id);

    public Task AddProductAsync(ProductModel model);

    public Task UpdateProductAsync(Guid id, ProductModel model);

    public Task DeleteProductAsync(Guid id);
}