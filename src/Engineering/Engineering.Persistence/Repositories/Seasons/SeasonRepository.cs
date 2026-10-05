using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.GetActiveSeasons;
using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;
using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;
using Engineering.Application.Services.Seasons.Models.GetSeasonById;
using Engineering.Application.Services.Seasons.Models.GetSeasonByName;
using Engineering.Application.Services.Seasons.Models.GetSeasons;
using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;
using Engineering.Application.Services.Seasons.Models.SeasonModels;
using Engineering.Persistence.Migrations;
using System.Text.RegularExpressions;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Persistence.Repositories.Seasons;

public class SeasonRepository : BaseRepository<EngineeringDBContext, Season>, ISeasonRepository
{
    public SeasonRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<Season?> FindByName(
        string name,
        long? companyId,
        long branchId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Where(oo => oo.SeasonName == name &&
            (companyId == null || oo.CompanyId == companyId) &&
            oo.BranchId == branchId &&
            !oo.Branch.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetSeasonByNameResponse?> FindByNameForResponse(
        string name,
        long? companyId,
        long branchId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Where(oo => oo.SeasonName == name &&
            (companyId == null || oo.CompanyId == companyId) &&
            oo.BranchId == branchId &&
            !oo.Branch.IsDeleted)
            .Select(e => new GetSeasonByNameResponse
            {
                Id = e.Id,
                SeasonName = e.SeasonName,
                SeasonCode = e.SeasonCode,
                BranchId = e.BranchId,
                IsActive = e.IsActive,
                CompanyId = companyId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Season?> FindByCode(
        string code,
        long branchId,
        long? companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Where(oo => oo.SeasonCode == code &&
            (companyId == null || oo.CompanyId == companyId) &&
            (oo.BranchId == branchId) &&
            !oo.Branch.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetSeasonByCodeResponse?> FindByCodeForResponse(
        string code,
        long branchId,
        long? companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Where(oo => oo.SeasonCode == code &&
            (companyId == null || oo.CompanyId == companyId) &&
            (oo.BranchId == branchId) &&
            !oo.Branch.IsDeleted)
            .Select(e => new GetSeasonByCodeResponse
            {
                Id = e.Id,
                BranchId = e.BranchId,
                SeasonName = e.SeasonName,
                SeasonCode = e.SeasonCode,
                IsActive = e.IsActive
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public async Task<Season?> HaveSeasonChild(
        long seasonId, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == seasonId && oo.OperationInfoSeasons.Any());

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<Season?> FindForDelete(
        long id, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == id)
             .Include(oo => oo.OperationInfoSeasons);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<Season?> FindByIdWithBranch(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .ThenInclude(x => x.Category)
            .Where(oo => oo.Id == id && !oo.Branch.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetSeasonByIdResponse?> FindByIdWithBranchForResponse(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .ThenInclude(x => x.Category)
            .Where(oo => oo.Id == id && !oo.Branch.IsDeleted)
            .Select(e => new GetSeasonByIdResponse
            {
                Id = e.Id,
                SeasonName = e.SeasonName,
                PreferentialReferenceCode = e.PreferentialReferenceCode,
                SeasonCode = e.SeasonCode,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId,
                BranchId = e.BranchId
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public async Task<string> CodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.SeasonCode)).Select(x => Convert.ToInt64(x.SeasonCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public Task<bool> FindSeasonByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long branchId,
        long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.SeasonName) ||
            codes.Contains(oo.SeasonCode),
            ct);

        return query;
    }

    public async Task<List<Season>> GetByCodes(
        List<string> catCodes,
        List<string> branchCodes,
        List<string> seasonCodes, CT ct)
    {
        return await DbSet
            .Where(oo =>
            catCodes.Contains(oo.Branch.Category.CategoryCode) ||
            branchCodes.Contains(oo.Branch.BranchCode) ||
            seasonCodes.Contains(oo.SeasonCode)).ToListAsync(ct);
    }

    public async Task<List<Season>> GetByCodesIncludeNavigations(
    List<string> categoryCodes,
    List<string> branchCodes,
    List<string> seasonCodes,
    long companyId,
    CT ct)
    {
        return await DbSet
            .Include(x => x.Branch)
                .ThenInclude(x => x.Category)
            .Where(x =>
                x.CompanyId == companyId &&
                categoryCodes.Contains(x.Branch.Category.CategoryCode) &&
                branchCodes.Contains(x.Branch.BranchCode) &&
                seasonCodes.Contains(x.SeasonCode))
            .ToListAsync(ct);
    }

    public async Task<(List<Season> Data, int RowCount)> GetSeasons(
        List<long>? ids,
        string? filterData,
        long? branchId,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
                .ThenInclude(x => x.Category)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.SeasonCode.Contains(code)) &&
            (name == null || oo.SeasonName.Contains(name)) &&
            (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonName, filterData.MakeLikePattern())) &&
            ((branchId == null || oo.Branch.Id == branchId) && !oo.Branch.IsDeleted) &&
            ((categoryId == null || oo.Branch.Category.Id == categoryId) && !oo.Branch.Category.IsDeleted));
        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);
        query = query
            .OrderBy(oo => !oo.IsActive);
        var allItems = await query.ToListAsync(ct);
        var numericItems = allItems
            .Where(x => Regex.IsMatch(x.SeasonCode, @"^d+$"))
            .OrderBy(x => int.Parse(x.SeasonCode));
        var nonNumericItems = allItems
            .Where(x => !Regex.IsMatch(x.SeasonCode, @"^d+$"))
            .OrderBy(x => x.SeasonCode);
        var combined = numericItems.Concat(nonNumericItems);
        if (pageIndex > 0 && pageSize > 0)
            combined = combined.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (combined.ToList(), allItems.Count);
    }

    public async Task<(List<GetSeasonsModel> Data, int RowCount)> GetSeasonsForResponse(
        List<long>? ids,
        string? filterData,
        long? branchId,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
                .ThenInclude(x => x.Category)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.SeasonCode.Contains(code)) &&
            (name == null || oo.SeasonName.Contains(name)) &&
            (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonName, filterData.MakeLikePattern())) &&
            ((branchId == null || oo.Branch.Id == branchId) && !oo.Branch.IsDeleted) &&
            ((categoryId == null || oo.Branch.Category.Id == categoryId) && !oo.Branch.Category.IsDeleted));
        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);
        query = query
            .OrderBy(oo => !oo.IsActive);
        var allItems = await query.ToListAsync(ct);
        var numericItems = allItems
            .Where(x => Regex.IsMatch(x.SeasonCode, @"^d+$"))
            .OrderBy(x => int.Parse(x.SeasonCode))
            .Select(e => new GetSeasonsModel(
                Id: e.Id,
                SeasonName: e.SeasonName,
                SeasonCode: e.SeasonCode,
                BranchId: e.BranchId,
                BranchName: null,
                BranchCode: null,
                IsActive: e.IsActive,
                CompanyId: e.CompanyId,
                CompanyNameFa: null));
        var nonNumericItems = allItems
            .Where(x => !Regex.IsMatch(x.SeasonCode, @"^d+$"))
            .OrderBy(x => x.SeasonCode)
            .Select(e => new GetSeasonsModel(
                Id: e.Id,
                SeasonName: e.SeasonName,
                SeasonCode: e.SeasonCode,
                BranchId: e.BranchId,
                BranchCode: null,
                BranchName: null,
                IsActive: e.IsActive,
                CompanyId: e.CompanyId,
                CompanyNameFa: null));
        var combined = numericItems.Concat(nonNumericItems);
        if (pageIndex > 0 && pageSize > 0)
            combined = combined.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (combined.ToList(), allItems.Count);
    }


    public async Task<(List<Season> Data, int RowCount)> GetActiveSeasons(
        string? filterData,
        long? branchId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.SeasonCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonName, filterData.MakeLikePattern())) &&
            (name == null || oo.SeasonName.Contains(name)) &&
            (branchId == null || oo.Branch.Id == branchId) &&
            oo.IsActive &&

            !oo.Branch.IsDeleted &&
            oo.Branch.IsActive
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsActiveSeasonModel> Data, int RowCount)> GetActiveSeasonsForResponse(
        string? filterData,
        long? branchId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.SeasonCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonName, filterData.MakeLikePattern())) &&
            (name == null || oo.SeasonName.Contains(name)) &&
            (branchId == null || oo.Branch.Id == branchId) &&
            oo.IsActive &&

            !oo.Branch.IsDeleted &&
            oo.Branch.IsActive
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .Select(e => new GetsActiveSeasonModel
            {
                Id = e.Id,
                SeasonCode = e.SeasonCode,
                SeasonName = e.SeasonName
            }).ToListAsync(ct);
        return (items, count);
    }


