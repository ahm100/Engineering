namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithWarehousesInclude;

public class GetCostCenterWithWarehousesIncludeQueryValidator : AbstractValidator<GetCostCenterWithWarehousesIncludeQuery>
{
    public GetCostCenterWithWarehousesIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}
