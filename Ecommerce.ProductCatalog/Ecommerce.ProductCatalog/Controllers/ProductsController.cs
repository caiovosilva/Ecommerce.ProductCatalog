using Ecommerce.ProductCatalog.Models;
using Ecommerce.ProductCatalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ProductCatalog.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(ProductService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await service.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await service.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductModel model)
    {
        await service.AddProductAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, model);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductModel model)
    {
        await service.UpdateProductAsync(id, model);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeleteProductAsync(id);
        return NoContent();
    }
}