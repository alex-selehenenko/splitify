using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.Identity.Application.Commands;
using Splitify.Shared.AspDotNet.Results;

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
            return await _mediator.Send(body)
                .MapAsync(Ok, CreateProblemResponse);
        }

        [Authorize(Roles = "registered")]
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAsync([FromBody] VerifyUserCommand body)
        {
            return await _mediator.Send(body)
                .MapAsync(Ok, CreateProblemResponse);
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}