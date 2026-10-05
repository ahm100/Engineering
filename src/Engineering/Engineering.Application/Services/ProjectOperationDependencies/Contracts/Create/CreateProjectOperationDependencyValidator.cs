namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.Create;

public class CreateProjectOperationDependencyValidator : AbstractValidator<CreateProjectOperationDependencyRequest>
{
    public CreateProjectOperationDependencyValidator()
    {
        RuleFor(oo => oo.SuccessorProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperation);
        RuleFor(oo => oo.PredecessorProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperation);
        RuleFor(oo => oo.DependencyType)
            .IsEnum(GlobalCmts.Type);
    }
}
