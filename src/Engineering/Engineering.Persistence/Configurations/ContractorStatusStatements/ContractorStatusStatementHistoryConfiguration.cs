using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementHistoryConfiguration : IEntityTypeConfiguration<ContractorStatusStatementHistory>
{
    private const string _tableName = "ContractorStatusStatementHistories";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementHistory> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Code)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasDefaultValue(CSSStatus.New)
            .IsRequired();

        builder.Property(oo => oo.FinalTotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalPercentageDoingJobWell)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalPercentageAdvancePayment)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalAdvancePaymentAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalDailyLatenessPenalty)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalWorkDonePercent)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalWorkDeliveryPercent)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TotalWorkCompletionPercent)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ProductsAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.FinesAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.RewardsAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ThirdPartiesAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PaymentedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder
            .HasOne(oo => oo.ContractorStatusStatement)
            .WithMany(oo => oo.ContractorStatusStatementHistories)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
