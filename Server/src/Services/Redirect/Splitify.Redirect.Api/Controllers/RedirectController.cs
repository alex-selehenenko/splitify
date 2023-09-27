using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.Redirect.Application.Commands;
using Splitify.Redirect.Application.Models;
using Splitify.Shared.AspDotNet.Results;

namespace Splitify.Redirect.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RedirectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var isUniqueVisitor = HttpContext.Request.Cookies.ContainsKey(id);

            return !isUniqueVisitor
                ? await RedirectUniqueVisitorAsync(id)
                : await RedirectExistingVisitorAsync();
        }

        private async Task<IActionResult> RedirectUniqueVisitorAsync(string redirectionId)
        {
            return await _mediator.Send(new RedirectUniqueVisitorCommand(redirectionId))
                .MapAsync(
                    data => CreateRedirectResponseForUniqueVisitor(redirectionId, data),
                    error => CreateProblemResponse(error));
        }

        private IActionResult CreateRedirectResponseForUniqueVisitor(string redirectionId, DestinationModel destination)
        {
            HttpContext.Response.Cookies.Append(redirectionId, destination.Id, new() { HttpOnly = true });
            IActionResult response = Redirect(destination.Url);

            return response;
        }

        private async Task<IActionResult> RedirectExistingVisitorAsync()
        {
            await Task.CompletedTask;
            return Redirect("https://google.com");
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}