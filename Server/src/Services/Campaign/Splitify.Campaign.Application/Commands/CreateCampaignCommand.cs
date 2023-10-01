using MediatR;
using Resulty;
using Splitify.Campaign.Application.Commands.Models;

namespace Splitify.Campaign.Application.Commands
{
    public record CreateCampaignCommand(string Name, string[] Destinations) : IRequest<Result<CampaignModel>>;
}
