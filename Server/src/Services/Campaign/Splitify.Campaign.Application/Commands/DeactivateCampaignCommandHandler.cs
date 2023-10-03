using MediatR;
using Resulty;
using Splitify.Campaign.Domain;
using Splitify.Shared.Services.Misc;
using System.Reflection.Metadata.Ecma335;

namespace Splitify.Campaign.Application.Commands
{
    public class DeactivateCampaignCommandHandler
        : IRequestHandler<DeactivateCampaignCommand, Result>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IDateTimeService _dateTimeService;

        public DeactivateCampaignCommandHandler(ICampaignRepository campaignRepository, IDateTimeService dateTimeService)
        {
            _campaignRepository = campaignRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result> Handle(DeactivateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(request.CampaignId);
            
            if (campaign is null)
            {
                return Result.Success();
            }

            var result = campaign.Deactivate(_dateTimeService);

            if (result.IsSuccess)
            {
                await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
    }
}
