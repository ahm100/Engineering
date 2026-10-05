using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;
using Engineering.Application.Services.CostOvers.Models.GetCostOverById;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;
using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Persistence.Repositories.CostOvers;

public class CostOverRepository : BaseRepository<EngineeringDBContext, CostOver>, ICostOverRepository
{
    public CostOverRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<CostOver?> GetCostOverByName(
        string costOverName,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostOverName.Equals(costOverName));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCostOverByNameResponse?> GetCostOverByNameForResponse(
        string costOverName,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostOverName.Equals(costOverName))
            .Select(e => new GetCostOverByNameResponse
            {
                Id = e.Id,
                CostOverName = e.CostOverName,
                CostOverCode = e.CostOverCode,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    /// <summary>رکورد حذف نشده را همراه Tracking برای عملیات واحد کار دریافت می کند.</summary>
    public async Task<CostOver?> GetCostOverById(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                w.Id.Equals(id)).AsTracking();

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCostOverByIdResponse?> GetCostOverByIdForResponse(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                w.Id.Equals(id)).AsTracking()
            .Select(e => new GetCostOverByIdResponse
            {
                Id = e.Id,
                ApprovalStatus = e.ApprovalStatus,
                CurrentApprovalAttemptId = e.CurrentApprovalAttemptId,
                CostOverName = e.CostOverName,
                CostOverCode = e.CostOverCode,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<CostOver?> GetCostOverByCode(
        string costOverCode,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostOverCode.Equals(costOverCode));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCostOverByCodeResponse?> GetCostOverByCodeForResponse(
        string costOverCode,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostOverCode.Equals(costOverCode))
            .Select(e => new GetCostOverByCodeResponse
            {
                Id = e.Id,
                CostOverCode = e.CostOverCode,
                CostOverName = e.CostOverName,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public Task<bool> GetsCostOverByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .AnyAsync(a =>
                (companyId == null || a.CompanyId.Equals(companyId)) ||
                names.Contains(a.CostOverName) ||
                codes.Contains(a.CostOverCode), ct);

        return query;
    }

    public async Task<(List<GetsCostOversModel> Data, int RowCount)> GetsCostOvers(
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
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                (code == null || w.CostOverCode.Contains(code)) &&
                (name == null || w.CostOverName.Contains(name)) &&
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverName, filterData.MakeLikePattern())))
            .Select(s => new GetsCostOversModel
            {
                ApprovalStatus = s.ApprovalStatus,
                CurrentApprovalAttemptId = s.CurrentApprovalAttemptId,
                Id = s.Id,
                CostOverName = s.CostOverName,
                CostOverCode = s.CostOverCode,
                IsActive = s.IsActive,
                CompanyId = s.CompanyId
            });

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);

        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.CostOverName);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costOvers = await query.ToListAsync(ct);
        return (costOvers, count);
    }

    public async Task<(List<CostOver> Data, int RowCount)> GetsCostOversByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                ids.Contains(w.Id));

        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);

        var count = await query.CountAsync(ct);
        var costOvers = await query.ToListAsync(ct);
        return (costOvers, count);
    }

    public async Task<(List<CostOver> Data, int RowCount)> GetsByNameOrCode(
        string filterData,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverName, filterData.MakeLikePattern())));

        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costOvers = await query.ToListAsync(ct);
        return (costOvers, count);
    }

    public async Task<(List<GetsCostOverByNameOrCodeModel> Data, int RowCount)> GetsCostOverByNameOrCode(
        string filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverName, filterData.MakeLikePattern())))
            .Select(s => new GetsCostOverByNameOrCodeModel
            {
                Id = s.Id,
                CostOverName = s.CostOverName,
                CostOverCode = s.CostOverCode,
                IsActive = s.IsActive
            });

        query = query.OrderBy(o => !o.IsActive).ThenByDescending(t => t.CostOverName);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costOvers = await query.ToListAsync(ct);
        return (costOvers, count);
    }


    public async Task<(List<GetsActiveCostOversModel> Data, int RowCount)> GetsActiveCostOvers(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.IsActive &&
                !w.IsDeleted &&
                (code == null || w.CostOverCode.Contains(code)) &&
                (name == null || w.CostOverName.Contains(name)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostOverName, filterData.MakeLikePattern())))
            .Select(s => new GetsActiveCostOversModel
            {
                Id = s.Id,
                CostOverName = s.CostOverName,
                CostOverCode = s.CostOverCode
            });

        query = query.OrderByDescending(o => o.CostOverName);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costOvers = await query.ToListAsync(ct);
        return (costOvers, count);
    }

    public async Task<List<CostOver>> GetsCostOverByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<string> CodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                EF.Functions.IsNumeric(w.CostOverCode))
            .Select(s => Convert.ToInt64(s.CostOverCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }


}
