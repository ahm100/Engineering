
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperation;

public record GetsEmployerStatusStatementProjectOperationRequest(
    long EmployerStatusStatementId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
