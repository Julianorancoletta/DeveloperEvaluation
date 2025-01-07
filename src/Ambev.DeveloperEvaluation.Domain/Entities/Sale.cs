using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public int Id { get;  set; } 
    public Guid UserId { get; private set; } 
    public DateTime SaleDate { get; private set; } 
    public List<SaleItem> SaleItems { get; private set; } = []; 
    public decimal TotalSaleAmount { get; private set; } 
    public bool IsCanceled { get; private set; } 

    public decimal TotalSaleDiscount { get; private set; } 
    public string Branch { get; private set; } 

    public Sale() { }

    public void BuildSale(IReadOnlyCollection<CartItemDetails> cartItems, IReadOnlyCollection<Product> products, string branch,
        Guid userId)
    {
        foreach (var item in cartItems)
        {
            var product = GetProductById(item.ProductId); // Fetch product details (example, price)
            var saleItem = SaleItem.Create(product.Id, item.Quantity, product.Price, item.TotalAmountWithDiscount,
                item.TotalAmount);
            SaleItems.Add(saleItem);
            TotalSaleAmount += saleItem.TotalAmountWithDiscount;
        }

        SaleDate = DateTime.Now;
        Branch = branch;
        UserId = userId;

        // calculate total discount by looking at the total discount of each item
        TotalSaleDiscount = cartItems.Sum(x => (x.UnitPrice * x.Quantity) - x.TotalAmountWithDiscount!);

        Product GetProductById(Guid productId) => products.FirstOrDefault(p => p.Id == productId)!;
        
    }

    public void CancelSale()
    {
        if (IsCanceled)
            throw new DomainException("Sale already canceled");
        

        IsCanceled = true;
    }

    // update sale
    public void UpdateSale(IReadOnlyCollection<CartItemDetails> cartItemDetails, IReadOnlyCollection<Product> products,
        string branch, Guid userId)
    {
        if (IsCanceled)
            throw new DomainException("Sale already canceled");
        

        SaleItems.Clear();
        TotalSaleAmount = 0;
        TotalSaleDiscount = 0;

        foreach (var cartItem in cartItemDetails)
        {
            var product = GetProductById(cartItem.ProductId); // Fetch product details (example, price)
            var saleItem = SaleItem.Create(product.Id, cartItem.Quantity, product.Price, cartItem.TotalAmountWithDiscount,
                cartItem.TotalAmount);
            SaleItems.Add(saleItem);
            TotalSaleAmount += saleItem.TotalAmountWithDiscount;
        }

        SaleDate = DateTime.Now;
        Branch = branch;
        UserId = userId;

        TotalSaleDiscount = cartItemDetails.Sum(x => (x.UnitPrice * x.Quantity) - x.TotalAmountWithDiscount!);

        Product GetProductById(Guid productId) => products.FirstOrDefault(p => p.Id == productId)!;
    }
}

