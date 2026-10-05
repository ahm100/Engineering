using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractDetailCostOverConfiguration : IEntityTypeConfiguration<ContractorContractDetailCostOver>
{
    private const string TableName = "ContractorContractDetailCostOvers";
    public void Configure(EntityTypeBuilder<ContractorContractDetailCostOver> builder)
    {
        builder.MetaConfiguration<ContractorContractDetailCostOver, long>(TableName);

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.Percentage)
            .HasColumnType("decimal(5, 2)")
            .IsRequired();

        builder.Property(oo => oo.Amount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.ContractorContractDetail)
            .WithMany(oo => oo.ContractorContractDetailCostOvers)
            .HasForeignKey("ContractorContractDetailId")
            .HasPrincipalKey(nameof(ContractorContractDetail.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ContractorContract)
            .WithMany(oo => oo.ContractorContractDetailCostOvers)
            .HasForeignKey("ContractorContractId")
            .HasPrincipalKey(nameof(ContractorContract.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.CostOver)
            .WithMany(oo => oo.ContractorContractDetailCostOvers)
            .HasForeignKey("CostOverId")
            .HasPrincipalKey(nameof(CostOver.Id))
            .OnDelete(DeleteBehavior.Restrict);

    }
}
