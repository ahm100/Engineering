namespace Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;

public class GetsActiveAuthorizedCostCenterValidator : AbstractValidator<GetsActiveAuthorizedCostCenterRequest>
{
    public GetsActiveAuthorizedCostCenterValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
