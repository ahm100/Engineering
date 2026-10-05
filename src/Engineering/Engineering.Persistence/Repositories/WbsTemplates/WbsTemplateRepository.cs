using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;
using Engineering.Domain.Entities.WbsTemplates;

namespace Engineering.Persistence.Repositories.WbsTemplates;

public class WbsTemplateRepository : BaseRepository<EngineeringDBContext, WbsTemplate>, IWbsTemplateRepository
{
    public WbsTemplateRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<WbsTemplate?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectWbses)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<GetWbsTemplateByIdResponse?> GetWbsTemplateById(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetWbsTemplateByIdResponse
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                Description = x.Description,
                IsActive = x.IsActive
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFltrWbsTemplateModel>? Data, int RowCount)> GetFltrWbsTemplate(
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Title, filterData.MakeLikePattern()))
            .Select(x => new GetFltrWbsTemplateModel
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                Description = x.Description,
                IsActive = x.IsActive
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<(List<GetWbsTemplateForProjectModel>? Data, int RowCount)> GetWbsTemplateForProject(
        string? filterData,
        List<long>? notShow,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x =>
            (notShow == null || notShow.Count < 1 || !notShow.Contains(x.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Title, filterData.MakeLikePattern())))
            .Select(x => new GetWbsTemplateForProjectModel
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                Description = x.Description,
                IsActive = x.IsActive
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<WbsTemplate?> IsDuplicateTitle(
        string title,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.Title == title);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<WbsTemplate?> IsDuplicateCode(
        string code,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.Code == code);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }
}