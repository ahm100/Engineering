
namespace Engineering.Application.Services.ContractorContracts.Queries.GetServiceContractorContractByIdIncludeless;

public class GetServiceContractorContractByIdIncludelessQueryValidator : AbstractValidator<GetServiceContractorContractByIdIncludelessQuery>
{
    public GetServiceContractorContractByIdIncludelessQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
