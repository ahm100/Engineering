namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByIdIncludeless;

public class GetCostCenterByIdIncludelessQueryValidator : AbstractValidator<GetCostCenterByIdIncludelessQuery>
{
    public GetCostCenterByIdIncludelessQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}