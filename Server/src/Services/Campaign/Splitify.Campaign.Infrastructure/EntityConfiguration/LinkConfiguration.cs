using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Campaign.Domain;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Campaign.Infrastructure.EntityConfiguration
{
    internal class LinkConfiguration : EntityConfigurationBase<Link>
    {
        public override void Configure(EntityTypeBuilder<Link> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Url)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
