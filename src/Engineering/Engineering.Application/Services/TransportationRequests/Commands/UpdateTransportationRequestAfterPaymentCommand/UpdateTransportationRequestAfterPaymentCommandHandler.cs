using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportationRequestAfterPaymentCommand;

public class UpdateTransportationRequestAfterPaymentCommandHandler : ICommandHandler<UpdateTransportationRequestAfterPaymentCommand, TransportationRequest>
{
    private readonly ILogger<UpdateTransportationRequestAfterPaymentCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateTransportationRequestAfterPaymentCommandHandler(
        ILogger<UpdateTransportationRequestAfterPaymentCommand> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateTransportationRequestAfterPaymentCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

            entity.SetPaymentOrderId(request.PaymentOrderId);
            entity.SetPaymentDate(request.PaymentDate);
            entity.SetManagerDescription(request.Description);

            entity.SetTransportationRequestStatus(request.PaymentType == TransportationPaymentType.Panel4818 || request.PaymentType == TransportationPaymentType.PettyCash ?
                TransportationRequestStatus.PaidDone : TransportationRequestStatus.Paid);

            entity.SetTransportationPaymentType(request.PaymentType);

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