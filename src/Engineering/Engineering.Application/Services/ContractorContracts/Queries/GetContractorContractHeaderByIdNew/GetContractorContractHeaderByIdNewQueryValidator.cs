
namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdNew;

public class GetContractorContractHeaderByIdNewQueryValidator : AbstractValidator<GetContractorContractHeaderByIdNewQuery>
{
    public GetContractorContractHeaderByIdNewQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
