
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementFine;

public class CreateContractorStatusStatementFineCommandValidator : AbstractValidator<CreateContractorStatusStatementFineCommand>
{
    public CreateContractorStatusStatementFineCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
