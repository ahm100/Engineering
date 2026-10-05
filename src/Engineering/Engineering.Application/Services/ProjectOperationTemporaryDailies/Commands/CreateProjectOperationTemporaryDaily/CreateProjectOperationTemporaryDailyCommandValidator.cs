namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.CreateProjectOperationTemporaryDaily;

public class CreateProjectOperationTemporaryDailyCommandValidator : AbstractValidator<CreateProjectOperationTemporaryDailyCommand>
{
    public CreateProjectOperationTemporaryDailyCommandValidator()
    {
        RuleFor(oo => oo.StartDate).NotNull().WithError(ProjectOperationTemporaryDailyErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotNull().WithError(ProjectOperationTemporaryDailyErrors.EndDateIsEmpty);
        RuleFor(oo => oo.Project).NotEmpty().WithError(ProjectOperationTemporaryDailyErrors.ProjectIsEmpty);
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(ProjectOperationTemporaryDailyErrors.CostCenterIsEmpty);
    }
}
