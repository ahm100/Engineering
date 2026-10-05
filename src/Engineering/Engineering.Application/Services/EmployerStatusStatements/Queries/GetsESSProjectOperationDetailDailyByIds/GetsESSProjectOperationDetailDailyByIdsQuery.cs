
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsESSProjectOperationDetailDailyByIds;

public record GetsESSProjectOperationDetailDailyByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>>;
