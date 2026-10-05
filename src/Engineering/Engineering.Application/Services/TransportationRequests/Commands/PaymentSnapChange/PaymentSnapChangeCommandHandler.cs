using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.TransportationRequests.Commands.PaymentSnapChange;

public class PaymentSnapChangeCommandHandler : ICommandHandler<PaymentSnapChangeCommand, bool?>
{
    private readonly ILogger<PaymentSnapChangeCommandHandler> _logger;
    private readonly ITransportationRequestRepository _repository;

    public PaymentSnapChangeCommandHandler(
        ILogger<PaymentSnapChangeCommandHandler> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(PaymentSnapChangeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetSnapsWithRefrenceId(request.RefrenceId, ct);
            if (entity is null)
                return Result.Failure<bool?>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

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
                entity.SetPaymentDate(null);
                entity.SetPaymentOrderId(null);
                entity.SetTransportationPaymentType(TransportationPaymentType.NotPaid);
            }

            if (status.HasValue)
                if (entity is not null)
                {
                    entity.SetTransportationRequestStatus(status.Value);

                    await _repository.Update(entity);
                }

            entity.AddHistory();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
