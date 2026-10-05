namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.UpdateProjectOperationTemporaryDaily;

public class UpdateProjectOperationTemporaryDailyCommandValidator : AbstractValidator<UpdateProjectOperationTemporaryDailyCommand>
{
    public UpdateProjectOperationTemporaryDailyCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationTemporaryDailyErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.StartDate).NotNull().WithError(ProjectOperationTemporaryDailyErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotNull().WithError(ProjectOperationTemporaryDailyErrors.EndDateIsEmpty);
        RuleFor(oo => oo.Project).NotEmpty().WithError(ProjectOperationTemporaryDailyErrors.ProjectIsEmpty);
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(ProjectOperationTemporaryDailyErrors.CostCenterIsEmpty);
    }
}
