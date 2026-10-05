using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Abstractions.Data.ContractorStatusStatements;

public interface IContractorStatusStatementDiscountRepository : IBaseRepository<ContractorStatusStatementDiscount>
{
    Task<(List<ContractorStatusStatementDiscount> Data, int RowCount)> GetsContractorStatusStatementDiscountById(
        long contractorStatusStatementId,
        int pageIndex,
        int pageSize,
        CT ct);

}
