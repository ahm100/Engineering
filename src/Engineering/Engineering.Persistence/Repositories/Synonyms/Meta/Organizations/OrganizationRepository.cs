using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Domain.Entities.Synonyms.MetaData.Organizations;

namespace Engineering.Persistence.Repositories.Synonyms.Meta.Organizations;

public class ViewOrganizationRepository : BaseRepository<EngineeringDBContext, ViewOrganization>, IViewOrganizationRepository
{
    public ViewOrganizationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewOrganization?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ViewOrganization>?> GetByIds(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id)).ToListAsync(ct);
    }

    public async Task<List<long>?> GetByManagerId(
        long managerId, CT ct)
    {
        return await DbSet
            .Where(x => x.ManagerId != null && x.ManagerId == managerId)
            .Select(x => x.Id).ToListAsync(ct);
    }
}