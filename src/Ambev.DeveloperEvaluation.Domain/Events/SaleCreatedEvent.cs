using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleCreatedEvent(
    int Id ,
    Guid UserId,
    DateTime SaleDate,
    List<SaleItem> SaleItems,
    decimal TotalSaleAmount,
    bool IsCanceled,
    decimal TotalSaleDiscount,
    string Branch);