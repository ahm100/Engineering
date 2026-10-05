namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterById;

public class GetCostCenterByIdQueryValidator : AbstractValidator<GetCostCenterByIdQuery>
{
    public GetCostCenterByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}