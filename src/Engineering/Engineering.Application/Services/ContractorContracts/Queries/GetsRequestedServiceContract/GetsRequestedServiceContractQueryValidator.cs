
namespace Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedServiceContract;

public class GetsRequestedServiceContractQueryValidator : AbstractValidator<GetsRequestedServiceContractQuery>
{
    public GetsRequestedServiceContractQueryValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(ContractorContractErrors.ProjectIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ContractorContractErrors.ProjectMustBiggerThanZero);

        RuleFor(oo => oo.ContractorId)
            .NotNull().WithError(ContractorContractErrors.ContractorIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ContractorContractErrors.ContractorMustBiggerThanZero);

        RuleFor(oo => oo.ServiceIds)
            .NotEmpty().WithError(ContractorContractErrors.InValidServiceIds);

        RuleForEach(c => c.ServiceIds)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
