using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectScheduleTaskDependencyRepository :
    IBaseRepository<ProjectScheduleTaskDependency>
{
    Task<ProjectScheduleTaskDependency?> GetById(
        long id, CT ct);

    Task<List<ProjectScheduleTaskDependency>?> GetByTaskIds(
        List<long> ids, CT ct);

    Task<List<ProjectScheduleTaskDependency>?> GetBySuccessorTaskIds(
        List<long> ids, CT ct);
}