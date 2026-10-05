using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectScheduleTaskRepository :
    IBaseRepository<ProjectScheduleTask>
{
    Task<ProjectScheduleTask?> GetById(
        long id, CT ct);

    Task<List<ProjectScheduleTask>?> GetByProjectScheduleImportId(
        long id, CT ct);

    Task<bool> HasChildren(
        long id, CT ct);

    Task<int> GetMaxMppUid(
        long importId, CT ct);

    Task<int> GetMaxMppId(
        long importId, CT ct);
}