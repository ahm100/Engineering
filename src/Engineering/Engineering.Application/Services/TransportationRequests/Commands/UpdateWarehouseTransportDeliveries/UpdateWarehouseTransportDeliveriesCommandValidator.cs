
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseTransportDeliveries;

public class UpdateWarehouseTransportDeliveriesCommandValidator : AbstractValidator<UpdateWarehouseTransportDeliveriesCommand>
{
    public UpdateWarehouseTransportDeliveriesCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.RequestNotValid);
    }
}
