using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

public record GetAllSaleResult(PagedResult<GetSaleResult> Sales)
{
    // for automapper
    public GetAllSaleResult() : this(new PagedResult<GetSaleResult>())
    {

    }
};