using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.OperationInfos;

public class OperationInfoConfiguration : IEntityTypeConfiguration<OperationInfo>
{
    private const string _tableName = "OperationInfos";
    public void Configure(EntityTypeBuilder<OperationInfo> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.OperationInfoName)
            .HasColumnType("nvarchar(max)")
            .HasComment(OperationInfoCmts.OperationInfoName)
            .IsRequired();

        builder.Property(oo => oo.OperationInfoCode)
            .HasColumnType("nvarchar(100)")
            .HasComment(OperationInfoCmts.OperationInfoCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.UnitOfMeasurementId)
            .HasComment(OperationInfoCmts.UnitOfMeasurementId)
            .IsRequired();

        builder.Property(oo => oo.BasePrice)
            .HasComment(OperationInfoCmts.BasePrice)
            .IsRequired()
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.CompanyId)
        .HasComment(GlobalCmts.CompanyId);

        builder.Property(oo => oo.OperationLatinName)
            .HasComment(OperationInfoCmts.OperationLatinName)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.Priority)
        .HasComment(OperationInfoCmts.Priority);

        builder.Property(oo => oo.IsActive)
            .HasComment(GlobalCmts.IsActive)
            .IsRequired();

        builder.Property(oo => oo.IsPriceList)
            .HasComment(OperationInfoCmts.IsPriceList)
            .IsRequired();

        builder.Property(oo => oo.HasChanged)
            .HasComment(OperationInfoCmts.HasChanged)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasComment(GlobalCmts.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created).ValueGeneratedOnAdd().IsRequired()
        .HasComment(GlobalCmts.Created);

        builder.Property(oo => oo.CreatorId).IsRequired()
        .HasComment(GlobalCmts.CreatorId);

        builder.Property(oo => oo.YearId);

    }
}