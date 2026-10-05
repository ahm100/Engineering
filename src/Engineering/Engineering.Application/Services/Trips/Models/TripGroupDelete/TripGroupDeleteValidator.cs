
namespace Engineering.Application.Services.Trips.Models.TripGroupDelete;

public class TripGroupDeleteValidator : AbstractValidator<TripGroupDeleteRequest>
{
    public TripGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
