using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure.EntityConfiguration.Abstractions;

namespace Splitify.Redirect.Infrastructure.EntityConfiguration
{
    internal class RedirectionConfiguration : EntityConfigurationBase<Redirection>
    {
        public override void Configure(EntityTypeBuilder<Redirection> builder)
        {
            base.Configure(builder);
            
            builder.HasMany(e => e.Destinations)
                .WithOne()
                .HasForeignKey("RedirectionId");
        }
    }
}
