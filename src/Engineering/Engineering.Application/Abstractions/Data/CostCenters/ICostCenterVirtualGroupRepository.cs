using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterVirtualGroupRepository : IBaseRepository<CostCenterVirtualGroup>
{
    Task<(List<CostCenterVirtualGroup> Data, int RowCount)> GetByCostCenterAsync(long costCenterId, int pageIndex, int pageSize, CT ct);
}
