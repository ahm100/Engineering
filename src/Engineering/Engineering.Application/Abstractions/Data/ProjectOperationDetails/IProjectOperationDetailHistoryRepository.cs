using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails;

public interface IProjectOperationDetailHistoryRepository : IBaseRepository<ProjectOperationDetailHistory>
{
    Task<(List<ProjectOperationDetailHistory> Data, int RowCount)> GetHistoryByProjectOperationDetailId(
        long? projectOperationDetailId, int pageIndex, int pageSize, CT ct);
}