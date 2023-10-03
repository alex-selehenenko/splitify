using MediatR;
using Resulty;
using Splitify.Campaign.Domain;
using Splitify.Shared.Services.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Application.Commands
{
    internal class DeleteCampaignCommandHandler
        : IRequestHandler<DeleteCampaignCommand, Result>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IDateTimeService _dateTimeService;

        public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository, IDateTimeService dateTimeService)
        {
            _campaignRepository = campaignRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(request.CampaignId);

            if ( campaign is null)
            {
                return Result.Success();
            }

            campaign.Delete(_dateTimeService);
            
            _campaignRepository.Remove(campaign);
            await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}
