namespace Engineering.Application.Services.CostCenters.Models.UpdateCostCenter;

public class UpdateCostCenterValidator : AbstractValidator<UpdateCostCenterRequest>
{
    public UpdateCostCenterValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.CostCenterTypeId)
            .IsPositive(CCenterCmts.CostCenterTypeId);
        RuleFor(oo => oo.CostCenterName)
            .IsFullString(CCenterCmts.CostCenterName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.CostCenterCode)
            .IsFullString(CCenterCmts.CostCenterCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.CityId)
            .IsPositive(GlobalCmts.CityId);
        RuleFor(oo => oo.Address)
            .HasMaxLength(CCenterCmts.Address, 1500);
        RuleFor(oo => oo.WeatherState)
            .IsRequiredBool(CCenterCmts.WeatherState);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
