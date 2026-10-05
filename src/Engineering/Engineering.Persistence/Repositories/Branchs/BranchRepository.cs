using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.BranchModels;
using Engineering.Application.Services.Branchs.Models.GetBranchByCode;
using Engineering.Application.Services.Branchs.Models.GetBranchByName;
using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;
using Engineering.Application.Services.Branchs.Models.GetsBranchs;
using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;
using Financial.Application.Extensions;
using System.Text.RegularExpressions;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Persistence.Repositories.Branchs;

public class BranchRepository : BaseRepository<EngineeringDBContext, Branch>, IBranchRepository
{
    public BranchRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetBranchByNameResponse?> GetBranchByName(
        string name,
        long categoryId,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.BranchName.Equals(name) &&
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CategoryId == categoryId &&
                !w.Category.IsDeleted &&
                !w.IsDeleted)
            .Select(s => new GetBranchByNameResponse
            {
                Id = s.Id,
                BranchName = s.BranchName,
                BranchCode = s.BranchCode,
                AlternativeId = s.Id.ToString(),
                CategoryId = s.Category.Id,
                CategoryCode = s.Category.CategoryCode,
                CategoryName = s.Category.CategoryName,
                IsActive = s.IsActive,
                Category = new BranchCategoryModel
                {
                    Id = s.Category.Id,
                    CategoryName = s.Category.CategoryName,
                    CategoryCode = s.Category.CategoryCode,
                    IsActive = s.Category.IsActive
                }
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public Task<bool> GetBranchByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long categoryId,
        long? companyId, CT ct)
    {
        var query = DbSet

            .AnyAsync(a =>
                (companyId == null || a.CompanyId.Equals(companyId)) &&
                names.Contains(a.BranchName) ||
                codes.Contains(a.BranchCode), ct);

        return query;
    }

    public async Task<Branch?> GetBranchWithoutInclude(
        long id, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.Id.Equals(id));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<GetBranchByCodeResponse?> GetBranchByCode(
        string code,
        long categoryId,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.BranchCode.Equals(code) &&
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CategoryId == categoryId &&
                !w.Category.IsDeleted &&
                !w.IsDeleted)
            .Select(s => new GetBranchByCodeResponse
            {
                Id = s.Id,
                BranchName = s.BranchName,
                BranchCode = s.BranchCode,
                AlternativeId = s.Id.ToString(),
                CategoryId = s.Category.Id,
                CategoryCode = s.Category.CategoryCode,
                CategoryName = s.Category.CategoryName,
                IsActive = s.IsActive,
                Category = new BranchCategoryModel
                {
                    Id = s.Category.Id,
                    CategoryName = s.Category.CategoryName,
                    CategoryCode = s.Category.CategoryCode,
                    IsActive = s.Category.IsActive
                }
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Branch?> GetBranchByIdWithCategory(
        long id, CT ct)
    {
        var query = DbSet

            .Include(i => i.Category)
            .Include(i => i.Seasons)
            .Where(w =>
                w.Id.Equals(id) &&
                !w.Category.IsDeleted &&
                !w.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<string> CodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                EF.Functions.IsNumeric(w.BranchCode))
            .Select(w => Convert.ToInt64(w.BranchCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<Branch?> HaveBranchChild(
        long branchId, CT ct)
    {
        var query = DbSet

             .Where(w =>
                w.Id.Equals(branchId) &&
                w.Seasons.Any() &&
                !w.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<Branch> Data, int RowCount)> GetsBranchByCodes(
        List<string> codes,
        long categoryId,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                w.CategoryId == categoryId &&
                         codes.Contains(w.BranchCode));

        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<Branch>> GetsBranchByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet.Include(x => x.Category)
            .Where(w =>
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<Branch> Data, int RowCount)> GetsBranchs(
        List<long>? ids,
        string? filterData,
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

            .Include(i => i.Category)
            .Include(i => i.Seasons)
            .Where(w =>
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                (code == null || w.BranchCode.Contains(code)) &&
                (name == null || w.BranchName.Contains(name)) &&
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchName, filterData.MakeLikePattern())) &&
                (categoryId == null || w.Category.Id.Equals(categoryId)) &&
                !w.Category.IsDeleted &&
                !w.IsDeleted);
        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);
        query = query
            .OrderBy(oo => !oo.IsActive);
        var allItems = await query.ToListAsync(ct);
        var numericItems = allItems
            .Where(x => Regex.IsMatch(x.BranchCode, @"^d+$"))
            .OrderBy(x => int.Parse(x.BranchCode));
        var nonNumericItems = allItems
            .Where(x => !Regex.IsMatch(x.BranchCode, @"^d+$"))
            .OrderBy(x => x.BranchCode);
        var combined = numericItems.Concat(nonNumericItems);
        if (pageIndex > 0 && pageSize > 0)
            combined = combined.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (combined.ToList(), allItems.Count);
    }

    public async Task<(List<GetsBranchsResponseModel> Data, int RowCount)> GetsBranchsResponse(
        string? filterData,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Include(i => i.Category)
            .Include(i => i.Seasons)
            .Where(w =>
                (code == null || w.BranchCode.Contains(code)) &&
                (name == null || w.BranchName.Contains(name)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchName, filterData.MakeLikePattern())) &&
                (categoryId == null || w.Category.Id.Equals(categoryId)) &&
                !w.Category.IsDeleted &&
                !w.IsDeleted);
        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);
        query = query
            .OrderBy(oo => !oo.IsActive);
        var allItems = await query.ToListAsync(ct);
        var numericItems = allItems
            .Where(x => Regex.IsMatch(x.BranchCode, @"^d+$"))
            .OrderBy(x => int.Parse(x.BranchCode));
        var nonNumericItems = allItems
            .Where(x => !Regex.IsMatch(x.BranchCode, @"^d+$"))
            .OrderBy(x => x.BranchCode);
        var combined = numericItems.Concat(nonNumericItems);
        if (pageIndex > 0 && pageSize > 0)
            combined = combined.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (combined
            .Select(e => new GetsBranchsResponseModel
            {
                Id = e.Id,
                BranchName = e.BranchName,
                BranchCode = e.BranchCode,
                AlternativeId = $"Branch{e.Id}",
                CategoryId = e.CategoryId,
                CategoryName = e.Category.CategoryName,
                CategoryCode = e.Category.CategoryCode,
                IsActive = e.IsActive,
                HaveChild = e.Seasons.Any(e => !e.IsDeleted),
                ChildCount = e.Seasons.Count(e => !e.IsDeleted),
                Category = new BranchCategoryModel
                {
                    Id = e.CategoryId,
                    CategoryName = e.Category.CategoryName,
                    CategoryCode = e.Category.CategoryCode,
                    IsActive = e.Category.IsActive
                }
            })
            .ToList(), allItems.Count);
    }

    public async Task<(List<GetsActiveBranchsResponseModel> Data, int RowCount)> GetsActiveBranchs(
        string? filterData,
        long? categoryId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (code == null || w.BranchCode.Contains(code)) &&
                (companyId == null || w.CompanyId.Equals(companyId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchName, filterData.MakeLikePattern())) &&
                (name == null || w.BranchName.Contains(name)) &&
                (categoryId == null || w.Category.Id.Equals(categoryId)) &&
                w.IsActive &&
                !w.Category.IsDeleted &&
                !w.IsDeleted &&
                w.Category.IsActive)
            .Select(s => new GetsActiveBranchsResponseModel
            {
                Id = s.Id,
                BranchName = s.BranchName,
                BranchCode = s.BranchCode
            });

        query = query.OrderBy(o => o.BranchName);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Branch> Data, int RowCount)> GetsBranchByCategoryId(
        long categoryId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Include(i => i.Category)
            .Include(i => i.Seasons)
            .Where(w =>
                w.Category.Id == categoryId &&
                !w.Category.IsDeleted &&
                !w.IsDeleted);


        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsBranchByCategoryIdModel> Data, int RowCount)> GetsBranchByCategoryIdModel(
        long categoryId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

        .Include(i => i.Category)
        .Include(i => i.Seasons)
        .Where(w =>
            w.Category.Id == categoryId &&
            !w.Category.IsDeleted &&
            !w.IsDeleted);


        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.Select(e => new GetsBranchByCategoryIdModel
        {
            Id = e.Id,
            BranchName = e.BranchName,
            BranchCode = e.BranchCode,
            AlternativeId = $"Branch{e.Id}",
            IsActive = e.IsActive,
            Category = new BranchCategoryModel
            {
                Id = e.Id,
                CategoryName = e.Category.CategoryName,
                CategoryCode = e.Category.CategoryCode,
                IsActive = e.Category.IsActive
            },
            CategoryCode = e.Category.CategoryCode,
            CategoryName = e.Category.CategoryName,
            CategoryId = e.CategoryId,
            HaveChild = e.Seasons.Any(e => !e.IsDeleted),
            ChildCount = e.Seasons.Count(e => !e.IsDeleted),
            CompanyId = e.CompanyId,
        }).ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsBranchByCategoryIdsModel> Data, int RowCount)> GetsBranchByCategoryIds(
        List<long> categoryIds,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

                .Where(w =>
                    categoryIds.Contains(w.Category.Id) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchCode, filterData.MakeLikePattern()) ||
                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.BranchName, filterData.MakeLikePattern())))
                .Select(s => new GetsBranchByCategoryIdsModel
                {
                    Id = s.Id,
                    CategoryId = s.Category.Id,
                    BranchName = s.BranchName,
                    BranchCode = s.BranchCode,
                    AlternativeId = s.Id.ToString(),
                    IsActive = s.IsActive
                });

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);

        query = query.OrderBy(o => !o.IsActive);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task AddRangeAsync(IEnumerable<Branch> branches, CT ct)
    {
        await DbSet.AddRangeAsync(branches, ct);
    }

    public async Task<List<Branch>> GetForRasteReshteImport(List<long> categoryIds, List<string> branchCodes, long companyId, CT ct)
    {
        return await DbSet
            .Where(x =>
                x.CompanyId == companyId &&
                !x.IsDeleted &&
                categoryIds.Contains(x.CategoryId) &&
                branchCodes.Contains(x.BranchCode))
            .ToListAsync(ct);
    }

    public async Task<List<Branch>> GetForAdjustmentImport(List<string> categoryCodes, List<string> branchCodes, long companyId, CT ct)
    {
        return await DbSet
            .Include(x => x.Category)
            .Where(x =>
                x.CompanyId == companyId &&
                categoryCodes.Contains(x.Category.CategoryCode) &&
                branchCodes.Contains(x.BranchCode))
            .ToListAsync(ct);
    }


}