using Microsoft.AspNetCore.Mvc;
using ProductRESTfulAPI.Dtos;
using ProductRESTfulAPI.Extensions;
using ProductRESTfulAPI.Models;
using ProductRESTfulAPI.Services;

namespace ProductRESTfulAPI.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> _productRepository;

    public ProductController(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto)
    {
        var createProductResult = Product.Create(productCreateUpdateDto.Name, productCreateUpdateDto.Price);

        if (createProductResult.IsFailed)
            return BadRequest(createProductResult.Errors.Select(error => error.Message));

        var newProduct = createProductResult.Value;
        _productRepository.Add(newProduct);
        await _productRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct.ToDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _productRepository.GetAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product.ToDto());
    }

    [HttpGet]
    public async Task<ActionResult<Product>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();

        if (!products.Any())
        {
            return Ok(new List<ProductDto>()); // Return an empty list if there are no products defined yet.
        }

        return Ok(products.Select(p => p.ToDto()).ToList());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(Guid id, ProductCreateUpdateDto productCreateUpdateDto)
    {
        var productToUpdate = await _productRepository.GetAsync(id);

        if (productToUpdate == null)
            return NotFound();

        var updateProductResult = productToUpdate.Update(productCreateUpdateDto.Name, productCreateUpdateDto.Price);

        if (updateProductResult.IsFailed)
            return BadRequest(updateProductResult.Errors.Select(error => error.Message));

        _productRepository.Update(productToUpdate);
        await _productRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetAsync(id);

        if (product == null)
            return NotFound();

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();

        return NoContent();
    }
}
