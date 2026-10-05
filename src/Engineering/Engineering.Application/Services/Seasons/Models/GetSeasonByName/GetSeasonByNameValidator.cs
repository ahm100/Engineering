namespace Engineering.Application.Services.Seasons.Models.GetSeasonByName;

public class GetSeasonByNameValidator : AbstractValidator<GetSeasonByNameRequest>
{
    public GetSeasonByNameValidator()
    {
        RuleFor(oo => oo.SeasonName)
            .IsFullString(SeasonCmts.SeasonName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
