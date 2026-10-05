namespace Engineering.Application.Services.Seasons.Queries.GetSeasonsForResponse;

public class GetSeasonsForResponseQueryValidator : AbstractValidator<GetSeasonsForResponseQuery>
{
    public GetSeasonsForResponseQueryValidator()
    {
        RuleFor(oo => oo.SeasonName).NotEmpty().WithError(SeasonErrors.SeasonNameIsEmpty);
    }
}
