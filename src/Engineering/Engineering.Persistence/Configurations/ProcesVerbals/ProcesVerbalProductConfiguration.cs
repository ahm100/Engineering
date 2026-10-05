using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Configurations.ProcesVerbals;

public class ProcesVerbalProductConfiguration : IEntityTypeConfiguration<ProcesVerbalProduct>
{
    private const string TableName = "ProcesVerbalEquipments";

    public void Configure(EntityTypeBuilder<ProcesVerbalProduct> builder)
    {
        builder.MetaConfiguration<ProcesVerbalProduct, long>(TableName);

        builder.Property(oo => oo.NewFinalValue)
            .HasComment(ProcesVerbalCmts.NewFinalAmout)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(oo => oo.ProcesVerbalProductItemStatus)
            .HasComment(ProcesVerbalCmts.ProcesVerbalProductItemStatus)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500);

        builder.HasOne(oo => oo.ProcesVerbal)
            .WithMany(oo => oo.ProcesVerbalProducts)
            .HasForeignKey("ProcesVerbalId").HasPrincipalKey(nameof(ProcesVerbal.Id))
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oo => oo.ConsumableVolumeProduct)
            .WithMany(oo => oo.ProcesVerbalProduct)
            .HasForeignKey("ConsumableVolumeProductId").HasPrincipalKey(nameof(ConsumableVolumeProduct.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}