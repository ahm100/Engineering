
namespace Engineering.Application.Services.Seasons.Commands.UpdateSeason;

public class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(SeasonErrors.IdIsEmpty);
        RuleFor(oo => oo.SeasonName).NotEmpty().WithError(SeasonErrors.SeasonNameIsEmpty);
        RuleFor(oo => oo.SeasonCode).NotEmpty().WithError(SeasonErrors.SeasonCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(SeasonErrors.IsActiveIsEmpty);
    }
}