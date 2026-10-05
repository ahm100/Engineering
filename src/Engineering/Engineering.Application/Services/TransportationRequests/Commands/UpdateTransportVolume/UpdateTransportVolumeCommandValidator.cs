
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportVolume;

public class UpdateTransportVolumeCommandValidator : AbstractValidator<UpdateTransportVolumeCommand>
{
    public UpdateTransportVolumeCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.RequestNotValid);
        RuleFor(oo => oo.Volume).NotNull().GreaterThan(0).WithError(TransportationRequestErrors.VolumeIsEmpty);
    }
}
