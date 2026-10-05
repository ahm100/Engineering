namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequestById;

public class GetProjectCostCenterRequestByIdValidator : AbstractValidator<GetProjectCostCenterRequestByIdRequest>
{
    public GetProjectCostCenterRequestByIdValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}