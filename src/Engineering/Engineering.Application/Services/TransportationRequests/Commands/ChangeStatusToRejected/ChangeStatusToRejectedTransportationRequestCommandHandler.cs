using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToRejected;

public class ChangeStatusToRejectedTransportationRequestCommandHandler : ICommandHandler<ChangeStatusToRejectedTransportationRequestCommand, TransportationRequest>
{
    private readonly ILogger<ChangeStatusToRejectedTransportationRequestCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public ChangeStatusToRejectedTransportationRequestCommandHandler(ILogger<ChangeStatusToRejectedTransportationRequestCommand> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(ChangeStatusToRejectedTransportationRequestCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

            entity.SetTransportationRequestStatus(Domain.Entities.Transportations.Enums.TransportationRequestStatus.RequestRejection);
            entity.SetManagerDescription(request.ManagerDescription);
            entity.SetConfirmDate(request.RejectDate);
            entity.SetConfirmUserId(request.RejectUserId);

            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}