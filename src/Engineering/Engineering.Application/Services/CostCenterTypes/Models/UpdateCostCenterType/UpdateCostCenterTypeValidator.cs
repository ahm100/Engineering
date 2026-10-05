namespace Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;

public class UpdateCostCenterTypeValidator : AbstractValidator<UpdateCostCenterTypeRequest>
{
    public UpdateCostCenterTypeValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(CCenterCmts.CostCenterTypeId);

        RuleFor(v => v.CostCenterTypeName)
            .IsFullString(CCenterCmts.CostCenterTypeTitle, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(v => v.CostCenterTypeCode)
            .IsFullString(CCenterCmts.CostCenterTypeCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}