using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAfterReforms;

public class UpdateAfterReformsCommandHandler : ICommandHandler<UpdateAfterReformsCommand, TransportationRequest>
{
    private readonly ILogger<UpdateAfterReformsCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateAfterReformsCommandHandler(
        ILogger<UpdateAfterReformsCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationRequestWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateAfterReformsCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;
            if (entity is null)
                return Result.Failure<TransportationRequest>
                    (TransportationRequestErrors.TransportationRequestWithIdNotFound);

            entity.UpdateWarehouseTransportExtras(request.Detail,
                request.TransferPrice, entity.Volume, request.LoadWeight);

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