namespace Engineering.Application.Services.ProjectOperations.Commands.Create;

public class CreateProjectOperationCommandValidator : AbstractValidator<CreateProjectOperationCommand>
{
    public CreateProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.OperationInfo).NotEmpty().WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.Project).NotEmpty().WithError(ProjectOperationErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.Workload).NotNull().WithError(ProjectOperationErrors.WorkloadIsEmpty);
        RuleFor(oo => oo.TolerancePercentage).NotNull().WithError(ProjectOperationErrors.TolerancePercentageIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().WithError(ProjectOperationErrors.MeasurementIdIsEmpty);
        RuleFor(oo => oo.ProjectOperationStatus).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}