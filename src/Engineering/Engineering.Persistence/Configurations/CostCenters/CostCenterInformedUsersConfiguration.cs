using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterInformedUsersConfiguration : IEntityTypeConfiguration<CostCenterInformedUser>
{
    private const string TableName = "InformedUsers";
    public void Configure(EntityTypeBuilder<CostCenterInformedUser> builder)
    {
        builder.MetaConfiguration<CostCenterInformedUser, long>(TableName);

        builder.Property(oo => oo.EmployeeId)
            .HasComment(CCenterCmts.EmployeeId)
            .IsRequired();

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.InformedUsers)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}