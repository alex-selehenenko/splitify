using MediatR;
using Microsoft.AspNetCore.Mvc;
using Splitify.Shared.AspDotNet.Results;
using Resulty;
using Splitify.Identity.Api.Controllers.Dto;
using Splitify.Identity.Application.Commands;

namespace Splitify.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/v1/password")]
    public class PasswordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public PasswordController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("token/reset")]
        public async Task<IActionResult> PostResetPasswordToken([FromBody] ResetPasswordTokenPost body)
        {
            var baseResetUrl = _configuration["BaseResetUrl"];
            var command = new SendResetPasswordTokenCommand(body.Email, baseResetUrl);

            return await _mediator.Send(command)
                .MapAsync(Ok, CreateProblemResponse);
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}
