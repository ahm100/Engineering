namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.UpdateProjectOperationDependency;

public class UpdateProjectOperationDependencyValidator : AbstractValidator<UpdateProjectOperationDependencyRequest>
{
    public UpdateProjectOperationDependencyValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}