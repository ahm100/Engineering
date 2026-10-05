
namespace Engineering.Application.Services.Seasons.Commands.ActiveSeason;

public class ActiveSeasonCommandValidator : AbstractValidator<ActiveSeasonCommand>
{
    public ActiveSeasonCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(SeasonErrors.IdIsEmpty);
    }
}