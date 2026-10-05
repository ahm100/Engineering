using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractDetailServiceConfiguration : IEntityTypeConfiguration<ContractorContractDetailService>
{
    private const string TableName = "ContractorContractDetailServices";
    public void Configure(EntityTypeBuilder<ContractorContractDetailService> builder)
    {
        builder.MetaConfiguration<ContractorContractDetailService, long>(TableName);

        builder.HasOne(oo => oo.ContractorContractDetail)
            .WithMany(oo => oo.ContractorContractDetailServices)
            .HasForeignKey("ContractorContractDetailId")
            .HasPrincipalKey(nameof(ContractorContractDetail.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ProjectOperationDetailContractorService)
            .WithMany(oo => oo.ContractorContractDetailServices)
            .HasForeignKey("ProjectOperationDetailContractorServiceId")
            .HasPrincipalKey(nameof(ProjectOperationDetailContractorService.Id))
            .OnDelete(DeleteBehavior.Restrict);

    }
}
