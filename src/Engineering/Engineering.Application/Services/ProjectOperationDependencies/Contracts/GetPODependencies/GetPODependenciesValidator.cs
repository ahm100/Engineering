namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;

public class GetPODependenciesValidator : AbstractValidator<GetPODependenciesRequest>
{
    public GetPODependenciesValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
    }
}