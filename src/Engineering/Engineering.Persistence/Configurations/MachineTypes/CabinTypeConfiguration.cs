using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Persistence.Configurations.MachineTypes;

public class CabinTypeConfiguration : IEntityTypeConfiguration<CabinType>
{
    private const string _tableName = "CabinTypes";
    public void Configure(EntityTypeBuilder<CabinType> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.CabinTypeCode)
            .IsRequired();

        builder.Property(oo => oo.CabinTypeName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();
    }
}