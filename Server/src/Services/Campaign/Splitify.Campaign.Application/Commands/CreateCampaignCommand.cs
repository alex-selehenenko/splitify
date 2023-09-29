using MediatR;
using Resulty;
using Splitify.Campaign.Application.Models;

namespace Splitify.Campaign.Application.Commands
{
    public record CreateCampaignCommand(string[] Urls) : IRequest<Result<CampaignModel>>;
}
