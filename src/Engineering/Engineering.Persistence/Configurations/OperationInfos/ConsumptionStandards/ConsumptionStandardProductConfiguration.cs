using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;
using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Persistence.Configurations.OperationInfos.ConsumptionStandards;

public class ConsumptionStandardProductConfiguration : IEntityTypeConfiguration<ConsumptionStandardProduct>
{
    private const string TableName = "EngineeringStandardProducts";
    public void Configure(EntityTypeBuilder<ConsumptionStandardProduct> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ProductUnitId)
            .IsRequired();

        builder.Property(oo => oo.StandardProductType)
            .HasDefaultValue(StandardProductType.ProductGroup)
            .IsRequired();

        builder.Property(oo => oo.ProductAllowedType)
            .HasDefaultValue(ProductAllowedType.IsStandard)
            .IsRequired();

        builder.Property(oo => oo.Number)
                .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.UnusedPercentage)
            .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.ConsumptionStandardProduct)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(nameof(OperationInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}