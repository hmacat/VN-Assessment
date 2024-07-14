using ProductRESTfulAPI.Dtos;
using ProductRESTfulAPI.Models;

namespace ProductRESTfulAPI.Extensions;

/// <summary>
/// Implemented to eliminate a direct dependency from Business Layer (Product) to ProductDTO.
/// </summary>
public static class ProductExtensions
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}