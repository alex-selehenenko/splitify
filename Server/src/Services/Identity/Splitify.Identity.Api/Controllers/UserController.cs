using MediatR;
using Splitify.Shared.AspDotNet.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.Identity.Application.Queries;

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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetUserQuery());
            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized();
        }
    }
}
