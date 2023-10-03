using MediatR;
using Resulty;

namespace Splitify.Campaign.Application.Commands
{
    public record DeactivateCampaignCommand(string CampaignId) : IRequest<Result>;
}
