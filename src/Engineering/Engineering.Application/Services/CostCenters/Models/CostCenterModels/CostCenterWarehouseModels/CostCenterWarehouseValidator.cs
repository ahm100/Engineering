
namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;

public class CostCenterWarehouseValidator : AbstractValidator<CostCenterWarehouseRequest>
{
    public CostCenterWarehouseValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.IsDefault).IsRequiredBool(CCenterCmts.IsDefault);
    }
}
