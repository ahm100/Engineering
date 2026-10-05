
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatement;

public class CreateContractorStatusStatementCommandValidator : AbstractValidator<CreateContractorStatusStatementCommand>
{
    public CreateContractorStatusStatementCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
