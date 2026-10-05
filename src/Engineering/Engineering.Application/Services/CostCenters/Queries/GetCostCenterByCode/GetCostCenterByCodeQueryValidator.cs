namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCode;

public class GetCostCenterByCodeQueryValidator : AbstractValidator<GetCostCenterByCodeQuery>
{
    public GetCostCenterByCodeQueryValidator()
    {
        RuleFor(oo => oo.CostCenterCode).NotEmpty().WithError(CostCenterErrors.CostCenterCodeIsEmpty);
    }
}