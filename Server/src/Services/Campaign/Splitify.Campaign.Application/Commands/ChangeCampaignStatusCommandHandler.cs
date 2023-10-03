using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Campaign.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Application.Commands
{
    public class ChangeCampaignStatusCommandHandler : IRequestHandler<ChangeCampaignStatusCommand, Result>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IDateTimeService _dateTimeService;

        public ChangeCampaignStatusCommandHandler(IDateTimeService dateTimeService, ICampaignRepository campaignRepository)
        {
            _dateTimeService = dateTimeService;
            _campaignRepository = campaignRepository;
        }

        public async Task<Result> Handle(ChangeCampaignStatusCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(request.CampaignId);
            if (campaign is null)
            {
                return Result.Failure(DomainError.ResourceNotFound(detail: $"Campaign was not found - {request.CampaignId}"));
            }

            var result = request.Status switch
            {
                CampaignStatus.Inactive => campaign.Deactivate(_dateTimeService),
                CampaignStatus.Active => campaign.Activate(_dateTimeService),
                _ => Result.Failure(DomainError.ValidationError(detail: "Invalid status"))
            };

            if (result.IsFailure)
            {
                return result;
            }

            await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
