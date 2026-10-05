namespace Engineering.Application.Services.Seasons.Commands.StateChangerSeasons;

public class StateChangerSeasonsCommandValidator : AbstractValidator<StateChangerSeasonsCommand>
{
    public StateChangerSeasonsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
