using ElectronicShopping.Api.Features.Authenticate.Commands;
using ElectronicShopping.Api.Features.Authenticate.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Authenticate
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Post([FromForm] AuthenticateRequestModel model)
        {
            return Ok(await _mediator.Send(new AuthenticateCommand(model.username, model.password)));
        }
    }
}
