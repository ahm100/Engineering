using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAggregateTransportationWarehouse;

public class UpdateAggregateTransportationWarehouseCommandHandler : ICommandHandler<UpdateAggregateTransportationWarehouseCommand, TransportationRequest>
{
    private readonly ILogger<UpdateAggregateTransportationWarehouseCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateAggregateTransportationWarehouseCommandHandler(
        ILogger<UpdateAggregateTransportationWarehouseCommand> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateAggregateTransportationWarehouseCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnvalidPackingWarehouse);

            var result = request.WarehousePrices?.Select(x => (x.Id, x.ShippingPrice)).ToList();

            entity.UpdateAfterAggregate(request.TransferPrice, request.Description, request.MachineType,
                request.DriverId, request.NumberPlate, request.CertificateNumber, request.PostageDate,
                request.DetailId, request.GlobalFreightNumber, request.ClassifiedFreightNumber, request.Tax,
                request.DetailTransferPrice, request.ServicePrice, request.InsuranceNumber, request.InsurancePrice,
                request.ShippingCost, request.ProductTotalPrice, request.OutofRange, request.OrderNumber,
                request.LoadWeight, request.Documents, result);

            await _repository.Update(entity);
            return request.TransportationRequest;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}