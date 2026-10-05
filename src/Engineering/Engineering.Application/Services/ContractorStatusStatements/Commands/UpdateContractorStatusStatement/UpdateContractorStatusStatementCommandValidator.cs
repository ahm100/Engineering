
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatement;

public class UpdateContractorStatusStatementCommandValidator : AbstractValidator<UpdateContractorStatusStatementCommand>
{
    public UpdateContractorStatusStatementCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