    public async Task<(List<Season> Data, int RowCount)> GetByBranchId(
        long branchId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)

            .Where(oo => oo.Branch.Id == branchId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsByBranchIdModel> Data, int RowCount)> GetByBranchIdForResponse(
        long branchId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)

            .Where(oo => oo.Branch.Id == branchId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .Select(e => new GetsByBranchIdModel
            {
                Id = e.Id,
                SeasonName = e.SeasonName,
                SeasonCode = e.SeasonCode,
                IsActive = e.IsActive,
                CompanyId = e.CompanyId
            })
            .ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Season> Data, int RowCount)> GetBySeasonIds(
        List<long> seasonIds,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch.Category)

            .Where(oo => seasonIds.Contains(oo.Id));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Season> Data, int RowCount)> GetBySeasonIdsIncludeLess(
        List<long> seasonIds,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
                .ThenInclude(x => x.Category)
            .Where(oo => seasonIds.Contains(oo.Id));

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Season> Data, int RowCount)> GetsSeasonByBranchIds(
        List<long> branchIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch.Category)

            .Where(oo => branchIds.Contains(oo.Branch.Id) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsSeasonByBranchIdsModel> Data, int RowCount)> GetsSeasonByBranchIdsForResponse(
        List<long> branchIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch.Category)

            .Where(oo => branchIds.Contains(oo.Branch.Id) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.SeasonName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .Select(e => new GetsSeasonByBranchIdsModel
            {
                Id = e.Id,
                BranchId = e.BranchId,
                SeasonCode = e.SeasonCode,
                SeasonName = e.SeasonName,
                IsActive = e.IsActive,
                AlternativeId = $"Season{e.Id}"
            }).ToListAsync(ct);
        return (items, count);
    }


    public async Task<(List<Season> Data, int RowCount)> GetsByBranchIdWhithOperationInfo(
        long branchId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Include(x => x.OperationInfoSeasons)

            .Where(oo => oo.Branch.Id == branchId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsByBranchIdWhithOperationInfoModel> Data, int RowCount)> GetsByBranchIdWhithOperationInfoForResponse(
        long branchId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .Include(x => x.OperationInfoSeasons)

            .Where(oo => oo.Branch.Id == branchId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .Select(e => new GetsByBranchIdWhithOperationInfoModel
            {
                Id = e.Id,
                SeasonName = e.SeasonName,
                SeasonCode = e.SeasonCode,
                AlternativeId = $"Season{e.Id}",
                IsActive = e.IsActive,
                HaveChild = e.OperationInfoSeasons.Any(e => !e.IsDeleted),
                ChildCount = e.OperationInfoSeasons.Count(e => !e.IsDeleted),
                CompanyId = e.CompanyId
            })
            .ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<Season>> GetByNames(
        List<string> seasonName, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .ThenInclude(x => x.Category)
            .Where(x => seasonName.Contains(x.SeasonName));

        return await query.ToListAsync();
    }

    public async Task<List<Season>> GetByBranchId(
        long branchId,
        long companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Branch)
            .ThenInclude(x => x.Category)
            .Where(x => x.BranchId == branchId
            && (x.CompanyId == companyId));

        return await query.ToListAsync();
    }

    public async Task AddRangeAsync(IEnumerable<Season> seasons, CT ct)
    {
        await DbSet.AddRangeAsync(seasons, ct);
    }

    public async Task<List<Season>> GetForRasteReshteImport(
    List<long> branchIds,
    List<string> seasonCodes,
    long companyId,
    CT ct)
    {
        return await DbSet
            .Where(x =>
                x.CompanyId == companyId &&
                !x.IsDeleted &&
                branchIds.Contains(x.BranchId) &&
                seasonCodes.Contains(x.SeasonCode))
            .ToListAsync(ct);
    }

    public async Task<List<Season>> GetForFehrestBahaImport(
    long branchId,
    List<string> seasonCodes,
    long companyId,
    CT ct)
    {
        if (seasonCodes == null || seasonCodes.Count == 0)
            return new List<Season>();

        return await DbSet
            .Where(x =>
                x.CompanyId == companyId &&
                x.BranchId == branchId &&
                !x.IsDeleted &&
                seasonCodes.Contains(x.SeasonCode))
            .ToListAsync(ct);
    }


}