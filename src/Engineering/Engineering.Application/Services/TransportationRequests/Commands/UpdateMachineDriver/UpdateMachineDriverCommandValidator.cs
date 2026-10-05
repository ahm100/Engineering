
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateMachineDriver;

public class UpdateMachineDriverCommandValidator : AbstractValidator<UpdateMachineDriverCommand>
{
    public UpdateMachineDriverCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.RequestNotValid);
        RuleFor(oo => oo.MachineType).NotNull().WithError(TransportationRequestErrors.MachineIdIsEmpty);
        RuleFor(oo => oo.DriverId).NotNull().WithError(TransportationRequestErrors.DriverIdNotValid);
        RuleFor(oo => oo.NumberPlate).NotNull().WithError(TransportationRequestErrors.NumberPlatesInValid);
    }
}
