
using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Persistence.Configurations.Adjustment;

public class AdjustmentIndexConfiguration
    : IEntityTypeConfiguration<AdjustmentIndex>
{
    private const string TableName = "AdjustmentIndexes";

    public void Configure(
        EntityTypeBuilder<AdjustmentIndex> builder)
    {
        builder.MetaConfiguration<AdjustmentIndex, long>(TableName);

        builder.Property(oo => oo.AdjustmentReferenceId)
            .IsRequired();

        builder.Property(oo => oo.BranchId)
            .IsRequired();

        builder.Property(oo => oo.SeasonId)
            .IsRequired(false);

        builder.Property(oo => oo.Code)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired()
            .HasComment(GlobalCmts.Code);

        builder.Property(oo => oo.Title)
            .HasMaxLength(250)
            .IsRequired()
            .HasComment(GlobalCmts.FaTitle);
            

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500)
            .HasComment(GlobalCmts.Description);


        builder.Property(oo => oo.DocumentFile)
             .HasMaxLength(250);

        builder.HasOne(oo => oo.Season)
            .WithMany()
            .HasForeignKey(oo => oo.SeasonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Branch)
            .WithMany()
            .HasForeignKey(oo => oo.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(oo => oo.Values)
            .WithOne(oo => oo.AdjustmentIndex)
            .HasForeignKey(oo => oo.AdjustmentIndexId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}