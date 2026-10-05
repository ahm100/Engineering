
namespace Engineering.Application.Services.Trips.Queries.GetsTripByIds;

public class GetsTripByIdsQueryValidator : AbstractValidator<GetsTripByIdsQuery>
{
    public GetsTripByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
