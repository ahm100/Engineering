using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Persistence.Configurations.MachineTypes;

public class MachineTypeConfiguration : IEntityTypeConfiguration<MachineType>
{
    private const string _tableName = "MachineTypes";
    public void Configure(EntityTypeBuilder<MachineType> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.MachineTypeCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.FromWeight)
            .IsRequired();

        builder.Property(oo => oo.UntilWeight)
            .IsRequired();

        builder.Property(oo => oo.MachineTypeTitle)
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

        builder.HasOne(oo => oo.CabinType)
            .WithMany(oo => oo.MachineTypes)
            .HasForeignKey(x => x.CabinTypeId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}