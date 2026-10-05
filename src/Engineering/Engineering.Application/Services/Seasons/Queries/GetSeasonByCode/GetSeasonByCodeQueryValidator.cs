namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByCode;

public class GetSeasonByCodeQueryValidator : AbstractValidator<GetSeasonByCodeQuery>
{
    public GetSeasonByCodeQueryValidator()
    {
        RuleFor(oo => oo.SeasonCode).NotEmpty().WithError(SeasonErrors.SeasonCodeIsEmpty);
    }
}