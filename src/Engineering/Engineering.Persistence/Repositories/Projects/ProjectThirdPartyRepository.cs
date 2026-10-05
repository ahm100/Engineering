using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectThirdParties;
using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectThirdPartyRepository : BaseRepository<EngineeringDBContext, ProjectThirdParty>, IProjectThirdPartyRepository
{
    public ProjectThirdPartyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectThirdParty?> GetProjectThirdPartyById(
        long id,
        CT ct)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectThirdParty>?> GetProjectThirdPartyByIds(
        List<long> ids,
        CT ct)
    {
        return await DbSet.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
    }

    public async Task<ProjectThirdParty?> GetByProjectAndThirdPartyId(
        long projectId,
        long thirdPartyId,
        CT ct)
    {
        return await DbSet.Where(x => projectId == x.ProjectId && thirdPartyId == x.AuthorizedThirdPartyId).FirstOrDefaultAsync(ct);
    }

    public async Task<bool> HasThirdParty(
        long projectId,
        CT ct)
    {
        return await DbSet.AnyAsync(
            x => x.ProjectId == projectId,
            ct);
    }

    public async Task<(List<GetFltrProjectThirdPartyModel>? Data, int RowCount)> GetFltrProjectThirdParty(
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => projectId == x.ProjectId).Select(x => new GetFltrProjectThirdPartyModel
        {
            Id = x.Id,
            ThirdPartyId = x.AuthorizedThirdPartyId,
        });

        var count = await query.CountAsync();

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync();

        return (baseQuery, count);
    }

}
