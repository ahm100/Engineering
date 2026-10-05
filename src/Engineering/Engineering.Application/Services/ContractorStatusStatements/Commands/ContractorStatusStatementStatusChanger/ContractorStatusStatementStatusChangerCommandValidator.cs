
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementStatusChanger;

public class ContractorStatusStatementStatusChangerCommandValidator : AbstractValidator<ContractorStatusStatementStatusChangerCommand>
{
    public ContractorStatusStatementStatusChangerCommandValidator()
    {
        RuleFor(c => c.Entity)
            .NotNull().WithError(CSSErrors.ContractorStatusStatementWithIdNotFound);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
