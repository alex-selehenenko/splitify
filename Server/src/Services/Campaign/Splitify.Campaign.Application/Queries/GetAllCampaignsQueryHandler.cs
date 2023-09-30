using MediatR;
using Splitify.Campaign.Application.Queries.Models;
using Splitify.Campaign.Domain;
using Splitify.Shared.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Application.Queries
{
    public class GetAllCampaignsQueryHandler
        : IRequestHandler<GetAllCampaignsQuery, IEnumerable<CampaignResponseModel>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUserService _userService;

        public GetAllCampaignsQueryHandler(IUserService userService, ICampaignRepository campaignRepository)
        {
            _userService = userService;
            _campaignRepository = campaignRepository;
        }

        public async Task<IEnumerable<CampaignResponseModel>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
        {
            var campaigns = await _campaignRepository.GetAllAsync(_userService.GetUserId(), cancellationToken);
            
            return campaigns.Select(x =>
                new CampaignResponseModel(
                    x.Id,
                    x.IsRunning,
                    x.Links.Select(l => l.Url).ToArray(),
                    x.CreatedAt)
                );
        }
    }
}
