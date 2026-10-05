using Engineering.Application.Services.Seasons.Models.GetSeasonById;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByIdForResponse;

public class GetSeasonByIdForResponseQueryValidator : AbstractValidator<GetSeasonByIdForResponseQuery>
{
    public GetSeasonByIdForResponseQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(SeasonErrors.IdIsEmpty);
    }
}
