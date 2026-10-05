
namespace Engineering.Application.Services.TransportationRequests.Models.TransportationRequestGroupDelete;

public class TransportationRequestGroupDeleteValidator : AbstractValidator<TransportationRequestGroupDeleteRequest>
{
    public TransportationRequestGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
