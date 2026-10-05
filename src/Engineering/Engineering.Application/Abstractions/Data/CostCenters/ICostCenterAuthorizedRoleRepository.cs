using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterAuthorizedRoleRepository : IBaseRepository<CostCenterAuthorizedRole>
{
    Task<(List<CostCenterAuthorizedRole> Data, int RowCount)> GetAuthorizedRolesByCostCenter(long Id, int pageIndex, int pageSize, CT ct);
    Task<(List<CostCenterAuthorizedRole> Data, int RowCount)> GetAuthorizedRolesByCostCenter(long Id, CT ct);
    Task<CostCenterAuthorizedRole?> FindRole(long roleId, long costCenterId, CT ct);
}
