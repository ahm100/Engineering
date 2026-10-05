using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerCostOverRepository : IBaseRepository<EmployerCostOver>
{
    Task<EmployerCostOver?> FindByIdWithCostOver(long id, CT ct);
    Task<EmployerCostOver?> HaveContractCostOverChild(long id, CT ct);

    Task<(List<EmployerCostOver> Data, int RowCount)> GetContractCostOvers(long? costOverId, long? employerId, string[]? orderBy, long? companyId, int pageIndex, int pageSize, CT ct);
    Task<(List<EmployerCostOver> Data, int RowCount)> GetByCostOverId(long costOverId, int pageIndex, int pageSize, CT ct);
}