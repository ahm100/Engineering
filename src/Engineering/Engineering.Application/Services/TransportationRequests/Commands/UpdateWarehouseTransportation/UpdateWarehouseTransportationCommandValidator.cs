
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseTransportation;

public class UpdateWarehouseTransportationCommandValidator : AbstractValidator<UpdateWarehouseTransportationCommand>
{
    public UpdateWarehouseTransportationCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
    }
}
