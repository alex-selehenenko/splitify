using MediatR;
using Resulty;
using Splitify.Campaign.Application.Queries.Models;

namespace Splitify.Campaign.Application.Queries
{
    public record GetCampaignQuery(string Id) : IRequest<Result<CampaignResponseModel>>;
}
