
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToAccepted;

public class ChangeToAcceptedTransportationRequestValidator : AbstractValidator<ChangeToAcceptedTransportationRequestRequest>
{
    public ChangeToAcceptedTransportationRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(TransportationRequestErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
