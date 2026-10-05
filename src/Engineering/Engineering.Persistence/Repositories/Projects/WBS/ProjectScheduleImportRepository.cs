using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectScheduleImportRepository : BaseRepository<EngineeringDBContext, ProjectScheduleImport>, IProjectScheduleImportRepository
{
    public ProjectScheduleImportRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectScheduleImport?> GetById(
        long id, CT ct)
     {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<Project?> GetProjectByImportId(
        long id, CT ct)
    {
        return await DbSet
            .Where(e => e.Id == id).Select(e => e.Project).FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectScheduleImport?> GetByProjectId(
        long projectId, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x =>
            x.ProjectId == projectId &&
            x.Status != ProjectScheduleImportStatus.Archived, ct);
    }

    public async Task<List<ProjectScheduleImport>?> GetNonArchived(
        long projectId, CT ct)
    {
        return await DbSet
            .Where(x =>
            x.ProjectId == projectId &&
            x.Status != ProjectScheduleImportStatus.Archived)
            .ToListAsync(ct);
    }
}