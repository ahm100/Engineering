
namespace Engineering.Application.Services.Trips.Models.GetById;

public class GetTripByIdValidator : AbstractValidator<GetTripByIdRequest>
{
    public GetTripByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TripErrors.IdIsEmpty);
    }
}
