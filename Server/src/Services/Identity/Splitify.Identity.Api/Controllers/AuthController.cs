using MediatR;
using Microsoft.AspNetCore.Mvc;
using Splitify.Identity.Application.Commands;

namespace Splitify.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand body)
        {
            var result = await _mediator.Send(body);
            return Ok(result.Value);
        }
    }
}