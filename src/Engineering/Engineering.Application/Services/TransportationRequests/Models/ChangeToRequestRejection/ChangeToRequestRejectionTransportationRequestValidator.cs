
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestRejection;

public class ChangeToRequestRejectionTransportationRequestValidator : AbstractValidator<ChangeToRequestRejectionTransportationRequestRequest>
{
    public ChangeToRequestRejectionTransportationRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.ManagerDescription).NotEmpty().WithError(TransportationRequestErrors.ManagerDescriptionIsEmpty);
    }
}
