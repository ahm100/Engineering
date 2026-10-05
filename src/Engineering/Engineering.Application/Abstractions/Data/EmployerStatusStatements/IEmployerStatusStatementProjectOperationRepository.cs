using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Abstractions.Data.EmployerStatusStatements;

public interface IEmployerStatusStatementProjectOperationRepository : IBaseRepository<EmployerStatusStatementProjectOperation>
{
    Task<(List<EmployerStatusStatementProjectOperation> Data, int RowCount)> GetsEmployerStatusStatementProjectOperation(long employerStatusStatementId, int pageIndex, int pageSize, CT ct);
    Task<(List<EmployerStatusStatementProjectOperation> Data, int RowCount)> GetsESSProjectOperationByIds(List<long> ids, CT ct);

}
