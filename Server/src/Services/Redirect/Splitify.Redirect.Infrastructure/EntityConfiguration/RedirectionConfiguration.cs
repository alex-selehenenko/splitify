using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure.EntityConfiguration.Abstractions;

namespace Splitify.Redirect.Infrastructure.EntityConfiguration
{
    internal class RedirectionConfiguration : EntityConfigurationBase<RedirectAggregate>
    {
        public override void Configure(EntityTypeBuilder<RedirectAggregate> builder)
        {
            base.Configure(builder);
            
            builder.HasMany(e => e.Destinations)
                .WithOne()
                .HasForeignKey("RedirectionId");
        }
    }
}
