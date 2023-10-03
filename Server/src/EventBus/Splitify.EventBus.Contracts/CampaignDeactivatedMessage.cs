using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.EventBus.Contracts
{
    public record CampaignDeactivatedMessage(string CampaignId);
}
