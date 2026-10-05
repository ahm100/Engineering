namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationStatus;

public class UpdateProjectOperationStatusCommandValidator : AbstractValidator<UpdateProjectOperationStatusCommand>
{
    public UpdateProjectOperationStatusCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}