using MediatR;
using Resulty;
using Splitify.Campaign.Application.Commands.Models;
using Splitify.Campaign.Domain;
using Splitify.Campaign.Domain.Factories;
using Splitify.Shared.Services.Identity;
using Splitify.Shared.Services.Misc;

namespace Splitify.Campaign.Application.Commands
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Result<CampaignModel>>
    {
        private const int CampaignIdLength = 10;

        private readonly IRandomStringService _randomStringService;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUserService _userService;
        private readonly IDateTimeService _dateTimeService;

        public CreateCampaignCommandHandler(
            IRandomStringService randomStringService,
            ICampaignRepository campaignRepository,
            IUserService userService,
            IDateTimeService dateTimeService)
        {
            _randomStringService = randomStringService;
            _campaignRepository = campaignRepository;
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result<CampaignModel>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var links = new List<Link>();
            foreach (var url in request.Destinations)
            {
                var linkCreationResult = LinkFactory.Create(url, _dateTimeService);
                if (linkCreationResult.IsFailure)
                {
                    return Result.Failure<CampaignModel>(linkCreationResult.Error);
                }

                links.Add(linkCreationResult.Value);
            }

            string campaignId;
            do
            {
                campaignId = _randomStringService.Generate(CampaignIdLength);
            }
            while (await _campaignRepository.ExistsAsync(campaignId, cancellationToken));

            var campaignCreationResult = CampaignFactory.Create(campaignId, request.Name, _userService.GetUserId(), links, _dateTimeService);

            if (campaignCreationResult.IsFailure)
            {
                return Result.Failure<CampaignModel>(campaignCreationResult.Error);
            }

            var campaign = campaignCreationResult.Value;

            _campaignRepository.Add(campaign);
            await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new CampaignModel(campaign.Id));
        }
    }
}
