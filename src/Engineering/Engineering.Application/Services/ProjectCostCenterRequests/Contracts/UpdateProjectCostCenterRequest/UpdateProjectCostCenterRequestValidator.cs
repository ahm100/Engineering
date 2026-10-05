namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.UpdateProjectCostCenterRequest;

public class UpdateProjectCostCenterRequestValidator : AbstractValidator<UpdateProjectCostCenterRequestRequest>
{
    public UpdateProjectCostCenterRequestValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Name).MaximumLength(250);
        RuleFor(oo => oo.Description).HasMaxLength(GlobalCmts.Description, 1500);
    }
}