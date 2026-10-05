namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.ChangeProjectOperationTemporaryDailyStatus;

public class ChangeProjectOperationTemporaryDailyStatusCommandValidator : AbstractValidator<ChangeProjectOperationTemporaryDailyStatusCommand>
{
    public ChangeProjectOperationTemporaryDailyStatusCommandValidator()
    {
        RuleFor(oo => oo.Entity).NotNull().WithError(ProjectOperationTemporaryDailyErrors.TemporaryDailyIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(ProjectOperationTemporaryDailyErrors.TemporaryDailyStatusIsEmpty)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
