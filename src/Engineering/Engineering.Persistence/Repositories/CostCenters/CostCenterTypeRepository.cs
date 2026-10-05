using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;
using Engineering.Domain.Entities.CostCenters;
using System.Diagnostics;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterTypeRepository : BaseRepository<EngineeringDBContext, CostCenterType>, ICostCenterTypeRepository
{

    public CostCenterTypeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<CostCenterType?> GetCostCenterTypeByName(
        string name,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostCenterTypeTitle.Equals(name));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCostCenterTypeByNameResponse?> GetCostCenterTypeByNameForResponse(
        string name,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostCenterTypeTitle.Equals(name))
            .Select(e => new GetCostCenterTypeByNameResponse
            {
                Id = e.Id,
                CostCenterTypeCode = e.CostCenterTypeCode,
                CostCenterTypeTitle = e.CostCenterTypeTitle,
                IsActive = e.IsActive
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<CostCenterType?> GetCostCenterTypeById(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                w.Id.Equals(id));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCostCenterTypeByIdResponse?> GetCostCenterTypeByIdForResponse(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                !w.IsDeleted &&
                w.Id.Equals(id))
            .Select(e => new GetCostCenterTypeByIdResponse
            {
                Id = e.Id,
                CostCenterTypeCode = e.CostCenterTypeCode,
                CostCenterTypeTitle = e.CostCenterTypeTitle,
                IsActive = e.IsActive
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public Task<bool> GetCostCenterTypeByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .AnyAsync(a =>
                (companyId == null || a.CompanyId.Equals(companyId)) &&
                names.Contains(a.CostCenterTypeTitle) ||
                codes.Contains(a.CostCenterTypeCode), ct);

        return query;
    }

    public async Task<CostCenterType?> GetCostCenterTypeByCode(
        string code,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostCenterTypeCode.Equals(code));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetCostCenterTypeByCodeResponse?> GetCostCenterTypeByCodeForResponse(
        string code,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CostCenterTypeCode.Equals(code))
            .Select(e => new GetCostCenterTypeByCodeResponse
            {
                Id = e.Id,
                CostCenterTypeCode = e.CostCenterTypeCode,
                CostCenterTypeTitle = e.CostCenterTypeTitle,
                IsActive = e.IsActive
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<GetsCostCenterTypeModel> Data, int RowCount)> GetsCostCenterType(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostCenterTypeCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostCenterTypeTitle, filterData.MakeLikePattern())))
            .Select(s => new GetsCostCenterTypeModel
            {
                Id = s.Id,
                CostCenterTypeTitle = s.CostCenterTypeTitle,
                CostCenterTypeCode = s.CostCenterTypeCode,
                IsActive = s.IsActive,
                CompanyId = s.CompanyId,
                Created = s.Created,
            });

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);

        query = query.OrderByDescending(c => c.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var CostCenterTypes = await query.ToListAsync(ct);
        return (CostCenterTypes, count);
    }

    public async Task<List<CostCenterType>> GetsCostCenterTypeByIds(
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
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                EF.Functions.IsNumeric(w.CostCenterTypeCode))
            .Select(s => Convert.ToInt64(s.CostCenterTypeCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<GetsActiveCostCenterTypesModel> Data, int RowCount)> GetsActiveCostCenterTypes(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.IsActive &&
                (code == null || w.CostCenterTypeCode.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostCenterTypeCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CostCenterTypeTitle, filterData.MakeLikePattern())) &&
                (name == null || w.CostCenterTypeTitle.Contains(name)))
            .Select(s => new GetsActiveCostCenterTypesModel
            {
                Id = s.Id,
                CostCenterTypeTitle = s.CostCenterTypeTitle,
                CostCenterTypeCode = s.CostCenterTypeCode
            });
        query = query.OrderBy(o => o.CostCenterTypeTitle);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}