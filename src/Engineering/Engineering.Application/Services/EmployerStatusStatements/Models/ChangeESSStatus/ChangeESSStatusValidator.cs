namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public class ChangeESSStatusValidator : AbstractValidator<ChangeESSStatusRequest>
{
    public ChangeESSStatusValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(EmployerStatusStatementErrors.UnValidId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
