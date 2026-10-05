using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Abstractions.Data.ContractorStatusStatements;

public interface IContractorStatusStatementHistoryRepository : IBaseRepository<ContractorStatusStatementHistory>
{

    Task<(List<GetsContractorStatusStatementHistoryModel>? Data, int RowCount)> GetsContractorStatusStatementHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);

}
