using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.OperationInfos;

public class OperationInfoGroupConfiguration : IEntityTypeConfiguration<OperationInfoGroup>
{
    private const string _tableName = "OperationInfoGroups";
    public void Configure(EntityTypeBuilder<OperationInfoGroup> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.OperationInfoGroupCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.OperationInfoGroupTitle)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();
    }
}