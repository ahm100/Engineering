
namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetById;

public class GetCostCenterWarehouseByIdQueryValidator : AbstractValidator<GetCostCenterWarehouseByIdQuery>
{
    public GetCostCenterWarehouseByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterWarehouseErrors.CostCenterWarehouseIdIsEmpty);
    }
}
