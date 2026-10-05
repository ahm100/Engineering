
namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Delete;

public class DeleteCostCenterWarehouseCommandValidator : AbstractValidator<DeleteCostCenterWarehouseCommand>
{
    public DeleteCostCenterWarehouseCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterWarehouseErrors.CostCenterWarehouseIdIsEmpty);
    }
}