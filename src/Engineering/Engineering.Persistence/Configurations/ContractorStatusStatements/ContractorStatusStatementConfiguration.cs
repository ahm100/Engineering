using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementConfiguration : IEntityTypeConfiguration<ContractorStatusStatement>
{
    private const string _tableName = "ContractorStatusStatements";
    public void Configure(EntityTypeBuilder<ContractorStatusStatement> builder)
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

        builder.Property(oo => oo.Type)
            .HasDefaultValue(CSSType.System)
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

        builder.Property(oo => oo.ForContractorProductsAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ServicedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.FixedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.RemainingAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.FinesAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.RewardsAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.CostOversAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ThirdPartiesAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ProjectManagerApprovalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PayableAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.CanPayableAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.DiscountPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ManagementApprovalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.CreatorConfirmedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ProjectManagerConfirmedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ManagementConfirmedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PaymentedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagmentDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ProjectManagmentDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ConfirmedPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ConfirmedBankAccountId);

        builder.Property(oo => oo.PaymentOrderId);

        builder.Property(oo => oo.ConfirmedPaymentDate);

        builder.Property(oo => oo.PrimaryManagerConfirmed);

        builder.Property(oo => oo.FinalManagerConfirmed);

        builder.Property(oo => oo.ConfirmedDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.PrimaryManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.FinalManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.CurrencyId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.Season)
            .WithMany(oo => oo.ContractorStatusStatements)
            .HasForeignKey("SeasonId")
            .HasPrincipalKey(nameof(Season.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.Project)
            .WithMany(oo => oo.ContractorStatusStatements)
            .HasForeignKey("ProjectId")
            .HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
