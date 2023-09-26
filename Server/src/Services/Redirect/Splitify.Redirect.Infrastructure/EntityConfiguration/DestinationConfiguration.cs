using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Infrastructure.EntityConfiguration.Abstractions;

namespace Splitify.Redirect.Infrastructure.EntityConfiguration
{
    internal class DestinationConfiguration : EntityConfigurationBase<Destination>
    {
        public override void Configure(EntityTypeBuilder<Destination> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.UniqueVisitors)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Property(e => e.Url)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
