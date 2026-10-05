using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.PaymentRequestMachineryStatusChange;

public class PaymentRequestMachineryStatusChangeCommandHandler : ICommandHandler<PaymentRequestMachineryStatusChangeCommand, RequestMachineryStatusStatement>
{
    private readonly ILogger<PaymentRequestMachineryStatusChangeCommandHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;
    private readonly IRequestMachineryRepository _requestMachineryRepository;

    public PaymentRequestMachineryStatusChangeCommandHandler(
        ILogger<PaymentRequestMachineryStatusChangeCommandHandler> logger,
        IRequestMachineryStatusStatementRepository repository,
        IRequestMachineryRepository requestMachineryRepository)
    {
        _logger = logger;
        _repository = repository;
        _requestMachineryRepository = requestMachineryRepository;
    }

    public async Task<Result<RequestMachineryStatusStatement?>> Handle(PaymentRequestMachineryStatusChangeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetStatusStatementById(request.RefrenceId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryStatusStatement>(RequestMachineryStatusStatementErrors.RequestMachineryStatusStatementWithIdNotFound);

            RequestMachineryStatusStatementStatus? statementStatus = null;
            RequestMachineryStatus? requestMachineryStatus = null;
            if (request.IsDeleted == false)
            {
                if (request.Status == PaymentOrderStatus.Rejected)
                {
                    statementStatus = RequestMachineryStatusStatementStatus.PaidRejected;
                    requestMachineryStatus = RequestMachineryStatus.PaymentRejected;
                }

                if (request.Status == PaymentOrderStatus.IncompletelyPaid)
                {
                    statementStatus = RequestMachineryStatusStatementStatus.IncompletelyPaid;
                    requestMachineryStatus = RequestMachineryStatus.IncompletelyPaid;
                }

                if (request.Status == PaymentOrderStatus.Paid)
                {
                    statementStatus = RequestMachineryStatusStatementStatus.PaidDone;
                    requestMachineryStatus = RequestMachineryStatus.Done;
                }
            }

            if (request.IsDeleted == true)
            {
                statementStatus = RequestMachineryStatusStatementStatus.Invalidated;
                requestMachineryStatus = RequestMachineryStatus.OnProject;
                entity.SetPaymentOrderId(null);
                entity.SetPaymentDate(null);
            }

            if (statementStatus.HasValue)
            {
                var requestMachineries = entity.RequestMachineryStatusStatementDetails.Select(x => x.RequestMachinery).ToList();
                foreach (var item in requestMachineries)
                    if (requestMachineryStatus.HasValue)
                    {
                        item.ChangeStatus(requestMachineryStatus.Value);

                        await _requestMachineryRepository.Update(item);
                    }

                entity.ChangeStatus(statementStatus.Value);

                await _repository.Update(entity);
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
