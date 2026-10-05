using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToAccepted;

public class ChangeStatusToAcceptedTransportationRequestCommandHandler : ICommandHandler<ChangeStatusToAcceptedTransportationRequestCommand, TransportationRequest>
{
    private readonly ILogger<ChangeStatusToAcceptedTransportationRequestCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public ChangeStatusToAcceptedTransportationRequestCommandHandler(ILogger<ChangeStatusToAcceptedTransportationRequestCommand> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(ChangeStatusToAcceptedTransportationRequestCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors
                    .TransportationRequestWithIdNotFound);

            if (request.PanelPaid == true)
            {
                entity.SetTransportationRequestStatus(TransportationRequestStatus.PaidDone);
                entity.SetTransportationPaymentType(TransportationPaymentType.Panel4818);
                entity.SetPaymentDate(DateTime.Now);
            }
            else
                entity.SetTransportationRequestStatus(TransportationRequestStatus.Accepted);

            entity.SetManagerDescription(request.ManagerDescription);
            entity.SetConfirmDate(request.ConfrimDate);
            entity.SetConfirmUserId(request.ConfrimUserId);

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