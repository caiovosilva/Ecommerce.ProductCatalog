using Ecommerce.ProductCatalog.Data;
using Ecommerce.ProductCatalog.Models;

namespace Ecommerce.ProductCatalog.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync() => await repository.GetAllAsync();

    public async Task<Product?> GetProductByIdAsync(Guid id) => await repository.GetByIdAsync(id);

    public async Task AddProductAsync(ProductModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price
        };
        await repository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Guid id, ProductModel model)
    {
        var product = await repository.GetByIdAsync(id);
        if (product != null)
        {
            var updatedProduct = product with { Name = model.Name, Description = model.Description, Price = model.Price };
            await repository.UpdateAsync(updatedProduct);
        }
    }

    public async Task DeleteProductAsync(Guid id) => await repository.DeleteAsync(id);
}