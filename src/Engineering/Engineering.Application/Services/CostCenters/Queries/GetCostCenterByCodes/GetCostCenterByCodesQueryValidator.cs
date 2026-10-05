namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCodes;

public class GetCostCenterByCodesQueryValidator : AbstractValidator<GetCostCenterByCodesQuery>
{
    public GetCostCenterByCodesQueryValidator()
    {
        RuleFor(oo => oo.CostCenterCodes).NotEmpty().WithError(CostCenterErrors.CostCenterCodesIsEmpty);
    }
}