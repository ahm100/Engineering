namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;

public class GetPORequirementsValidator : AbstractValidator<GetPORequirementsRequest>
{
    public GetPORequirementsValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
    }
}