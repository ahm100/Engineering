
namespace Engineering.Application.Services.CostCenterWarehouses.Models.CostCenterWarehouseGroupDelete;

public class CostCenterWarehouseGroupDeleteValidator : AbstractValidator<CostCenterWarehouseGroupDeleteRequest>
{
    public CostCenterWarehouseGroupDeleteValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.WarehouseId);
    }
}
