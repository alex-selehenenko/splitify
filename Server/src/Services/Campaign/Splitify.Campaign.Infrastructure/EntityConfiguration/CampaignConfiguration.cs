using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Campaign.Domain;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Campaign.Infrastructure.EntityConfiguration
{
    internal class CampaignConfiguration : EntityConfigurationBase<CampaignAggregate>
    {
        public override void Configure(EntityTypeBuilder<CampaignAggregate> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.IsRunning)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Property(x => x.UserId)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Links)
                .WithOne()
                .HasForeignKey("CampaignId")
                .Metadata.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
