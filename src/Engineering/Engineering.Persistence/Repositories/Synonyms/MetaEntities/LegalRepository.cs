using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.MetaData.Legals;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewLegalRepository : BaseRepository<EngineeringDBContext, ViewLegal>, IViewLegalRepository
{
    public ViewLegalRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewLegal?> GetById(long id, CT ct)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<(List<ViewLegal> Data, int RowCount)> GetLegalsByIds(List<long> ids, int pageIndex, int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => ids.Contains(x.Id));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0)
                     .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewLegal> Data, int RowCount)> GetAllActiveLegals(string? filterData, int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.IsActive
                                     && (string.IsNullOrWhiteSpace(filterData) || x.CompanyName.Contains(filterData)
                                                                               || x.RegistrationNo
                                                                                   .Contains(filterData)));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0)
                     .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }
}