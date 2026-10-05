namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.RejectProjectCostCenterRequest;

public class RejectProjectCostCenterRequestValidator : AbstractValidator<RejectProjectCostCenterRequestRequest>
{
    public RejectProjectCostCenterRequestValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Reason).HasMaxLength(GlobalCmts.RejectionReason, 1000);
    }
}