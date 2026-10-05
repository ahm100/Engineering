namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPORequirements;

public class GetPORequirementsQueryValidator : AbstractValidator<GetPORequirementsQuery>
{
    public GetPORequirementsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
    }
}