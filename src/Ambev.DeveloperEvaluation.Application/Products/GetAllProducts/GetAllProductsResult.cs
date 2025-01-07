using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public record GetAllProductsResult(PagedResult<GetProductResult> Products)
{
    // for automapper
    public GetAllProductsResult() : this(new PagedResult<GetProductResult>())
    {

    }
}