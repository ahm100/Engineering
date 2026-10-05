namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByCodeForResponse;

public class GetSeasonByCodeForResponseQueryValidator : AbstractValidator<GetSeasonByCodeForResponseQuery>
{
    public GetSeasonByCodeForResponseQueryValidator()
    {
        RuleFor(oo => oo.SeasonCode).NotEmpty().WithError(SeasonErrors.SeasonCodeIsEmpty);
    }
}
