
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailDaily;

public record GetsEmployerStatusStatementProjectOperationDetailDailyRequest(
    long EmployerStatusStatementId,
    long? EmployerStatusStatementProjectOperationId,
    long? EmployerStatusStatementProjectOperationDetailId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
