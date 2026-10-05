namespace Engineering.Application.Services.CostCenterWarehouses.Models.Creates;

public class CostCenterWarehousesModelValidator : AbstractValidator<CostCenterWarehousesModel>
{
    public CostCenterWarehousesModelValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(CostCenterWarehouseErrors.WarehouseIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.IsDefault).NotNull().WithError(CostCenterWarehouseErrors.IsDefaultIsEmpty);
    }
}
