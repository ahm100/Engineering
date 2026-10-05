
namespace Engineering.Application.Services.ContractorStatusStatements.Models.DeleteContractorStatusStatement;

public class DeleteContractorStatusStatementValidator : AbstractValidator<DeleteContractorStatusStatementRequest>
{
    public DeleteContractorStatusStatementValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(CSSErrors.ContractorStatusStatementWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

    }
}
