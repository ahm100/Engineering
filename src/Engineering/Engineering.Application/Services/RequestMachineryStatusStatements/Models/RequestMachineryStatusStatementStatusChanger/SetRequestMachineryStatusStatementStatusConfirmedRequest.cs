namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

public record SetRequestMachineryStatusStatementStatusConfirmedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
