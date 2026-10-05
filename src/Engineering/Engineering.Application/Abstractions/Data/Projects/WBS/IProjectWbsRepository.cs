using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectWbsRepository : IBaseRepository<ProjectWbs>
{
    Task<ProjectWbs?> GetById(
        long id, CT ct);

    Task<List<ProjectWbs>?> GetByProjectScheduleImportId(
        long id, CT ct);

    Task<List<long>?> GetWbsIdsToNotShow(
        long projectId,
        long? parentId,
        CT ct);
}