
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperationDetail;

public record GetsEmployerStatusStatementProjectOperationDetailQuery(
    long EmployerStatusStatementId,
    long? EmployerStatusStatementProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<EmployerStatusStatementProjectOperationDetail>>>;
