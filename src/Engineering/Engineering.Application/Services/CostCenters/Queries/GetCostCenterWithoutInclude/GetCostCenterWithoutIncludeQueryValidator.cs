namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;

public class GetCostCenterWithoutIncludeQueryValidator : AbstractValidator<GetCostCenterWithoutIncludeQuery>
{
    public GetCostCenterWithoutIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}
