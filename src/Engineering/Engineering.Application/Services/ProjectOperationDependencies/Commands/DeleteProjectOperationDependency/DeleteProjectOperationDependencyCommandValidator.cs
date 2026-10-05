namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.DeleteProjectOperationDependency;

public class DeleteProjectOperationDependencyCommandValidator : AbstractValidator<DeleteProjectOperationDependencyCommand>
{
    public DeleteProjectOperationDependencyCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}