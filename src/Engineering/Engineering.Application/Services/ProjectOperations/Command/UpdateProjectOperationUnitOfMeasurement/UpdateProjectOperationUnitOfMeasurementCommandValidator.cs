namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationUnitOfMeasurement;

public class UpdateProjectOperationUnitOfMeasurementCommandValidator : AbstractValidator<UpdateProjectOperationUnitOfMeasurementCommand>
{
    public UpdateProjectOperationUnitOfMeasurementCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperations).NotNull().WithError(ProjectOperationErrors.IdsAreEmpty);
    }
}
