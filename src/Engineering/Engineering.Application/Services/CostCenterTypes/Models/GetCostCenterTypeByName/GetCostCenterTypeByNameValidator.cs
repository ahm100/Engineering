namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;

public class GetCostCenterTypeByNameValidator : AbstractValidator<GetCostCenterTypeByNameRequest>
{
    public GetCostCenterTypeByNameValidator()
    {
        RuleFor(v => v.CostCenterTypeName)
            .IsFullString(CCenterCmts.CostCenterTypeTitle, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}