namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.CreateProjectOperationTemporaryDaily;

public class CreateProjectOperationTemporaryDailyValidator : AbstractValidator<CreateProjectOperationTemporaryDailyRequest>
{
    public CreateProjectOperationTemporaryDailyValidator()
    {
        RuleFor(oo => oo.StartDate).NotNull().WithError(ProjectOperationTemporaryDailyErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotNull().WithError(ProjectOperationTemporaryDailyErrors.EndDateIsEmpty);
        RuleFor(oo => oo.projectId).NotNull().WithError(ProjectOperationTemporaryDailyErrors.ProjectIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(ProjectOperationTemporaryDailyErrors.CostCenterIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
