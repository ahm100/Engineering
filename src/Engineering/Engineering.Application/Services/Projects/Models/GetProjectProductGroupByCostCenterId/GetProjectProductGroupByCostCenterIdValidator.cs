namespace Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;

public class GetProjectProductGroupByCostCenterIdValidator : AbstractValidator<GetProjectProductGroupByCostCenterIdRequest>
{
    public GetProjectProductGroupByCostCenterIdValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.CostCenterId);
    }
}