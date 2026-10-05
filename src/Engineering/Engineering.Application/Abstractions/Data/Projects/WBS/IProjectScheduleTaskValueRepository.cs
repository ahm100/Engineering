using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectScheduleTaskValueRepository : IBaseRepository<ProjectScheduleTaskValue>
{
    Task<ProjectScheduleTaskValue?> GetById(
        long id, CT ct);

    Task<List<ProjectScheduleTaskValue>?> GetByColumnId(
        long columnId, CT ct);

    Task<ProjectScheduleTaskValue?> GetByTaskAndColumnId(
        long columnId, long taskId, CT ct);

    Task<List<ProjectScheduleTaskValue>> GetByImportId(
        long importId, CT ct);
}
