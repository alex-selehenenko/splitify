using Splitify.BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Domain.Events
{
    public record CampaignActivatedDomainEvent(DateTime OccuredAt,string CampaignId) : IDomainEvent;
}
