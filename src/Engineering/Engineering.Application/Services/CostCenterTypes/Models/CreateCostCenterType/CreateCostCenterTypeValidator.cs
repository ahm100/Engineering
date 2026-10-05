namespace Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;

public class CreateCostCenterTypeValidator : AbstractValidator<CreateCostCenterTypeRequest>
{
    public CreateCostCenterTypeValidator()
    {
        RuleFor(v => v.CostCenterTypeName)
            .IsFullString(CCenterCmts.CostCenterName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(v => v.CostCenterTypeCode)
            .IsFullString(CCenterCmts.CostCenterCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}