using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterVirtualGroupConfiguration : IEntityTypeConfiguration<CostCenterVirtualGroup>
{
    private const string TableName = "CostCenterVirtualGroups";
    public void Configure(EntityTypeBuilder<CostCenterVirtualGroup> builder)
    {
        builder.MetaConfiguration<CostCenterVirtualGroup, long>(TableName);

        builder.Property(oo => oo.Title)
            .HasComment(CCenterCmts.Title)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.Link)
            .HasColumnType("nvarchar(1500)")
            .HasComment(CCenterCmts.Link)
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(oo => oo.Identifier)
            .HasComment(CCenterCmts.Identifier)
            .IsRequired();

        builder.Property(oo => oo.SendToday)
            .HasComment(CCenterCmts.SendToday)
            .IsRequired();

        builder.Property(oo => oo.SendYesterday)
            .HasComment(CCenterCmts.SendYesterday)
            .IsRequired();

        builder.HasOne(c => c.CostCenter)
               .WithMany(c => c.Groups)
               .HasForeignKey("CostCenterId")
               .HasPrincipalKey(nameof(CostCenter.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
