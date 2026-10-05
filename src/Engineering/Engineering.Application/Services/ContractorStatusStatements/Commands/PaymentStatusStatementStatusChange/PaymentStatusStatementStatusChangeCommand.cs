using Engineering.Domain.Entities.ContractorStatusStatements;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.PaymentStatusStatementStatusChange;

public record PaymentStatusStatementStatusChangeCommand(
    long RefrenceId,
    long PaymentOrderId,
    decimal? FilledAmount,
    PaymentOrderStatus Status
    ) : ICommand<ContractorStatusStatement>;
