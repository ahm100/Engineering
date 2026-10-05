
namespace Engineering.Application.Services.CostCenterWarehouses.Models.Create;

public class CreateCostCenterWarehouseValidator : AbstractValidator<CreateCostCenterWarehouseRequest>
{
    public CreateCostCenterWarehouseValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.WarehouseId);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.IsDefault)
            .IsRequiredBool(CCenterCmts.IsDefault);
    }
}
