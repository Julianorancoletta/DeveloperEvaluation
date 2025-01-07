using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
///  Represents a product in the system with information and ratings.
/// </summary>
public class Product : BaseEntity
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Image { get; private set; }
    public Rating Rating { get; private set; }


    public Product() { }

    private Product(string title, decimal price, string description, string category, string image, Rating rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = rating;
    }

    public static Product Create(string title, decimal price, string description, string category, string image, Rating rating)
    {
        return new Product(title, price, description, category, image, rating);
    }


    public void Update(string title, decimal price, string description, string category, string image, Rating rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = rating;
    }
    public void UpdateRating(Rating rating)
    {
        Rating = rating;
    }
}