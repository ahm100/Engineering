using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseTransportDeliveries;

public class UpdateWarehouseTransportDeliveriesCommandHandler : ICommandHandler<UpdateWarehouseTransportDeliveriesCommand, TransportationRequest>
{
    private readonly ILogger<UpdateWarehouseTransportDeliveriesCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateWarehouseTransportDeliveriesCommandHandler(
        ILogger<UpdateWarehouseTransportDeliveriesCommand> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateWarehouseTransportDeliveriesCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;

            entity.SetDeliveryMethod(request.DeliveryMethod);
            entity.SetDeliveryType(request.DeliveryType);
            entity.SetPostageDate(request.PostageDate);
            entity.SetDescription(request.Description);

            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}