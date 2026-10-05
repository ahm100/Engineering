namespace Engineering.Application.Services.CostCenters.Models.CreateCostCenter;

public class CreateCostCenterValidator : AbstractValidator<CreateCostCenterRequest>
{
    public CreateCostCenterValidator()
    {
        RuleFor(oo => oo.CostCenterTypeId)
            .IsPositive(CCenterCmts.CostCenterTypeId);
        RuleFor(oo => oo.CostCenterName)
            .IsFullString(CCenterCmts.CostCenterName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.CostCenterCode)
            .IsFullString(CCenterCmts.CostCenterCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.CityId)
            .IsPositive(CCenterCmts.CostCenterTypeId);
        RuleFor(oo => oo.Address)
            .HasMaxLength(CCenterCmts.Address, 1500);
        RuleFor(oo => oo.WeatherState)
            .IsRequiredBool(CCenterCmts.WeatherState);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
