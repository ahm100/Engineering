namespace Engineering.Application.Services.TransportationRequests.Models.UpdateAggregateTransportationWarehouse;

public class UpdateAggregateTransportationWarehouseRequestValidator : AbstractValidator<UpdateAggregateTransportationWarehouseRequest>
{
    public UpdateAggregateTransportationWarehouseRequestValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(TransportationContractorCmts.TransportationRequestId);
        RuleFor(oo => oo.DetailId).IsPositive(TransportationContractorCmts.TransportationRequestDetailId);
        RuleFor(oo => oo.MachineTypeId).IsPositive(TransportationContractorMachineCmts.MachineTypeId);
        RuleFor(oo => oo.DriverId).IsPositive(TransportationContractorMachineCmts.DriverId);
    }
}