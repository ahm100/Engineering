namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPODependencies;

public class GetPODependenciesQueryValidator : AbstractValidator<GetPODependenciesQuery>
{
    public GetPODependenciesQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
    }
}