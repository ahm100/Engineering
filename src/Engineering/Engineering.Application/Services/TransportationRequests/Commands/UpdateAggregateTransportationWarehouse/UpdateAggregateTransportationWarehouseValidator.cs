
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAggregateTransportationWarehouse;

public class UpdateAggregateTransportationWarehouseValidator : AbstractValidator<UpdateAggregateTransportationWarehouseCommand>
{
    public UpdateAggregateTransportationWarehouseValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
    }
}
