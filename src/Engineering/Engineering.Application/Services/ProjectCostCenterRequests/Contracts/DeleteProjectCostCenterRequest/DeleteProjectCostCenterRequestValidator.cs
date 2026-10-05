namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.DeleteProjectCostCenterRequest;

public class DeleteProjectCostCenterRequestValidator : AbstractValidator<DeleteProjectCostCenterRequestRequest>
{
    public DeleteProjectCostCenterRequestValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}