
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementCostOver;

public class CreateContractorStatusStatementCostOverCommandValidator : AbstractValidator<CreateContractorStatusStatementCostOverCommand>
{
    public CreateContractorStatusStatementCostOverCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
