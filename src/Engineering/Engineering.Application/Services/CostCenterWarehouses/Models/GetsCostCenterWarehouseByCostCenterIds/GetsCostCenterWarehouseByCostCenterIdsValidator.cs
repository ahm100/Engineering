
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseByCostCenterIds;

public class GetsCostCenterWarehouseByCostCenterIdsValidator : AbstractValidator<GetsCostCenterWarehouseByCostCenterIdsRequest>
{
    public GetsCostCenterWarehouseByCostCenterIdsValidator()
    {
        RuleForEach(oo => oo.CostCenterIds)
            .IsPositive(GlobalCmts.CostCenterId);
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
