
namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;

public class GetContractorStatusStatementByIdValidator : AbstractValidator<GetContractorStatusStatementByIdRequest>
{
    public GetContractorStatusStatementByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(CSSErrors.ContractorStatusStatementWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

    }
}
