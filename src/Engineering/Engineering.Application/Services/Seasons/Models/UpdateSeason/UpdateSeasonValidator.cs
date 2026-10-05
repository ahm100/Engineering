namespace Engineering.Application.Services.Seasons.Models.UpdateSeason;

public class UpdateSeasonValidator : AbstractValidator<UpdateSeasonRequest>
{
    public UpdateSeasonValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.SeasonId);

        RuleFor(oo => oo.SeasonName)
            .IsFullString(SeasonCmts.SeasonName, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(oo => oo.SeasonCode)
            .IsFullString(SeasonCmts.SeasonName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
