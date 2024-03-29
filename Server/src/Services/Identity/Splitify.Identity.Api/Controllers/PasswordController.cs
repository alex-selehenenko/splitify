﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Splitify.Shared.AspDotNet.Results;
using Resulty;
using Splitify.Identity.Api.Controllers.Dto;
using Splitify.Identity.Application.Commands;
using Splitify.Identity.Application.Queries;
using Splitify.Identity.Api.Validators;

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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateNewPasswordCommand body)
        {
            var validation = Validator.ValidatePassword(body.Password);

            if (validation.IsFailure)
            {
                return CreateProblemResponse(validation.Error);
            }

            return await _mediator
                .Send(body)
                .MapAsync(Ok, CreateProblemResponse);
        }

        [HttpGet("token/{token}")]
        public async Task<IActionResult> GetResetPasswordToken(string token)
        {
            return await _mediator
                .Send(new GetResetPasswordTokenQuery(token))
                .MapAsync(Ok, CreateProblemResponse);
        }

        [HttpPost("token/reset")]
        public async Task<IActionResult> PostResetPasswordToken([FromBody] ResetPasswordTokenPost body)
        {
            var baseResetUrl = _configuration["ResetPasswordUrl"];
            var command = new SendResetPasswordTokenCommand(body.Email, baseResetUrl);

            _ = await _mediator.Send(command);
            
            return Ok();
        }

        private IActionResult CreateProblemResponse(Error error)
        {
            var problemDetails = error.ToProblemDetails();
            return StatusCode(problemDetails.Status ?? 500, problemDetails);
        }
    }
}
