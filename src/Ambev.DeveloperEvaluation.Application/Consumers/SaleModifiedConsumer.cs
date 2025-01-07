using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleModifiedConsumer 
{
    private readonly ILogger<SaleModifiedConsumer> _logger;

    public SaleModifiedConsumer(ILogger<SaleModifiedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(SaleModifiedEvent context)
    {
        _logger.LogInformation("{SaleModifiedEvent} received with id: {SaleId}", nameof(SaleModifiedEvent),
            context);
    }
}