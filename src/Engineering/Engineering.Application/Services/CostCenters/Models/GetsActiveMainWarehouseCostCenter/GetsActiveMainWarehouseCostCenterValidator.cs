namespace Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;

public class GetsActiveMainWarehouseCostCenterValidator : AbstractValidator<GetsActiveMainWarehouseCostCenterRequest>
{
    public GetsActiveMainWarehouseCostCenterValidator()
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
