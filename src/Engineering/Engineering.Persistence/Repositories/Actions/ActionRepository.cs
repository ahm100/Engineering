using Engineering.Application.Abstractions.Data.Actions;
using Action = Engineering.Domain.Entities.Actions.Action;

namespace Engineering.Persistence.Repositories.Actions;

public class ActionRepository : BaseRepository<EngineeringDBContext, Action>, IActionRepository
{
    public ActionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<string> CodeCreator(CT ct)
    {
        var query = await DbSet

            .Where(w =>
                EF.Functions.IsNumeric(w.ActionCode))
            .Select(s =>
                Convert.ToInt64(s.ActionCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<Action?> GetActionById(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<List<Action>> GetActions(
        List<long> ids, CT ct)
    {
        var query = DbSet

            .Where(w =>
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<List<Action>> GetsActiveActions(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (code == null || w.ActionCode.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.ActionCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.ActionName, filterData.MakeLikePattern())) &&
                (name == null || w.ActionName.Contains(name)) && w.IsActive);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<List<Action>> GetFilteredActions(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (code == null || w.ActionCode.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.ActionCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.ActionName, filterData.MakeLikePattern())) &&
                (name == null || w.ActionName.Contains(name)));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<Action?> GetActionByName(
        string name,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.ActionName == name);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Action?> GetActionByCode(
        string code,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.ActionCode == code);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }
}
