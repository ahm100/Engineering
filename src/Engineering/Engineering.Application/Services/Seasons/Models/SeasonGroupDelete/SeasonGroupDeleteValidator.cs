
namespace Engineering.Application.Services.Seasons.Models.SeasonGroupDelete;

public class SeasonGroupDeleteValidator : AbstractValidator<SeasonGroupDeleteRequest>
{
    public SeasonGroupDeleteValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.SeasonId);
    }
}
