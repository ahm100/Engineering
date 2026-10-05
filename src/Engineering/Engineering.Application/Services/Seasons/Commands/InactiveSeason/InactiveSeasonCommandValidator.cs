namespace Engineering.Application.Services.Seasons.Commands.InactiveSeason;

public class InactiveSeasonCommandValidator : AbstractValidator<InactiveSeasonCommand>
{
    public InactiveSeasonCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(SeasonErrors.IdIsEmpty);
    }
}