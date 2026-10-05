using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationHistoryConfiguration : IEntityTypeConfiguration<DailyProjectOperationHistory>
{
    private const string TableName = "DailyProjectOperationHistories";
    public void Configure(EntityTypeBuilder<DailyProjectOperationHistory> builder)
    {
        builder.MetaConfiguration<DailyProjectOperationHistory, long>(TableName);

        builder.Property(oo => oo.LegacyId);

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Length)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Width)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Height)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Weight)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasDefaultValue(DailyProjectOperationType.Daily)
            .IsRequired();

        builder.Ignore(c => c.FinalAmount);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(c => c.DailyProjectOperation)
               .WithMany(c => c.DailyProjectOperationHistories)
               .HasForeignKey("DailyProjectOperationId")
               .HasPrincipalKey(nameof(DailyProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}