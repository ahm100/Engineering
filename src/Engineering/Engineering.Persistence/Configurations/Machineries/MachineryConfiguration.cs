using Engineering.Domain.Entities.Machineries;

namespace Engineering.Persistence.Configurations.Machineries;

public class MachineryConfiguration : IEntityTypeConfiguration<Machinery>
{
    private const string TableName = "Machineries";
    public void Configure(EntityTypeBuilder<Machinery> builder)
    {
        builder.MetaActiveConfiguration<Machinery, long>(TableName);

        builder.Property(oo => oo.MachineryName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.MachineryCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.HasOne(oo => oo.MachineriesGroup)
            .WithMany(oo => oo.Machineries)
            .HasForeignKey("GroupId")
            .HasPrincipalKey(nameof(MachineriesGroup.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}