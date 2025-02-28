using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteCartAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Cart>> GetAllCartsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        string? order = null,
        CancellationToken cancellationToken = default);
    Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);
}