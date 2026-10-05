
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetById;

public class GetCostCenterWarehouseByIdValidator : AbstractValidator<GetCostCenterWarehouseByIdRequest>
{
    public GetCostCenterWarehouseByIdValidator()
    {
        RuleFor(oo => oo.CostCenterWarehouseId)
            .IsPositive(GlobalCmts.WarehouseId);
    }
}
