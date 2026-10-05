
namespace Engineering.Application.Services.Transportations.Models.TransportationGroupDelete;

public class TransportationGroupDeleteValidator : AbstractValidator<TransportationGroupDeleteRequest>
{
    public TransportationGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
