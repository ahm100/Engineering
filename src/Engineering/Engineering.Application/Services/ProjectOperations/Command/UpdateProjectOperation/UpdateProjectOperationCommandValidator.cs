namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperation;

public class UpdateProjectOperationCommandValidator : AbstractValidator<UpdateProjectOperationCommand>
{
    public UpdateProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
        RuleFor(oo => oo.OperationInfo).NotNull().WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.Project).NotNull().WithError(ProjectOperationErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.Workload).NotNull().WithError(ProjectOperationErrors.WorkloadIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().WithError(ProjectOperationErrors.MeasurementIdIsEmpty);
    }
}