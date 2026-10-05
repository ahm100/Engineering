using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.PaymentRequestMachineryStatusChange;

public record PaymentRequestMachineryStatusChangeCommand(
    long RefrenceId,
    PaymentOrderStatus? Status,
    bool IsDeleted
    ) : ICommand<RequestMachineryStatusStatement>;
