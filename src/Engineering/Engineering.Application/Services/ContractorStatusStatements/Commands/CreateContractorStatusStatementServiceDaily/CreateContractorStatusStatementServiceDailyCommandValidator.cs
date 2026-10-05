
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementServiceDaily;

public class CreateContractorStatusStatementServiceDailyCommandValidator : AbstractValidator<CreateContractorStatusStatementServiceDailyCommand>
{
    public CreateContractorStatusStatementServiceDailyCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
