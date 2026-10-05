using Engineering.Domain.Entities.GoodsManager;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IGoodsManagerAssignmentHistoryRepository
    : IBaseRepository<GoodsManagerAssignmentHistory>
{
    Task<(List<GoodsManagerAssignmentHistory> Data, int RowCount)> GetByAssignmentId(
        long assignmentId,
        int pageIndex,
        int pageSize,
        CT ct);
}