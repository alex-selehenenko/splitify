using Splitify.BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Redirect.Domain.Events
{
    public record UniqueVisitorRegisteredDomainEvent(string DestinationId, DateTime OccuredAt) : IDomainEvent;
}
