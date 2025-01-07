using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleModifiedEvent(
    int Id,
    Guid UserId,
    DateTime SaleDate,
    List<SaleItem> SaleItems,
    decimal TotalSaleAmount,
    decimal TotalSaleDiscount,
    string Branch,
    bool IsCanceled);