using Ecommerce.ProductCatalog.Data;
using Ecommerce.ProductCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductCatalog.UnitTests;

public class ProductRepositoryTests
{
    private readonly ProductRepository _repository;
    private readonly ProductContext _context;
    private readonly Faker<Product> _productFaker;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ProductContext(options);
        _repository = new ProductRepository(_context);

        _productFaker = new Faker<Product>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000));
    }

    [Fact]
    public async Task AddAsync_ShouldAddProduct()
    {
        // Arrange
        var product = _productFaker.Generate();

        // Act
        await _repository.AddAsync(product);
        var addedProduct = await _context.Products.FindAsync(product.Id);

        // Assert
        Assert.NotNull(addedProduct);
        Assert.Equal(product.Name, addedProduct.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = _productFaker.Generate();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Act
        var retrievedProduct = await _repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(retrievedProduct);
        Assert.Equal(product.Id, retrievedProduct.Id);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        var product = _productFaker.Generate();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Detach the tracked entity to prevent conflicts
        _context.Entry(product).State = EntityState.Detached;

        // Act
        product = product with { Name = "Updated Name" };
        await _repository.UpdateAsync(product);

        var updatedProduct = await _context.Products.FindAsync(product.Id);

        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal("Updated Name", updatedProduct.Name);
    }


    [Fact]
    public async Task DeleteAsync_ShouldRemoveProduct()
    {
        // Arrange
        var product = _productFaker.Generate();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(product.Id);
        var deletedProduct = await _context.Products.FindAsync(product.Id);

        // Assert
        Assert.Null(deletedProduct);
    }
}