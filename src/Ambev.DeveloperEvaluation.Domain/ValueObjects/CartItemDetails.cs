using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public record CartItemDetails : CartItem
{
    public decimal UnitPrice { get; private set; } = 0M;
    public decimal TotalAmountWithDiscount { get; private set; } = 0M;
    public decimal TotalAmount { get; set; } = 0M;
    public Product Product { get; private set; } = new();
    public decimal TotalDiscounts { get; private set; } = 0M;

    public CartItemDetails(CartItem cartItem)
    {
        Quantity = cartItem.Quantity;
        ProductId = cartItem.ProductId;
    }

    public CartItemDetails ToCartItemDetails(Product product)
    {
        var totalAmountWithDiscount = CalculateTotal(product.Price);

        TotalAmountWithDiscount = totalAmountWithDiscount;
        UnitPrice = product.Price;
        Product = product;
        TotalDiscounts = totalAmountWithDiscount - Quantity * product.Price;
        TotalAmount = Quantity * product.Price;

        return this;
    }

    private decimal CalculateTotal(decimal unitPrice)
    {
        decimal discount = 0;

        if (Quantity > 20)
            throw new DomainException("Maximum limit of 20 items per product");

        if (Quantity >= 4 && Quantity <= 20)
        {
            if (Quantity >= 10)
                discount = 0.20m; 
            else
                discount = 0.10m;
        }

        return Quantity * unitPrice * (1 - discount);
    }
}