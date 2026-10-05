using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.RequestMachineryStatusStatementStatusChanger;

public record RequestMachineryStatusStatementStatusChangerCommand(
    RequestMachineryStatusStatement Entity,
    RequestMachineryStatusStatementStatus Status
    ) : ICommand<RequestMachineryStatusStatement>;
