using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterTypeConfiguration : IEntityTypeConfiguration<CostCenterType>
{
    private const string TableName = "CostCenterTypes";
    public void Configure(EntityTypeBuilder<CostCenterType> builder)
    {
        builder.MetaActiveConfiguration<CostCenterType, long>(TableName);

        builder.Property(oo => oo.CostCenterTypeCode)
            .HasComment(CCenterCmts.CostCenterTypeCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.CostCenterTypeTitle)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(oo => oo.CompanyId);
    }
}