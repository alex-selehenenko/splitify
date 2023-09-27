using MediatR;
using Resulty;
using Splitify.Campaign.Application.Models;

namespace Splitify.Campaign.Application.Commands
{
    public class CreateCampaignCommand : IRequest<Result<CampaignModel>>
    {
        public string[] Urls { get; }

        public CreateCampaignCommand(string[] urls)
        {
            Urls = urls;
        }
    }
}
