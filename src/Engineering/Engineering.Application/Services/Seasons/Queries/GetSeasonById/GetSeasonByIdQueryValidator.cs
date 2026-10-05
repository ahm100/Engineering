namespace Engineering.Application.Services.Seasons.Queries.GetSeasonById;

public class GetSeasonByIdQueryValidator : AbstractValidator<GetSeasonByIdQuery>
{
    public GetSeasonByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(SeasonErrors.IdIsEmpty);
    }
}