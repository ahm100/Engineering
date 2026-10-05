namespace Engineering.Application.Services.Seasons.Models.GetSeasonByCode;

public class GetSeasonByCodeValidator : AbstractValidator<GetSeasonByCodeRequest>
{
    public GetSeasonByCodeValidator()
    {
        RuleFor(oo => oo.SeasonCode)
            .IsFullString(SeasonCmts.SeasonCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
