namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public record Rating(decimal Rate, int Count)
{
    public static Rating Create(decimal rate, int count) => new Rating(rate, count);
}
