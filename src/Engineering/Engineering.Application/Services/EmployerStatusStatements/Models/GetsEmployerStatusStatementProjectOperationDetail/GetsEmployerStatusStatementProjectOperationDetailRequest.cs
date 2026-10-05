
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetail;

public record GetsEmployerStatusStatementProjectOperationDetailRequest(
    long EmployerStatusStatementId,
    long? EmployerStatusStatementProjectOperationId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
