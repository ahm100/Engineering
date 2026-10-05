
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToPending;

public class ChangeToPendingTransportationRequestValidator : AbstractValidator<ChangeToPendingTransportationRequestRequest>
{
    public ChangeToPendingTransportationRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
