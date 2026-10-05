using Engineering.Domain.Entities.Machineries;

namespace Engineering.Persistence.Configurations.Machineries;

public class MachineriesGroupConfiguration : IEntityTypeConfiguration<MachineriesGroup>
{
    private const string TableName = "MachineriesGroup";
    public void Configure(EntityTypeBuilder<MachineriesGroup> builder)
    {
        builder.MetaActiveConfiguration<MachineriesGroup, long>(TableName);

        builder.Property(oo => oo.GroupName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.GroupCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();
        builder.Property(oo => oo.CompanyId);
    }
}
