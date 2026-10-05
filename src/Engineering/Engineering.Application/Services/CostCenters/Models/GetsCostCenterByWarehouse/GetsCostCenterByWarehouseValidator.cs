namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByWarehouse;

public class GetsCostCenterByWarehouseValidator : AbstractValidator<GetsCostCenterByWarehouseRequest>
{
    public GetsCostCenterByWarehouseValidator()
    {
        RuleFor(oo => oo.WarehouseId)
            .IsPositive(GlobalCmts.WarehouseId);
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
