using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Abstractions.Data.EmployerStatusStatements;

public interface IEmployerStatusStatementProjectOperationDetailDailyRepository : IBaseRepository<EmployerStatusStatementProjectOperationDetailDaily>
{

    Task<(List<EmployerStatusStatementProjectOperationDetailDaily> Data, int RowCount)> GetsEmployerStatusStatementProjectOperationDetailDaily(
        long employerStatusStatementId,
        long? employerStatusStatementProjectOperationId,
        long? employerStatusStatementProjectOperationDetailId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<EmployerStatusStatementProjectOperationDetailDaily> Data, int RowCount)> GetsESSProjectOperationDetailDailyByIds(
        List<long> ids,
        CT ct);
}
