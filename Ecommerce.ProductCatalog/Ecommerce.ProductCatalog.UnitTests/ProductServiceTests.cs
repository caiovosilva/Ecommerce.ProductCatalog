using Ecommerce.ProductCatalog.Data;
using Ecommerce.ProductCatalog.Models;
using Ecommerce.ProductCatalog.Services;

namespace Ecommerce.ProductCatalog.UnitTests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly ProductService _productService;
    private readonly Faker<ProductModel> _productModelFaker;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_repositoryMock.Object);

        _productModelFaker = new Faker<ProductModel>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000));
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var fakeProducts = new List<Product> { new Product(), new Product() };
        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeProducts);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.Equal(fakeProducts.Count, result.Count());
        _repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var fakeProduct = new Product { Id = productId };
        _repositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(fakeProduct);

        // Act
        var result = await _productService.GetProductByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeProduct.Id, result?.Id);
        _repositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        // Arrange
        var productModel = _productModelFaker.Generate();

        // Act
        await _productService.AddProductAsync(productModel);

        // Assert
        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldUpdateProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product { Id = productId, Name = "Old Name" };
        var productModel = _productModelFaker.Generate();

        _repositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);

        // Act
        await _productService.UpdateProductAsync(productId, productModel);

        // Assert
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Product>(p => p.Name == productModel.Name)), Times.Once);
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldDeleteProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        await _productService.DeleteProductAsync(productId);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
    }
}