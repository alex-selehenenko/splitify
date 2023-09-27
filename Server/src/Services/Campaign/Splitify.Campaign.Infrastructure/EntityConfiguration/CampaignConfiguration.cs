using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Campaign.Domain;
using Splitify.Shared.AspDotNet.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Infrastructure.EntityConfiguration
{
    internal class CampaignConfiguration : EntityConfigurationBase<CampaignAggregate>
    {
        public override void Configure(EntityTypeBuilder<CampaignAggregate> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.IsActive)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Links)
                .WithOne()
                .HasForeignKey("CampaignId");
        }
    }
}
