
namespace Engineering.Application.Services.CostCenterWarehouses.Commands.DeleteWarehouseFromCostCenter;

public class DeleteWarehouseFromCostCenterCommandValidator : AbstractValidator<DeleteWarehouseFromCostCenterCommand>
{
    public DeleteWarehouseFromCostCenterCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterWarehouseErrors.CostCenterWarehouseIdIsEmpty);
    }
}