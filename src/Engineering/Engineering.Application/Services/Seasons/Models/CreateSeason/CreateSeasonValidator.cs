namespace Engineering.Application.Services.Seasons.Models.CreateSeason;

public class CreateSeasonValidator : AbstractValidator<CreateSeasonRequest>
{
    public CreateSeasonValidator()
    {
        RuleFor(oo => oo.BranchId)
            .IsPositive(GlobalCmts.BranchId);
        RuleFor(oo => oo.SeasonName)
            .IsFullString(SeasonCmts.SeasonName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.SeasonCode)
            .IsFullString(SeasonCmts.SeasonCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);

    }
}
