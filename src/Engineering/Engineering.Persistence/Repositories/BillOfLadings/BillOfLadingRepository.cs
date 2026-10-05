using Engineering.Application.Abstractions.Data.BillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;
using BillOfLading = Engineering.Domain.Entities.BillOfLadings.BillOfLading;

namespace Engineering.Persistence.Repositories.BillOfLadings;

public class BillOfLadingRepository : BaseRepository<EngineeringDBContext, BillOfLading>, IBillOfLadingRepository
{
    public BillOfLadingRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<BillOfLading?> GetBillOfLadingById(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetBillOfLadingByIdResponse?> GetBillOfLadingByIdForResponse(long id, CT ct)
    {
        var query = DbSet
            .Where(w => w.Id.Equals(id) && !w.IsDeleted)
            .Select(s => new GetBillOfLadingByIdResponse
            {
                Id = s.Id,
                BillOfLadingName = s.BillOfLadingName,
                BillOfLadingCode = s.BillOfLadingCode,
                IsActive = s.IsActive
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<BillOfLading>> GetBillOfLadings(
        List<long> ids, CT ct)
    {
        var query = DbSet

            .Where(w =>
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<BillOfLading?> GetBillOfLadingByName(
        string name,
        long? companyId,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                w.BillOfLadingName == name);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<BillOfLading?> GetBillOfLadingByCode(
        string code,
        long? companyId,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                w.BillOfLadingCode == code);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public Task<bool> GetsBillOfLadingByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .AnyAsync(a =>
                (companyId == null || a.CompanyId == companyId) &&
                names.Contains(a.BillOfLadingName) ||
                codes.Contains(a.BillOfLadingCode), ct);

        return query;
    }

    public async Task<string> CodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                EF.Functions.IsNumeric(w.BillOfLadingCode))
            .Select(s =>
                Convert.ToInt64(s.BillOfLadingCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<GetsFilteredBillOfLadingResponseModel> Data, int RowCount)> GetsFilteredBillOfLading(
        List<long>? ids,
        string? filterData,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var baseQuery = GetsFilteredBillOfLadingQuery(ids, filterData, code, name, isActive, companyId);
        var count = await baseQuery.CountAsync(ct);

        var projected = baseQuery.Select(s => new GetsFilteredBillOfLadingResponseModel
        {
            Id = s.Id,
            BillOfLadingName = s.BillOfLadingName,
            BillOfLadingCode = s.BillOfLadingCode,
            IsActive = s.IsActive
        });

        if (orderBy?.Length > 0)
            projected = projected.SortBy(orderBy);
        if (pageIndex > 0 || pageSize > 0)
            projected = projected.Page(pageIndex, pageSize);

        var items = await projected.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsBillOfLadingExcelExporterModel> Data, int RowCount)> GetsFilteredBillOfLadingForExcel(
        List<long>? ids,
        string? filterData,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var baseQuery = GetsFilteredBillOfLadingQuery(ids, filterData, code, name, isActive, companyId);
        var count = await baseQuery.CountAsync(ct);

        var projected = baseQuery.Select(s => new GetsBillOfLadingExcelExporterModel
        {
            Id = s.Id,
            BillOfLadingName = s.BillOfLadingName,
            BillOfLadingCode = s.BillOfLadingCode,
            IsActive = s.IsActive
        });

        if (orderBy?.Length > 0)
            projected = projected.SortBy(orderBy);
        if (pageIndex > 0 || pageSize > 0)
            projected = projected.Page(pageIndex, pageSize);

        var items = await projected.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsActiveBillOfLadingResponseModel> Data, int RowCount)> GetsActiveBillOfLading(
        string? filterData,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                (code == null || w.BillOfLadingCode.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BillOfLadingCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BillOfLadingName, filterData.MakeLikePattern())) &&
                (name == null || w.BillOfLadingName.Contains(name)) && w.IsActive);

        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .Select(e => new GetsActiveBillOfLadingResponseModel
            {
                Id = e.Id,
                BillOfLadingName = e.BillOfLadingName,
                BillOfLadingCode = e.BillOfLadingCode
            }).ToListAsync(ct);
        return (items, count);
    }


    private IQueryable<BillOfLading> GetsFilteredBillOfLadingQuery(
        List<long>? ids,
        string? filterData,
        string? code,
        string? name,
        bool? isActive,
        long? companyId)
    {
        var query = DbSet
            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                (code == null || w.BillOfLadingCode.Contains(code)) &&
                (name == null || w.BillOfLadingName.Contains(name)) &&
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BillOfLadingCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BillOfLadingName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);

        return query.OrderBy(o => !o.IsActive).ThenByDescending(t => t.Created);
    }
}