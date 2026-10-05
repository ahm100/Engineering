
namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractByIdIncludeless;

public class GetContractorContractByIdIncludelessQueryValidator : AbstractValidator<GetContractorContractByIdIncludelessQuery>
{
    public GetContractorContractByIdIncludelessQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
