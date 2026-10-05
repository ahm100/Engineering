
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementHistory;

public record GetsEmployerStatusStatementHistoryRequest(
    long EmployerStatusStatementId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
