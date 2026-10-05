namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;

public class GetCostCenterTypeByCodeValidator : AbstractValidator<GetCostCenterTypeByCodeRequest>
{
    public GetCostCenterTypeByCodeValidator()
    {
        RuleFor(v => v.CostCenterTypeCode)
            .IsFullString(CCenterCmts.CostCenterCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}