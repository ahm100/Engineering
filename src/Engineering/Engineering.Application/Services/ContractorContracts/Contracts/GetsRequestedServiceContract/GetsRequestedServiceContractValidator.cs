
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;

public class GetsRequestedServiceContractValidator : AbstractValidator<GetsRequestedServiceContractRequest>
{
    public GetsRequestedServiceContractValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull()
            .IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.ContractorId)
            .IsPositive(GlobalCmts.ContractorId);
        RuleForEach(oo => oo.ServiceIds)
            .IsPositive(GlobalCmts.ServiceId);
    }
}
