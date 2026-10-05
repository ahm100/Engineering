namespace Engineering.Application.Services.Seasons.Commands.CreateSeason;

public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
{
    public CreateSeasonCommandValidator()
    {
        RuleFor(oo => oo.Branch).NotEmpty().WithError(SeasonErrors.BranchIsEmpty);
        RuleFor(oo => oo.SeasonName).NotEmpty().WithError(SeasonErrors.SeasonNameIsEmpty);
        RuleFor(oo => oo.SeasonCode).NotEmpty().WithError(SeasonErrors.SeasonCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(SeasonErrors.IsActiveIsEmpty);
    }
}