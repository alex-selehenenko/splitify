using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.Campaign.Api.Controllers.Dto;
using Splitify.Campaign.Application.Commands;
using Splitify.Campaign.Application.Queries;
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

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var campaigns = await _mediator.Send(new GetAllCampaignsQuery());
            
            return Ok(campaigns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return await _mediator.Send(new GetCampaignQuery(id))
                .MapAsync(Ok, CreateProblemResponse);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(string id, [FromBody] CampaignPatch body)
        {
            return await _mediator.Send(new ChangeCampaignStatusCommand(id, body.Status))
                .MapAsync(Ok, CreateProblemResponse);
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}