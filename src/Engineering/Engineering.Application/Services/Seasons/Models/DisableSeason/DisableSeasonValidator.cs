namespace Engineering.Application.Services.Seasons.Models.DisableSeason;

public class DisableSeasonValidator : AbstractValidator<DisableSeasonRequest>
{
    public DisableSeasonValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.SeasonId);
    }
}
