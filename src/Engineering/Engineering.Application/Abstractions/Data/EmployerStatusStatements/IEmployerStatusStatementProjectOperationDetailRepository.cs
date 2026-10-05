using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Abstractions.Data.EmployerStatusStatements;

public interface IEmployerStatusStatementProjectOperationDetailRepository : IBaseRepository<EmployerStatusStatementProjectOperationDetail>
{
    Task<(List<EmployerStatusStatementProjectOperationDetail> Data, int RowCount)> GetsEmployerStatusStatementProjectOperationDetail(
        long employerStatusStatementId,
        long? employerStatusStatementProjectOperationId,
        int pageIndex,
        int pageSize,
        CT ct);

}
