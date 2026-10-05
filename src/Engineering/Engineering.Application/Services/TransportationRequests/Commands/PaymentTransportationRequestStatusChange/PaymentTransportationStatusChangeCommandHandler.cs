using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.TransportationRequests.Commands.PaymentTransportationStatusChange;

public class PaymentTransportationStatusChangeCommandHandler : ICommandHandler<PaymentTransportationStatusChangeCommand, TransportationRequest>
{
    private readonly ILogger<PaymentTransportationStatusChangeCommandHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public PaymentTransportationStatusChangeCommandHandler(
        ILogger<PaymentTransportationStatusChangeCommandHandler> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(PaymentTransportationStatusChangeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.RefrenceId, ct);
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

            TransportationRequestStatus? status = null;

            if (request.IsDeleted == false)
            {
                if (request.Status == PaymentOrderStatus.Rejected)
                    status = TransportationRequestStatus.PaymentRejected;

                if (request.Status == PaymentOrderStatus.IncompletelyPaid)
                    status = TransportationRequestStatus.IncompletelyPaid;

                if (request.Status == PaymentOrderStatus.Paid)
                    status = TransportationRequestStatus.PaidDone;
            }

            if (request.IsDeleted == true)
            {
                status = TransportationRequestStatus.Accepted;
                entity.SetPaymentOrderId(null);
                entity.SetPaymentDate(null);
                entity.SetTransportationPaymentType(TransportationPaymentType.NotPaid);
            }

            if (status.HasValue)
            {
                entity.SetTransportationRequestStatus(status.Value);

                await _repository.Update(entity);
            }

            entity.AddHistory();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}
