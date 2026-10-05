
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementHistory;

public record GetsEmployerStatusStatementHistoryQuery(
    long EmployerStatusStatementId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<EmployerStatusStatementHistory>>>;