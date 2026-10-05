namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterByCode;

public class GetCostCenterByCodeValidator : AbstractValidator<GetCostCenterByCodeRequest>
{
    public GetCostCenterByCodeValidator()
    {
        RuleFor(oo => oo.CostCenterCode)
            .IsFullString(CCenterCmts.CostCenterCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
