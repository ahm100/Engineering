
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetDefaultCostCenterId;

public class GetDefaultCostCenterWarehouseByCostCenterIdValidator : AbstractValidator<GetDefaultCostCenterWarehouseByCostCenterIdRequest>
{
    public GetDefaultCostCenterWarehouseByCostCenterIdValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
