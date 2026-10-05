using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseTransportation;

public class UpdateWarehouseTransportationCommandHandler : ICommandHandler<UpdateWarehouseTransportationCommand, TransportationRequest>
{
    private readonly ILogger<UpdateWarehouseTransportationCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateWarehouseTransportationCommandHandler(
        ILogger<UpdateWarehouseTransportationCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationRequestWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateWarehouseTransportationCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

            entity.UpdateWarehouseTransport(request.Detail, request.MachineType, request.TransferPrice,
                request.DriverId, request.NumberPlate, request.CertificateNumber, request.Volume,
                request.DocumentUrls);

            entity.SetTransportationRequestStatus(TransportationRequestStatus.DriverAssignment);
            entity.SetFreightNumber(request.FreightNumber);

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