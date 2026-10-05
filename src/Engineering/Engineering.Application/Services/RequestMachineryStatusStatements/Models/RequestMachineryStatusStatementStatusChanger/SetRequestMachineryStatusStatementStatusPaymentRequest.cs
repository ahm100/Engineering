namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

public record SetRequestMachineryStatusStatementStatusPaymentRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
