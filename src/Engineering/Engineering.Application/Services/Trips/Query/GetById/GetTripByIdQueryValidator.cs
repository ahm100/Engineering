
namespace Engineering.Application.Services.Trips.Queries.GetById;

public class GetTripByIdQueryValidator : AbstractValidator<GetTripByIdQuery>
{
    public GetTripByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TripErrors.IdIsEmpty);
    }
}
