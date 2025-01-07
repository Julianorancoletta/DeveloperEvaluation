using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public CreateProductCommand()
    {
    }

    public CreateProductCommand(string title, decimal price, string description, string category, string image, Rating rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = rating;
    }

    public string Title { get; init; } = string.Empty;
    public decimal Price { get; init; } = 0M;
    public string Description { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
    public Rating Rating { get; init; } = new(0M,0);

    public ValidationResultDetail Validate()
    {
        var result = new CreateProductCommandValidator().Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}