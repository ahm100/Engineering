using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails.ConsumableVolumes;

public class ConsumableVolumeProductConfiguration : IEntityTypeConfiguration<ConsumableVolumeProduct>
{
    private const string _tableName = "ProjectOperationDetailConsumableVolumeProducts";
    public void Configure(EntityTypeBuilder<ConsumableVolumeProduct> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ProductGroupId)
            .IsRequired();

        builder.Property(oo => oo.FinalValue)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(oo => oo.IsStandard)
            .IsRequired();

        builder.Property(oo => oo.VolumeProductType)
            .HasDefaultValue(VolumeProductType.ProductGroup)
            .IsRequired();

        builder.Property(oo => oo.StandardValue)
            .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ConsumableVolumeProducts)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}