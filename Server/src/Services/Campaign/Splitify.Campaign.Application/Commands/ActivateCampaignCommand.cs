using MediatR;
using Resulty;

namespace Splitify.Campaign.Application.Commands
{
    public record ActivateCampaignCommand(string CampaignId) : IRequest<Result>;
}
