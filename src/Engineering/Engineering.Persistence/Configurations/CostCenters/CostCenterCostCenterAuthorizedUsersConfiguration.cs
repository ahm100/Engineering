using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterCostCenterAuthorizedUsersConfiguration : IEntityTypeConfiguration<CostCenterAuthorizedUser>
{
    private const string TableName = "CostCenterAuthorizedUsers";
    public void Configure(EntityTypeBuilder<CostCenterAuthorizedUser> builder)
    {
        builder.MetaConfiguration<CostCenterAuthorizedUser, long>(TableName);

        builder.Property(oo => oo.AuthorizedUserId)
            .HasComment(CCenterCmts.AuthorizedUserId)
            .IsRequired();

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.CostCenterAuthorizedUsers)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}