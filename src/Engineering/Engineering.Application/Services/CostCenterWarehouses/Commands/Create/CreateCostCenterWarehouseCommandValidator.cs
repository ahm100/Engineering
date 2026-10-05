
namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Create;

public class CreateCostCenterWarehouseCommandValidator : AbstractValidator<CreateCostCenterWarehouseCommand>
{
    public CreateCostCenterWarehouseCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterWarehouseErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(CostCenterWarehouseErrors.WarehouseIdIsEmpty);
        RuleFor(oo => oo.IsDefault).NotNull().WithError(CostCenterWarehouseErrors.IsDefaultIsEmpty);
    }
}