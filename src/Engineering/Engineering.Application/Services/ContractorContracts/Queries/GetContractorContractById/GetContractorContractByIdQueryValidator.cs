
namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractById;

public class GetContractorContractByIdQueryValidator : AbstractValidator<GetContractorContractByIdQuery>
{
    public GetContractorContractByIdQueryValidator()
    {
        RuleFor(c => c.ContractorContractId)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
