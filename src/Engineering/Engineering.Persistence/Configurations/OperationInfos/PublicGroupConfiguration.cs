
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.PublicGroups;

public class PublicGroupConfiguration : IEntityTypeConfiguration<PublicGroup>
{
    private const string _tableName = "PublicGroups";
    public void Configure(EntityTypeBuilder<PublicGroup> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ProductGroupId)
            .IsRequired();
    }
}