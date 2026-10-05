namespace Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperation;

public class DeleteProjectOperationValidator : AbstractValidator<DeleteProjectOperationRequest>
{
    public DeleteProjectOperationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
