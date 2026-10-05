using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

public record RequestMachineryStatusStatementStatusChangerRequest(
    long Id,
    RequestMachineryStatusStatementStatus Status
    ) : IHttpRequest;
