using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.Identity.Application.Commands;
using Splitify.Identity.Application.Queries;
using Splitify.Shared.AspDotNet.Results;

namespace Splitify.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "registered,verified")]
        [HttpPatch("verificationCode")]
        public async Task<IActionResult> PostVerificationCode()
        {
            return await _mediator.Send(new SendNewVerificationCodeCommand())
                .MapAsync(Ok, CreateProblemResponse);
        }


        [Authorize(Roles = "registered,verified")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetUserQuery());
            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized();
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}
