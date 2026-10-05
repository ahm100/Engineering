
namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdIncludeless;

public class GetContractorContractHeaderByIdIncludelessQueryValidator : AbstractValidator<GetContractorContractHeaderByIdIncludelessQuery>
{
    public GetContractorContractHeaderByIdIncludelessQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
