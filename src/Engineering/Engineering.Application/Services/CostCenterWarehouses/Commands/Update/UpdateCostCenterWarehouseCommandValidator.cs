
namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Update;

public class UpdateCostCenterWarehouseCommandValidator : AbstractValidator<UpdateCostCenterWarehouseCommand>
{
    public UpdateCostCenterWarehouseCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterWarehouseErrors.CostCenterWarehouseIdIsEmpty);
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(CostCenterWarehouseErrors.WarehouseIdIsEmpty);
        RuleFor(oo => oo.IsDefault).NotNull().WithError(CostCenterWarehouseErrors.IsDefaultIsEmpty);
    }
}
