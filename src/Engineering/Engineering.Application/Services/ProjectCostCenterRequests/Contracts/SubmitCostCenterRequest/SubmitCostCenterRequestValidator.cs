namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.SubmitCostCenterRequest;

public class SubmitCostCenterRequestValidator : AbstractValidator<SubmitCostCenterRequestRequest>
{
    public SubmitCostCenterRequestValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.Name).MaximumLength(250);
        RuleFor(oo => oo.Description).HasMaxLength(GlobalCmts.Description, 1500);
    }
}