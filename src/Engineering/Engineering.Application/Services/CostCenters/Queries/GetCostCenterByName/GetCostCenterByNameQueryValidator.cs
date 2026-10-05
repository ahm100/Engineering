namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByName;

public class GetCostCenterByNameQueryValidator : AbstractValidator<GetCostCenterByNameQuery>
{
    public GetCostCenterByNameQueryValidator()
    {
        RuleFor(oo => oo.CostCenterName).NotEmpty().WithError(CostCenterErrors.CostCenterNameIsEmpty);
    }
}