using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterAuthorizedUserRepository : IBaseRepository<CostCenterAuthorizedUser>
{
    Task<(List<CostCenterAuthorizedUser> Data, int RowCount)> GetAuthorizedUsersByCostCenter(long Id, int pageIndex, int pageSize, CT ct);
    Task<(List<CostCenterAuthorizedUser> Data, int RowCount)> GetAuthorizedUsersByCostCenter(long Id, CT ct);
    Task<CostCenterAuthorizedUser?> FindUser(long userId, long costCenterId, CT ct);
}