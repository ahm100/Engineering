namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.WouldCreateCycle;

public class WouldCreateCycleQueryValidator : AbstractValidator<WouldCreateCycleQuery>
{
    public WouldCreateCycleQueryValidator()
    {
        RuleFor(oo => oo.StartId)
            .IsPositive(GlobalCmts.ProjectOperation);
        RuleFor(oo => oo.TargetId)
            .IsPositive(GlobalCmts.ProjectOperation);
    }
}