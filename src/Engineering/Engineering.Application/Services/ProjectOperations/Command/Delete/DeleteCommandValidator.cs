namespace Engineering.Application.Services.ProjectOperations.Commands.Delete;

public class DeleteProjectOperationCommandValidator : AbstractValidator<DeleteProjectOperationCommand>
{
    public DeleteProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationErrors.IdIsEmpty);
    }
}