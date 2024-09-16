using Ecommerce.ProductCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductCatalog.Data;

public class ProductRepository(ProductContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}