
namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.DeleteProjectOperationDependency;

public class DeleteProjectOperationDependencyValidator : AbstractValidator<DeleteProjectOperationDependencyRequest>
{
    public DeleteProjectOperationDependencyValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDependencyErrors.IdIsEmpty);
    }
}
