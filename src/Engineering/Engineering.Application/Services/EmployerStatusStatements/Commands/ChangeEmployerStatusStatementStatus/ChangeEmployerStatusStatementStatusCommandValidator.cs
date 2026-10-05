
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.ChangeEmployerStatusStatementStatus;

public class ChangeEmployerStatusStatementStatusCommandValidator : AbstractValidator<ChangeEmployerStatusStatementStatusCommand>
{
    public ChangeEmployerStatusStatementStatusCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(EContractErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
