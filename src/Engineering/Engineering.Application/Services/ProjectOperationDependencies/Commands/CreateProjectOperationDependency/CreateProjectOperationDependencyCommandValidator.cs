namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.CreateProjectOperationDependency;

public class CreateProjectOperationDependencyCommandValidator : AbstractValidator<CreateProjectOperationDependencyCommand>
{
    public CreateProjectOperationDependencyCommandValidator()
    {
        RuleFor(oo => oo.PredecessorProjectOperationId).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.SuccessorProjectOperationId).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.DependencyType).IsEnum(GlobalCmts.Type);
    }
}