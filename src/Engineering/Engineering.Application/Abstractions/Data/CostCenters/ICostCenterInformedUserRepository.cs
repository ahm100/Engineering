using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterInformedUserRepository : IBaseRepository<CostCenterInformedUser>
{
    Task<(List<CostCenterInformedUser> Data, int RowCount)> GetCostCenterInformedUserByCostCenter(long Id, int pageIndex, int pageSize, CT ct);
    Task<(List<CostCenterInformedUser> Data, int RowCount)> GetInformedUsersByCostCenter(long Id, CT ct);
    Task<CostCenterInformedUser?> FindUser(long userId, long costCenterId, CT ct);
}
