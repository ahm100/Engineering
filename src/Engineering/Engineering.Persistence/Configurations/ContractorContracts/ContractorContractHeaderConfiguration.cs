using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractHeaderConfiguration : IEntityTypeConfiguration<ContractorContractHeader>
{
    private const string TableName = "ContractorContractHeaders";
    public void Configure(EntityTypeBuilder<ContractorContractHeader> builder)
    {
        builder.MetaConfiguration<ContractorContractHeader, long>(TableName);

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasDefaultValue(ContractorContractStatus.New)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.CostCenterId)
            .IsRequired(false);
        builder.HasOne(oo => oo.CostCenter)
               .WithMany(oo => oo.ContractorContractHeaders)
               .HasForeignKey(oo => oo.CostCenterId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
    }
}
