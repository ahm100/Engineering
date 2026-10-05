using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterVirtualGroupAdminConfiguration : IEntityTypeConfiguration<CostCenterVirtualGroupAdmin>
{
    private const string TableName = "CostCenterVirtualGroupAdmins";
    public void Configure(EntityTypeBuilder<CostCenterVirtualGroupAdmin> builder)
    {
        builder.MetaConfiguration<CostCenterVirtualGroupAdmin, long>(TableName);

        builder.Property(oo => oo.ThirdPartyId)
            .HasComment(CCenterCmts.ThirdPartyId)
            .IsRequired();

        builder.Property(oo => oo.UserName)
            .HasComment(CCenterCmts.UserName)
            .IsRequired();

        builder.HasOne(c => c.CostCenterVirtualGroup)
               .WithMany(c => c.Admins)
               .HasForeignKey("CostCenterVirtualGroupId")
               .HasPrincipalKey(nameof(CostCenterVirtualGroup.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}

