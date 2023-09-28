using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.Campaign.Application.Commands;
using Splitify.Shared.AspDotNet.Results;

namespace Splitify.Campaign.Api.Controllers
{
    [ApiController]
    [Route("api/v1/campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCampaignCommand body)
        {
            return await _mediator.Send(body)
                .MapAsync(
                    result => Created(result.CampaignId, result),
                    error => CreateProblemResponse(error));
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}