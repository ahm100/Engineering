namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByName;

public class GetSeasonByNameQueryValidator : AbstractValidator<GetSeasonByNameQuery>
{
    public GetSeasonByNameQueryValidator()
    {
        RuleFor(oo => oo.SeasonName).NotEmpty().WithError(SeasonErrors.SeasonNameIsEmpty);
    }
}