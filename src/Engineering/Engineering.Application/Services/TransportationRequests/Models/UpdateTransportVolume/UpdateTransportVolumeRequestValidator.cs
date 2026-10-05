namespace Engineering.Application.Services.TransportationRequests.Models.UpdateTransportVolume;

public class UpdateTransportVolumeRequestValidator : AbstractValidator<UpdateTransportVolumeRequest>
{
    public UpdateTransportVolumeRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.Volume).NotNull().GreaterThanOrEqualTo(0).NotEmpty().WithError(TransportationRequestErrors.VolumeIsEmpty);
    }
}