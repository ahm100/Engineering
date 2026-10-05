
namespace Engineering.Application.Services.CostCenterWarehouses.Models.Update;

public class UpdateCostCenterWarehouseValidator : AbstractValidator<UpdateCostCenterWarehouseRequest>
{
    public UpdateCostCenterWarehouseValidator()
    {
        RuleFor(oo => oo.CostCenterWarehouseId)
            .IsPositive(GlobalCmts.CostCenter);
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.WarehouseId);
        RuleFor(oo => oo.IsDefault)
            .IsRequiredBool(CCenterCmts.IsDefault);
    }
}
