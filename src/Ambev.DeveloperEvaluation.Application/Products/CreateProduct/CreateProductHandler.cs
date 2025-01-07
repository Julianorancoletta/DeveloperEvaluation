using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {CreateProductHandler} triggered to handle {CreateProductCommand}",
            nameof(CreateProductHandler), nameof(CreateProductCommand));

        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"Validation failed for {nameof(CreateProductCommand)}");

            throw new ValidationException(validationResult.Errors);
        }

        var product = _mapper.Map<Product>(command);
        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        return _mapper.Map<CreateProductResult>(createdProduct);
    }
}