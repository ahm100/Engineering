using Engineering.Application.Abstractions.Data.Projects;
using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectImplementationAssistantRepository : BaseRepository<EngineeringDBContext, ProjectImplementationAssistant>, IProjectImplementationAssistantRepository
{
    public ProjectImplementationAssistantRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<ProjectImplementationAssistant> Data, int RowCount)> GetImplementationAssistansByProjectId(
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
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

    public async Task<ProjectImplementationAssistant?> FindUser(
        long userId,
        long projectId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ImplementationAssistantUserId == userId && oo.Project.Id == projectId);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }
}