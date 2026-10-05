namespace Engineering.Application.Services.Seasons.Queries.HaveSeasonChild;

public class HaveSeasonChildQueryValidator : AbstractValidator<HaveSeasonChildQuery>
{
    public HaveSeasonChildQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(SeasonErrors.IdIsEmpty);
    }
}