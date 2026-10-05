namespace Engineering.Application.Services.Seasons.Models.InactiveSeason;

public class InactiveSeasonValidator : AbstractValidator<InactiveSeasonRequest>
{
    public InactiveSeasonValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.SeasonId);
    }
}
