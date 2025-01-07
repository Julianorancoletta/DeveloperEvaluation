using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleCreatedConsumer
{
    private readonly ILogger<SaleCreatedConsumer> _logger;

    public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(SaleCreatedEvent context)
    {
        _logger.LogInformation("{SaleCreatedEvent} received with id: {SaleId}", nameof(SaleCreatedEvent), context);
    }
}