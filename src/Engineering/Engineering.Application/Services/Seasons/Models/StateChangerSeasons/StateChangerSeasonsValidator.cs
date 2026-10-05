
namespace Engineering.Application.Services.Seasons.Models.StateChangerSeasons;

public class StateChangerSeasonsValidator : AbstractValidator<StateChangerSeasonsRequest>
{
    public StateChangerSeasonsValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.SeasonId);

    }
}
