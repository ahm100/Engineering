namespace Engineering.Application.Services.TransportationRequests.Models.UpdateMachineDriver;

public class UpdateMachineDriverRequestValidator : AbstractValidator<UpdateMachineDriverRequest>
{
    public UpdateMachineDriverRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.MachineTypeId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.MachineIdIsEmpty);
        RuleFor(oo => oo.DriverId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.DriverIdNotValid);
        RuleFor(oo => oo.NumberPlates).NotNull().NotEmpty().WithError(TransportationRequestErrors.NumberPlatesInValid);
    }
}