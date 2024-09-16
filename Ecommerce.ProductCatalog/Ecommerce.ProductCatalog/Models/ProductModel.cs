namespace Ecommerce.ProductCatalog.Models;

public record ProductModel
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
}