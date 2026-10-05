
namespace Engineering.Application.Services.ContractorContracts.Queries.GetHeaderByIdForContractorStatusStatement;

public class GetHeaderByIdForContractorStatusStatementQueryValidator : AbstractValidator<GetHeaderByIdForContractorStatusStatementQuery>
{
    public GetHeaderByIdForContractorStatusStatementQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
