using ElectronicShopping.Api.Features.Cart.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Cart
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
