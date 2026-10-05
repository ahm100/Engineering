using Engineering.Application.Abstractions.Data.Projects;
using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectTechnicalAssistantRepository : BaseRepository<EngineeringDBContext, ProjectTechnicalAssistant>, IProjectTechnicalAssistantRepository
{
    public ProjectTechnicalAssistantRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<ProjectTechnicalAssistant> Data, int RowCount)> GetImpProjectTechnicalAssistantByProjectId(long projectId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(o => o.Project.Id == projectId);

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<ProjectTechnicalAssistant?> FindUser(long userId, long projectId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.TechnicalAssistantUserId == userId && oo.Project.Id == projectId);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }
}