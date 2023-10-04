using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Splitify.Identity.Domain;
using Splitify.Shared.AspDotNet.EntityFramework;

namespace Splitify.Identity.Infrastructure.EntityConfiguration
{
    internal class UserConfiguration : EntityConfigurationBase<UserAggregate>
    {
        public override void Configure(EntityTypeBuilder<UserAggregate> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Verified)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Property(x => x.Email)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.OwnsOne(x => x.Password, p =>
            {
                p.Property(p => p.Salt);
                p.Property(p => p.Hash);
            });

            builder.OwnsOne(x => x.VerificationCode, c =>
            {
                c.Property(c => c.CreatedAt);
                c.Property(c => c.Code);
            });
        }
    }
}
