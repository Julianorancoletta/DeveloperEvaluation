using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleCancelledConsumer 
{
    private readonly ILogger<SaleCancelledConsumer> _logger;

    public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(SaleCancelledEvent context)
    {
        _logger.LogInformation("{SaleCancelledEvent} received with id: {SaleId}", nameof(SaleCancelledEvent), context);
    }
}