
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperationDetailDaily;

public record GetsEmployerStatusStatementProjectOperationDetailDailyQuery(
    long EmployerStatusStatementId,
    long? EmployerStatusStatementProjectOperationId,
    long? EmployerStatusStatementProjectOperationDetailId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>>;
