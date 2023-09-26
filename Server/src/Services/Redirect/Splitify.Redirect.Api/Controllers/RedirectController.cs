using MediatR;
using Microsoft.AspNetCore.Mvc;
using Splitify.Redirect.Application.Commands;

namespace Splitify.Redirect.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RedirectController> _logger;

        public RedirectController(ILogger<RedirectController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[id]")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var isUniqueVisitor = HttpContext.Request.Cookies.ContainsKey(id);

            return isUniqueVisitor
                ? await RedirectUniqueVisitorAsync(id)
                : await RedirectExistingVisitorAsync();
        }

        private async Task<IActionResult> RedirectUniqueVisitorAsync(string redirectionId)
        {
            var result = await _mediator.Send(new RedirectUniqueVisitorCommand(redirectionId));
            return Ok();
        }

        private async Task<IActionResult> RedirectExistingVisitorAsync()
        {
            await Task.CompletedTask;
            return Redirect("https://google.com");
        }
    }
}