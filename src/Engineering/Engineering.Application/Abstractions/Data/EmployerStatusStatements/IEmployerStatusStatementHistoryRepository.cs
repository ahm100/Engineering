using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Abstractions.Data.EmployerStatusStatements;

public interface IEmployerStatusStatementHistoryRepository : IBaseRepository<EmployerStatusStatementHistory>
{
    Task<(List<EmployerStatusStatementHistory> Data, int RowCount)> GetsEmployerStatusStatementHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);
}
