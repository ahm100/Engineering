namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterByName;

public class GetCostCenterByNameValidator : AbstractValidator<GetCostCenterByNameRequest>
{
    public GetCostCenterByNameValidator()
    {
        RuleFor(oo => oo.CostCenterName)
            .IsFullString(CCenterCmts.CostCenterName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
