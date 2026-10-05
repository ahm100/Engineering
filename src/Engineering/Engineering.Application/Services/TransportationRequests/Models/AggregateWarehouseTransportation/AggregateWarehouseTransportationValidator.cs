namespace Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;

public class AggregateWarehouseTransportationValidator : AbstractValidator<AggregateWarehouseTransportationRequest>
{
    public AggregateWarehouseTransportationValidator()
    {
        RuleForEach(oo => oo.Cargos).NotNull().NotEmpty().WithError(TransportationRequestErrors.CargoIsEmpty);
    }
}