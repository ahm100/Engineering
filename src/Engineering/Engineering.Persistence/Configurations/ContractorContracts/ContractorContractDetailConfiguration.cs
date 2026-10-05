using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractDetailConfiguration : IEntityTypeConfiguration<ContractorContractDetail>
{
    private const string TableName = "ContractorContractDetails";
    public void Configure(EntityTypeBuilder<ContractorContractDetail> builder)
    {
        builder.MetaActiveConfiguration<ContractorContractDetail, long>(TableName);

        builder.Property(oo => oo.StartDate);

        builder.Property(oo => oo.EndDate);

        builder.Property(oo => oo.WorkLoad)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.UnitAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalCostOveredAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ContractCoefficient)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(oo => oo.ContractorContract)
            .WithMany(oo => oo.Details)
            .HasForeignKey("ContractorContractId")
            .HasPrincipalKey(nameof(ContractorContract.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.ContractorContractDetails)
            .HasForeignKey("ProjectOperationId")
            .HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
