namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByNameForRespone;

public class GetSeasonByNameForResponeQueryValidator : AbstractValidator<GetSeasonByNameForResponeQuery>
{
    public GetSeasonByNameForResponeQueryValidator()
    {
        RuleFor(oo => oo.SeasonName).NotEmpty().WithError(SeasonErrors.SeasonNameIsEmpty);
    }
}
