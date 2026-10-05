
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementService;

public class CreateContractorStatusStatementServiceCommandValidator : AbstractValidator<CreateContractorStatusStatementServiceCommand>
{
    public CreateContractorStatusStatementServiceCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
