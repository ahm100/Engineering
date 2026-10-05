
namespace Engineering.Application.Services.CostCenterWarehouses.Commands.UpdateWarehouseFromCostCenter;

public class UpdateWarehouseFromCostCenterCommandValidator : AbstractValidator<UpdateWarehouseFromCostCenterCommand>
{
    public UpdateWarehouseFromCostCenterCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterWarehouseErrors.CostCenterWarehouseIdIsEmpty);
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(CostCenterWarehouseErrors.WarehouseIdIsEmpty);
        RuleFor(oo => oo.IsDefault).NotNull().WithError(CostCenterWarehouseErrors.IsDefaultIsEmpty);
    }
}
