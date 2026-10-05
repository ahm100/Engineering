
namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorByContractorContractType;

public class GetsContractorByContractorContractTypeQueryValidator : AbstractValidator<GetsContractorByContractorContractTypeQuery>
{
    public GetsContractorByContractorContractTypeQueryValidator()
    {
        RuleFor(oo => oo.ContractorContractTypeId)
            .NotNull().WithError(ContractorContractErrors.TypeIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
