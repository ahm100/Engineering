using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectScheduleImportRepository :
    IBaseRepository<ProjectScheduleImport>
{
    Task<ProjectScheduleImport?> GetById(
        long id,
        CT ct);

    Task<Project?> GetProjectByImportId(
        long id, CT ct);

    Task<ProjectScheduleImport?> GetByProjectId(
        long projectId,
        CT ct);

    Task<List<ProjectScheduleImport>?> GetNonArchived(
        long projectId,
        CT ct);

}