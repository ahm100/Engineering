
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatement;

public class DeleteContractorStatusStatementCommandValidator : AbstractValidator<DeleteContractorStatusStatementCommand>
{
    public DeleteContractorStatusStatementCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
