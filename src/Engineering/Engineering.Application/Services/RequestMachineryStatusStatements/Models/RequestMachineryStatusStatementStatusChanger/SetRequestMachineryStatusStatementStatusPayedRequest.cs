namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

public record SetRequestMachineryStatusStatementStatusPaidRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
