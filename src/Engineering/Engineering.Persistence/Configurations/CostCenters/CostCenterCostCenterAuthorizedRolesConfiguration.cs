using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterCostCenterAuthorizedRolesConfiguration : IEntityTypeConfiguration<CostCenterAuthorizedRole>
{
    private const string TableName = "CostCenterAuthorizedRoles";
    public void Configure(EntityTypeBuilder<CostCenterAuthorizedRole> builder)
    {
        builder.MetaConfiguration<CostCenterAuthorizedRole, long>(TableName);

        builder.Property(oo => oo.AuthorizedRoleId)
            .HasComment(CCenterCmts.AuthorizedRoleId)
            .IsRequired();

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.CostCenterAuthorizedRoles)
            .HasForeignKey("CostCenterId")
            .HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}