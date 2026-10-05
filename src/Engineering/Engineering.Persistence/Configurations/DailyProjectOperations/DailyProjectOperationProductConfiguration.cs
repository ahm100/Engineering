using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationProductConfiguration : IEntityTypeConfiguration<DailyProjectOperationProduct>
{
    private const string _tableName = "DailyProjectOperationProducts";
    public void Configure(EntityTypeBuilder<DailyProjectOperationProduct> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.FinalValue)
            .IsRequired();

        builder.Property(oo => oo.ProductId)
            .IsRequired();

        builder.Property(oo => oo.UnusedValue)
            .HasColumnType("decimal(18,5)");

        builder.HasOne(c => c.ConsumableVolumeProduct)
               .WithMany(c => c.DailyOperationProducts)
               .HasForeignKey("ConsumableVolumeProductId")
               .HasPrincipalKey(nameof(ConsumableVolumeProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
