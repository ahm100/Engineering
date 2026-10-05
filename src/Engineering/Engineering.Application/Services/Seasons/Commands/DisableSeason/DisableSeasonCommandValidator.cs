
namespace Engineering.Application.Services.Seasons.Commands.DisableSeason;

public class DisableSeasonCommandValidator : AbstractValidator<DisableSeasonCommand>
{
    public DisableSeasonCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(SeasonErrors.IdIsEmpty);
    }
}