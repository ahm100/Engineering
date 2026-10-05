
namespace Engineering.Application.Services.CostCenterWarehouses.Models.Delete;

public class DeleteCostCenterWarehouseValidator : AbstractValidator<DeleteCostCenterWarehouseRequest>
{
    public DeleteCostCenterWarehouseValidator()
    {
        RuleFor(oo => oo.CostCenterWarehouseId)
            .IsPositive(GlobalCmts.WarehouseId);
    }
}
