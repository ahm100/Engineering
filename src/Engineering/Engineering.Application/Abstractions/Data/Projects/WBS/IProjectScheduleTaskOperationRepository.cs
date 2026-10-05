using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectScheduleTaskOperationRepository :
    IBaseRepository<ProjectScheduleTaskOperation>
{
    Task<ProjectScheduleTaskOperation?> GetById(
        long id, CT ct);

    Task<List<ProjectScheduleTaskOperation>?> GetByTaskIds(
        List<long> ids, CT ct);
}