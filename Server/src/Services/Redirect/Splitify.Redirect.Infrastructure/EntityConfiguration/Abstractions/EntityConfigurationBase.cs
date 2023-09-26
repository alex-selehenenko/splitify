using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Splitify.BuildingBlocks.Domain;

namespace Splitify.Redirect.Infrastructure.EntityConfiguration.Abstractions
{
    internal abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Property(e => e.UpdatedAt)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Ignore(entity => entity.Events);
        }
    }
}
