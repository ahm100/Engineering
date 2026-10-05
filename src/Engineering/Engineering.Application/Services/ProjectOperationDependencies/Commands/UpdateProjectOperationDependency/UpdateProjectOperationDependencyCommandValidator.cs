namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.UpdateProjectOperationDependency;

public class UpdateProjectOperationDependencyCommandValidator : AbstractValidator<UpdateProjectOperationDependencyCommand>
{
    public UpdateProjectOperationDependencyCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}