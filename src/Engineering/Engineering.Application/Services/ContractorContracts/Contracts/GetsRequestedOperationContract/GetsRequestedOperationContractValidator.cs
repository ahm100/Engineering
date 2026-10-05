
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;

public class GetsRequestedOperationContractValidator : AbstractValidator<GetsRequestedOperationContractRequest>
{
    public GetsRequestedOperationContractValidator()
    {
        RuleForEach(oo => oo.ProjectOperationDetailServiceIds)
            .IsPositive(CCCmts.ProjectOperationDetailContractorServiceId);
    }
}
