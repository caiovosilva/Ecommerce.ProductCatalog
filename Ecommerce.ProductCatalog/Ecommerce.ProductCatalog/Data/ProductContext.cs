using Ecommerce.ProductCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductCatalog.Data;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
}