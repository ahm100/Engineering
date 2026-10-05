namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

public record SetRequestMachineryStatusStatementStatusRejectedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
