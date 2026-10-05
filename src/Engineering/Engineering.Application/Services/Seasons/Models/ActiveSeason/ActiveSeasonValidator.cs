namespace Engineering.Application.Services.Seasons.Models.ActiveSeason;

public class ActiveSeasonValidator : AbstractValidator<ActiveSeasonRequest>
{
    public ActiveSeasonValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.SeasonId);
    }
}
