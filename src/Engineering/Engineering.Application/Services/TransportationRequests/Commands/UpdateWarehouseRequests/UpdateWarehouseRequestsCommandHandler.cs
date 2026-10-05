using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseRequests;

public class UpdateWarehouseRequestsCommandHandler : ICommandHandler<UpdateWarehouseRequestsCommand, TransportationRequest>
{
    private readonly ILogger<UpdateWarehouseRequestsCommand> _logger;
    private readonly ITransportationRequestWarehouseRepository _warehouseRepository;

    public UpdateWarehouseRequestsCommandHandler(
        ILogger<UpdateWarehouseRequestsCommand> logger,
        ITransportationRequestWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateWarehouseRequestsCommand request, CT ct)
    {
        try
        {
            var entities = request.Warehouses;
            if (entities is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnvalidPackingWarehouse);

            //foreach (var item in entities)
            //{
            //    item.SetRefrenceId(item.TransportationRequestId);

            //    if (item.TransportationRequest.TransportationContractor!.Type == TransportationContractorCalculateType.Distance)
            //    {
            //        var shippingCost = request.ShippingCosts?.FirstOrDefault(x =>
            //        x.SourceCityId == item.TransportationRequest.StartingCityId &&
            //        x.DestinationCityId == item.TransportationRequest.DestinationCityId);
            //        if (shippingCost is not null)
            //            item.SetShippingCost(shippingCost);
            //    }

            //    item.SetTransportRequest(request.TransportationRequest);
            //    await _warehouseRepository.Update(item);
            //}

            return request.TransportationRequest;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}