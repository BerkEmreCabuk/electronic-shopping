using ElectronicShopping.Api.Features.Item.Models;
using ElectronicShopping.Api.Features.Item.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Item
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ItemModel>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetItemsQuery(), _httpContextAccessor.HttpContext.RequestAborted));
        }
    }
}
