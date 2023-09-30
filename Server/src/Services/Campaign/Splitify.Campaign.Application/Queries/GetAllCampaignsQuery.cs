using MediatR;
using Splitify.Campaign.Application.Queries.Models;

namespace Splitify.Campaign.Application.Queries
{
    public class GetAllCampaignsQuery : IRequest<IEnumerable<CampaignResponseModel>>
    {
    }
}
