
namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderById;

public class GetContractorContractHeaderByIdQueryValidator : AbstractValidator<GetContractorContractHeaderByIdQuery>
{
    public GetContractorContractHeaderByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
