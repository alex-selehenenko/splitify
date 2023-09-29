using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Campaign.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Application.Commands
{
    public class ActivateCampaignCommandHandler : IRequestHandler<ActivateCampaignCommand, Result>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IDateTimeService _dateTimeService;

        public ActivateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Result> Handle(ActivateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(request.CampaignId);
            if (campaign is null)
            {
                return Result.Failure(DomainError.ResourceNotFound(detail: $"Campaign was not found - {request.CampaignId}"));
            }

            var result = campaign.Activate(_dateTimeService);
            if (result.IsFailure)
            {
                return result;
            }

            await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
