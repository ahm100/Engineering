using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementServiceConfiguration : IEntityTypeConfiguration<ContractorStatusStatementService>
{
    private const string _tableName = "ContractorStatusStatementServices";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementService> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ThirdPartiesAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ProjectManagerApprovalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ManagementApprovalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder
            .HasOne(oo => oo.ContractorStatusStatementDetail)
            .WithMany(oo => oo.ContractorStatusStatementServices)
            .HasForeignKey("ContractorStatusStatementDetailId")
            .HasPrincipalKey(nameof(ContractorStatusStatementDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.ContractorContractDetail)
            .WithMany(oo => oo.ContractorStatusStatementServices)
            .HasForeignKey("ContractorContractDetailId")
            .HasPrincipalKey(nameof(ContractorContractDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(oo => oo.DailyProjectOperation)
            .WithMany(oo => oo.ContractorStatusStatementServices)
            .HasForeignKey("DailyProjectOperationId")
            .HasPrincipalKey(nameof(DailyProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
