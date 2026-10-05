using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterHistoryConfiguration : IEntityTypeConfiguration<CostCenterHistory>
{
    private const string TableName = "CostCenterHistories";
    public void Configure(EntityTypeBuilder<CostCenterHistory> builder)
    {
        builder.MetaActiveConfiguration<CostCenterHistory, long>(TableName);

        builder.Property(oo => oo.CostCenterCode)
            .HasComment(CCenterCmts.CostCenterCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CostCenterName)
            .HasComment(CCenterCmts.CostCenterName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CostCenterEnName)
            .HasComment(CCenterCmts.CostCenterEnName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.DescriptionEn)
             .HasComment(GlobalCmts.Description)
             .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.CityId)
            .HasComment(GlobalCmts.CityId)
            .IsRequired();

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId);

        builder.Property(oo => oo.Address)
            .HasComment(CCenterCmts.Address)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.Property(oo => oo.PostalCode)
            .HasComment(CCenterCmts.PostalCode)
            .HasColumnType("nvarchar(10)");

        builder.Property(oo => oo.Latitude)
            .HasComment(CCenterCmts.Latitude)
            .HasColumnType("decimal(18,9)")
            .IsRequired();

        builder.Property(oo => oo.NoOperationDays)
            .HasComment(CCenterCmts.NoOperationDays);

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Longitude)
            .HasComment(CCenterCmts.Longitude)
            .HasColumnType("decimal(18,9)")
            .IsRequired();

        builder.Property(oo => oo.WeatherState)
            .HasComment(CCenterCmts.WeatherState)
            .HasDefaultValue(false);

        builder.Property(oo => oo.PreferentialReferenceCode)
            .HasComment(CCenterCmts.PreferentialReferenceCode)
            .HasComment("کد مرجع تفصیلی")
            .IsRequired();

        builder.HasOne(oo => oo.CostCenterType)
            .WithMany(oo => oo.CostCenterHistories)
            .HasForeignKey(x => x.CostCenterTypesId)
            .HasPrincipalKey(nameof(CostCenterType.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.CostCenterHistories)
            .HasForeignKey(x => x.CostCenterId)
            .HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(oo => oo.IsDefault)
            .HasComment(CCenterCmts.IsDefault)
            .HasDefaultValue(false)
            .IsRequired();
    }

}
