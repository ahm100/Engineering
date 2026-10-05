using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Abstractions.Data.ProjectOperations;

public interface IProjectOperationTemporaryDailyRepository : IBaseRepository<ProjectOperationTemporaryDaily>
{
    Task<ProjectOperationTemporaryDaily?> GetById(long id, CT ct);
    Task<(List<ProjectOperationTemporaryDaily> Data, int RowCount)> GetProjectOperationTemporaryDailies(
        long? costCenterId, long? projectId, long? projectOperationId, DateTime? startDate, DateTime? endTime,
        TemporaryDailyStatus? TemporaryDailyStatus, string? filterData, string[]? orderBy, int pageIndex, int pageSize,
        CT ct);
    Task<(List<ProjectOperationTemporaryDaily> Data, int RowCount)> GetCurrentUserTemporaryDailies(
        long creatorId,
        long? costCenterId,
        long? projectId,
        long? projectOperationId,
        DateTime? startDate,
        DateTime? endTime,
        TemporaryDailyStatus? TemporaryDailyStatus,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);
}