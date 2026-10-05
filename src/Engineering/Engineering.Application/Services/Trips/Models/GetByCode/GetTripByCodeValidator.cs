namespace Engineering.Application.Services.Trips.Models.GetByCode;

public class GetTripByCodeValidator : AbstractValidator<GetTripByCodeRequest>
{
    public GetTripByCodeValidator()
    {
        RuleFor(oo => oo.TripCode).NotEmpty().WithError(TripErrors.CodeIsEmpty);
    }
}
