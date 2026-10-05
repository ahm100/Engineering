
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToSendDoneTransportation;

public class ChangeToSendDoneTransportationValidator : AbstractValidator<ChangeToSendDoneTransportationRequest>
{
    public ChangeToSendDoneTransportationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(TransportationRequestErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
