using AutoMapper;
using ElectronicShopping.Api.Features.Cart.Commands;
using ElectronicShopping.Api.Features.Cart.Models;
using ElectronicShopping.Api.Models;
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
        private readonly UserModel _userModel;
        private readonly IMapper _mapper;
        public CartController(
            IMediator mediator,
            UserModel userModel,
            IMapper mapper)
        {
            _mediator = mediator;
            _userModel = userModel;
            _mapper = mapper;
        }

        [HttpPost("add-product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestModel model)
        {
            var command = _mapper.Map<AddProductCommand>(model);
            command.UserId = _userModel.Id;

            return Ok(await _mediator.Send(command));
        }

        [HttpPut("update-product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequestModel model)
        {
            var command = _mapper.Map<UpdateProductCommand>(model);
            command.UserId = _userModel.Id;

            return Ok(await _mediator.Send(command));
        }
    }
}
