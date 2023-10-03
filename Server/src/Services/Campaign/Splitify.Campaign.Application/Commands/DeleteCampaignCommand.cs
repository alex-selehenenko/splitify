using MediatR;
using Resulty;

namespace Splitify.Campaign.Application.Commands
{
    public record DeleteCampaignCommand(string CampaignId) : IRequest<Result>;
}
