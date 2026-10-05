using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementDetailConfiguration : IEntityTypeConfiguration<ContractorStatusStatementDetail>
{
    private const string _tableName = "ContractorStatusStatementDetails";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementDetail> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PercentageDoingJobWell)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.FixedContractPct)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.FixedContractPctAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.FixedContractPctDesc)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ProjectFixedContractPct)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.ProjectFixedContractPctAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ProjectFixedContractPctDesc)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagerFixedContractPct)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.ManagerFixedContractPctAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ManagerFixedContractPctDesc)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.DoingJobWellAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PercentageAdvancePayment)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.AdvancePaymentAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.DailyLatenessPenalty)
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
            .WithMany(oo => oo.ContractorStatusStatementDetails)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.ContractorContract)
            .WithMany(oo => oo.ContractorStatusStatementDetails)
            .HasForeignKey("ContractorContractId")
            .HasPrincipalKey(nameof(ContractorContract.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
