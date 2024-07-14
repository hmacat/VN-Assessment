using FluentResults;

namespace ProductRESTfulAPI.Models;

public class Product
{
    public static int MaxNameLength { get; } = 100;

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    /// <summary>
    /// Private constructor of Product. A Product instance can only be created with Create Factory method. 
    /// </summary>
    /// <param name="name">Name of the product.</param>
    /// <param name="price">Price of the product</param>
    private Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }

    /// <summary>
    /// Validates the input and creates a Product instance if the parameters are valid.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static Result<Product> Create(string name, decimal price)
    {
        var validationResult = Result.Merge(
            ValidateName(name),
            ValidatePrice(price)
        );

        if (validationResult.IsFailed)
            return validationResult;

        return Result.Ok(new Product(name, price));
    }

    /// <summary>
    /// Validates the input and updates the Product instance if the parameters are valid.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public Result Update(string name, decimal price)
    {
        var validationResult = Result.Merge(
            ValidateName(name),
            ValidatePrice(price)
        );

        if (validationResult.IsFailed)
            return validationResult;

        Name = name;
        Price = price;

        return Result.Ok();
    }

    private static Result ValidateName(string name)
    {
        return Result.Merge(
            Result.FailIf(string.IsNullOrWhiteSpace(name), $"{nameof(name)} is required."),
            Result.FailIf(
                !string.IsNullOrEmpty(name) && name.Length > MaxNameLength,
                $"{nameof(name)} should contain max {MaxNameLength} characters."
            )
        );
    }

    private static Result ValidatePrice(decimal price)
    {
        return Result.Merge(
            Result.FailIf(price <= 0, $"{nameof(price)} must be a positive value."));
    }
}