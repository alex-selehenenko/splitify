using MediatR;
using Resulty;

namespace Splitify.Campaign.Application.Commands
{
    public record RunCampaignCommand(string CampaignId) : IRequest<Result>;
}
