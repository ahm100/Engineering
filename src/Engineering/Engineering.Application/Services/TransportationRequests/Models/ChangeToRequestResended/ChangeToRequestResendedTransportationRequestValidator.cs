
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestResended;

public class ChangeToRequestResendedTransportationRequestValidator : AbstractValidator<ChangeToRequestResendedTransportationRequestRequest>
{
    public ChangeToRequestResendedTransportationRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.ManagerDescription).NotEmpty().WithError(TransportationRequestErrors.ManagerDescriptionIsEmpty);
    }
}
