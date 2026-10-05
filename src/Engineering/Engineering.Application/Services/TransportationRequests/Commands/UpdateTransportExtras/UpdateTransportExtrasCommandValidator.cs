
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportExtras;

public class UpdateTransportExtrasCommandValidator : AbstractValidator<UpdateTransportExtrasCommand>
{
    public UpdateTransportExtrasCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
    }
}
