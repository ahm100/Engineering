using Engineering.Domain.Entities.BillOfLadings;

namespace Engineering.Persistence.Configurations.BillOfLadings;

public class BillOfLadingConfiguration : IEntityTypeConfiguration<BillOfLading>
{
    private const string TableName = "BillOfLadings";
    public void Configure(EntityTypeBuilder<BillOfLading> builder)
    {
        builder.MetaActiveConfiguration<BillOfLading, long>(TableName);

        builder.Property(oo => oo.BillOfLadingName)
            .HasComment(BillOfLadingCmts.Name)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.BillOfLadingCode)
            .HasComment(BillOfLadingCmts.Code)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

    }
}
