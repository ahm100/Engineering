
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperation;

public record GetsEmployerStatusStatementProjectOperationQuery(
    long EmployerStatusStatementId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<EmployerStatusStatementProjectOperation>>>;