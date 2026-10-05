namespace Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;

public class GetProjectProductCategoryByCostCenterIdValidator : AbstractValidator<GetProjectProductCategoryByCostCenterIdRequest>
{
    public GetProjectProductCategoryByCostCenterIdValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.CostCenterId);
    }
}