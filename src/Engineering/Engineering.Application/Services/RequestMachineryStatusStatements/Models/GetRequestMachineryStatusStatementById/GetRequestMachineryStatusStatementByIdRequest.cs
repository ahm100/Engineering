
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementById;

public record GetRequestMachineryStatusStatementByIdRequest(
    long Id
    ) : IHttpRequest;
