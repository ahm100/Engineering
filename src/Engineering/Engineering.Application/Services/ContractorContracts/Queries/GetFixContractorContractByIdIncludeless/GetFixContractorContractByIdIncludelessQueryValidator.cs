
namespace Engineering.Application.Services.ContractorContracts.Queries.GetFixContractorContractByIdIncludeless;

public class GetFixContractorContractByIdIncludelessQueryValidator : AbstractValidator<GetFixContractorContractByIdIncludelessQuery>
{
    public GetFixContractorContractByIdIncludelessQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
