using MediatR;
using Resulty;
using Splitify.Campaign.Domain;

namespace Splitify.Campaign.Application.Commands
{
    public record ChangeCampaignStatusCommand(string CampaignId, CampaignStatus Status) : IRequest<Result>;
}
