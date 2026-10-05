using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterHistoryRepository : IBaseRepository<CostCenterHistory>
{
    public Task<List<GetCostCenterHistoriesModel>> GetCostCenterHistories(
        long id,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
}