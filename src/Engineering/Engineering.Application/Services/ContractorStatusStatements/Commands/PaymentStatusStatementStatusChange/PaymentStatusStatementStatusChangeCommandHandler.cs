using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.PaymentStatusStatementStatusChange;

public class PaymentStatusStatementStatusChangeCommandHandler : ICommandHandler<PaymentStatusStatementStatusChangeCommand, ContractorStatusStatement>
{
    private readonly ILogger<PaymentStatusStatementStatusChangeCommandHandler> _logger;
    private readonly IContractorStatusStatementRepository _repository;
    public PaymentStatusStatementStatusChangeCommandHandler(
        ILogger<PaymentStatusStatementStatusChangeCommandHandler> logger,
        IContractorStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatement?>> Handle(PaymentStatusStatementStatusChangeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorStatusStatementByIdNoInclude(request.RefrenceId, ct);
            if (entity is null)
                return Result.Failure<ContractorStatusStatement>(CSSErrors.ContractorStatusStatementWithIdNotFound);

            var statusMap = new Dictionary<PaymentOrderStatus, CSSStatus>
            {
                { PaymentOrderStatus.Rejected, CSSStatus.RejectPaid },
                { PaymentOrderStatus.IncompletelyPaid, CSSStatus.IncompletelyPaid },
                { PaymentOrderStatus.Paid, CSSStatus.Paid }
            };

            if (statusMap.TryGetValue(request.Status, out var statementStatus))
            {
                entity.ChangeStatus(statementStatus, null);
                entity.AddHistory();

                if (entity.ContractorStatusStatementPayments.Any(x => x.PaymentOrderId == request.PaymentOrderId))
                    entity.ContractorStatusStatementPayments.FirstOrDefault(x => x.PaymentOrderId == request.PaymentOrderId)!
                        .SetTreasuryPay(statementStatus, request.FilledAmount ?? 0);

                await _repository.Update(entity);
            }


            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
