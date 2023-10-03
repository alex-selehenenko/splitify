using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Campaign.Application.Queries.Models;
using Splitify.Campaign.Domain;

namespace Splitify.Campaign.Application.Queries
{
    public class GetCampaignQueryHandler
        : IRequestHandler<GetCampaignQuery, Result<CampaignResponseModel>>
    {
        ICampaignRepository _campaignRepository;

        public GetCampaignQueryHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Result<CampaignResponseModel>> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(request.Id, cancellationToken);

            return campaign is not null
                ? Result.Success(CampaignMapper.Map(campaign))
                : Result.Failure<CampaignResponseModel>(DomainError.ResourceNotFound());
        }
    }
}
