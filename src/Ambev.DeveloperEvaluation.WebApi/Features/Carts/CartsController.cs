using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController : MainController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CartsController> _logger;

    public CartsController(IMediator mediator, IMapper mapper, ILogger<CartsController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCartAsync([FromBody] CreateCartRequest request)
    {
        var command = _mapper.Map<CreateCartCommand>(request);
        var result = await _mediator.Send(command);

        return Created(nameof(GetCartAsync),
            new { id = result.Id }, _mapper.Map<CreateCartResponse>(result));
    }

    [HttpGet("{id:guid}", Name = nameof(GetCartAsync))]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartAsync(Guid id)
    {
        var command = _mapper.Map<GetCartCommand>(GetCartRequest.Create(id));
        var result = await _mediator.Send(command);

        return Ok(new ApiResponseWithData<GetCartResponse>()
        {
            Data = _mapper.Map<GetCartResponse>(result),
            Success = true,
            Message = "Cart retrieved successfully"
        });
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCartAsync(Guid id)
    {
        var request = DeleteCartRequest.Create(id);

        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<DeleteCartCommand>(request);
        var result = await _mediator.Send(command);

        return Ok(new ApiResponse()
        {
            Success = result.Success,
            Message = "Cart deleted successfully"
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCartsAsync(GetAllCartRequest request)
    {
        var validator = new GetAllCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<GetAllCardCommand>(request);
        var result = await _mediator.Send(command);

        return OkPaginated(new PaginatedList<GetCartResponse>(
            _mapper.Map<List<GetCartResponse>>(result.Carts.Items),
            result.Carts.TotalCount,
            result.Carts.CurrentPage,
            result.Carts.PageSize));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCartAsync(Guid id,UpdateCartRequest request)
    {
        var validator = new UpdateCartRequestValidator();

        var validationResult = await validator.ValidateAsync(request.IncludeId(id));

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<UpdateCartCommand>(request);

        var result = await _mediator.Send(command);
        var map = _mapper.Map<UpdateCartResponse>(result);

        return Ok(new ApiResponseWithData<UpdateCartResponse>()
        {
            Data = map,
            Success = true,
            Message = "Cart updated successfully"
        });
    }
}