using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Repositories.Projects.WBS;

public class ProjectWbsRepository : BaseRepository<EngineeringDBContext, ProjectWbs>, IProjectWbsRepository
{
    public ProjectWbsRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectWbs?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperationWbses)
            .Include(x => x.Childs)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectWbs>?> GetByProjectScheduleImportId(
        long id, CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectScheduleImportId == id)
            .ToListAsync(ct);
    }


    public async Task<List<long>?> GetWbsIdsToNotShow(
        long projectId,
        long? parentId,
        CT ct)
    {
        var query = DbSet

            .Where(x =>
                x.ProjectId == projectId &&
                (parentId == null && x.ParentId == null ||
                parentId != null && x.ParentId == parentId) ||
                parentId == x.Id)
            .Select(x => x.Id);

        var item = await query.ToListAsync(ct);
        return item;
    }
}