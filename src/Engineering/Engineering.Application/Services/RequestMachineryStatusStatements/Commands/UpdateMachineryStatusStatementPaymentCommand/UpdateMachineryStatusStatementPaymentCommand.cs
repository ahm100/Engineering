using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.UpdateRequestMachineryStatusStatementPaymentOrder;

public record UpdateMachineryStatusStatementPaymentCommand(
    RequestMachineryStatusStatement StatusStatement,
    List<RequestMachinery> RequestMachineries,
    long PaymentOrderId,
    RequestMachineryStatusStatementStatus Status) : ICommand<RequestMachineryStatusStatement>;
