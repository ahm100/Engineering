using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Persistence.Configurations.Adjustment;
public class AdjustmentIndexValueConfiguration
    : IEntityTypeConfiguration<AdjustmentIndexValue>
{
    private const string TableName = "AdjustmentIndexValues";

    public void Configure(
        EntityTypeBuilder<AdjustmentIndexValue> builder)
    {
        builder.MetaConfiguration<AdjustmentIndexValue, long>(
            TableName);

        builder.Property(oo => oo.AdjustmentIndexId)
            .IsRequired();

        builder.Property(oo => oo.YearName)
            .IsRequired()
             .HasMaxLength(10);

        builder.Property(oo => oo.Period)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(oo => oo.Value)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(oo => oo.Coefficient)
            .HasPrecision(18, 6);

        builder.Property(oo => oo.NotificationNumber)
            .HasMaxLength(100);
            
        builder.Property(oo => oo.IsActive)
            .IsRequired()
            .HasComment(GlobalCmts.IsActive);

        builder.HasOne(oo => oo.AdjustmentIndex)
            .WithMany(oo => oo.Values)
            .HasForeignKey(oo => oo.AdjustmentIndexId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}