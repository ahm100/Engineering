
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsESSProjectOperationByIds;

public record GetsESSProjectOperationByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<EmployerStatusStatementProjectOperation>>>;