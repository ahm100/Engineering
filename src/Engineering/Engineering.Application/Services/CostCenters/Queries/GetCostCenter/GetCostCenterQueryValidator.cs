namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenter;

public class GetCostCenterQueryValidator : AbstractValidator<GetCostCenterQuery>
{
    public GetCostCenterQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}
