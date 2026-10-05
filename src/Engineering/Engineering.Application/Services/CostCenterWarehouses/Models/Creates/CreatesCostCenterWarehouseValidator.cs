
namespace Engineering.Application.Services.CostCenterWarehouses.Models.Creates;

public class CreatesCostCenterWarehouseValidator : AbstractValidator<CreatesCostCenterWarehouseRequest>
{
    public CreatesCostCenterWarehouseValidator()
    {
        RuleForEach(oo => oo.CostCenterWarehouses).NotEmpty().WithError(CostCenterWarehouseErrors.WarehouseIdsIsEmpty);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleForEach(oo => oo.CostCenterWarehouses).NotEmpty().SetValidator(new CostCenterWarehousesModelValidator());
    }
}
