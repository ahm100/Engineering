using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.CategoryModels;
using Engineering.Application.Services.Categories.Models.GetCategoryByCode;
using Engineering.Application.Services.Categories.Models.GetCategoryById;
using Engineering.Application.Services.Categories.Models.GetsActiveCategories;
using Engineering.Application.Services.Categories.Models.GetsByFilterData;
using Engineering.Application.Services.Categories.Models.GetsCategories;
using System.Text.RegularExpressions;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Persistence.Repositories.Categories;

public class CategoryRepository : BaseRepository<EngineeringDBContext, Category>, ICategoryRepository
{
    public CategoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public Task<bool> GetCategoryByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .AnyAsync(a =>
                (companyId == null || a.CompanyId == (companyId)) &&
                names.Contains(a.CategoryName) || codes.Contains(a.CategoryCode), ct);

        return query;
    }

    public async Task<Category?> GetCategoryByName(
        string categoryName, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.CategoryName == categoryName);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Category?> IsDuplicateCategoryByName(
        string categoryName,
        long? companyId,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                w.CategoryName == categoryName);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Category?> IsDuplicateCategoryByCode(
        string categoryCode,
        long? companyId,
        CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                w.CategoryCode == categoryCode);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Category?> GetCategoryByCode(
        string categoryCode, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.CategoryCode == categoryCode);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetCategoryByCodeResponse?> GetCategoryByCodeForResponse(
        string categoryCode, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.CategoryCode == categoryCode)
            .Select(e => new GetCategoryByCodeResponse
            {
                Id = e.Id,
                CategoryName = e.CategoryName,
                CategoryCode = e.CategoryCode,
                AlternativeId = $"Category{e.Id}",
                IsActive = e.IsActive
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<string> CodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                EF.Functions.IsNumeric(w.CategoryCode))
            .Select(s => Convert.ToInt64(s.CategoryCode))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<Category?> HaveCategoryChild(
        long categoryId, CT ct)
    {
        var query = DbSet

            .Include(i => i.Branchs)
            .Where(w =>
                w.Id == categoryId &&
                w.Branchs.Any());

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Category?> GetCategoryById(
        long categoryId, CT ct)
    {
        var query = DbSet

            .Include(i => i.Branchs)
            .Where(w =>
                !w.IsDeleted &&
                w.Id == categoryId);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetCategoryByIdResponse?> GetCategoryByIdForResponse(
        long id, CT ct)
    {
        var query = DbSet
            .Where(w => w.Id == id && !w.IsDeleted)
            .Include(w => w.Branchs)
            .Select(s => new GetCategoryByIdResponse
            {
                Id = s.Id,
                CategoryCode = s.CategoryCode,
                CategoryName = s.CategoryName,
                IsActive = s.IsActive,
                HaveChild = s.Branchs.Any(w => !w.IsDeleted),
                AlternativeId = $"Category{s.Id}",
                PreferentialReferenceCode = s.PreferentialReferenceCode,
                ChildCount = s.Branchs.Count(w => !w.IsDeleted)
            });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<Category?> GetCategoryWithoutIncludeById(
        long categoryId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.Id == categoryId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<Category> Data, int RowCount)> GetsCategoryByCodes(
        List<string> codes,
        long? companyId, CT ct)
    {
        var query = DbSet

            .Where(w =>
                (companyId == null || w.CompanyId == companyId) &&
                codes.Contains(w.CategoryCode));

        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<Category>> GetsCategoryByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet

            .Where(w =>
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<Category> Data, int RowCount)> GetsCategories(
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

            .Include(i => i.Branchs)
            .ThenInclude(t => t.Seasons)
            .Where(w =>
                (companyId == null || w.CompanyId == (companyId)) &&
                (code == null || w.CategoryCode.Contains(code)) &&
                (name == null || w.CategoryName.Contains(name)) &&
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);
        var allItems = await query.ToListAsync(ct);
        var numericItems = allItems
            .Where(x => Regex.IsMatch(x.CategoryCode, @"^d+$"))
            .OrderBy(x => int.Parse(x.CategoryCode));
        var nonNumericItems = allItems
            .Where(x => !Regex.IsMatch(x.CategoryCode, @"^d+$"))
            .OrderBy(x => x.CategoryCode);
        var combined = numericItems.Concat(nonNumericItems);
        if (pageIndex > 0 && pageSize > 0)
            combined = combined.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (combined.ToList(), allItems.Count);
    }

    public async Task<(List<GetsCategoriesResponseModel> Data, int RowCount)> GetsCategoriesForResponse(
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

            .Include(i => i.Branchs)
            .ThenInclude(t => t.Seasons)
            .Where(w =>
                (companyId == null || w.CompanyId == (companyId)) &&
                (code == null || w.CategoryCode.Contains(code)) &&
                (name == null || w.CategoryName.Contains(name)) &&
                (ids == null || ids.Count == 0 || ids.Contains(w.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(w => w.IsActive == isActive);
        var allItems = await query.ToListAsync(ct);
        var numericItems = allItems
            .Where(x => Regex.IsMatch(x.CategoryCode, @"^d+$"))
            .OrderBy(x => int.Parse(x.CategoryCode));
        var nonNumericItems = allItems
            .Where(x => !Regex.IsMatch(x.CategoryCode, @"^d+$"))
            .OrderBy(x => x.CategoryCode);
        var combined = numericItems.Concat(nonNumericItems);
        if (pageIndex > 0 && pageSize > 0)
            combined = combined.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (combined
            .Select(e => new GetsCategoriesResponseModel
            {
                Id = e.Id,
                CategoryName = e.CategoryName,
                CategoryCode = e.CategoryCode,
                IsActive = e.IsActive,
                ChildCount = e.Branchs.Count(e => !e.IsDeleted),
                HaveChild = e.Branchs.Any(e => !e.IsDeleted)
            })
            .ToList(), allItems.Count);
    }

    public async Task<(List<Category> Data, int RowCount)> GetsByFilterData(
        string? filterData,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Include(i => i.Branchs)
            .ThenInclude(t => t.Seasons)
            .Where(w =>
                (companyId == null || w.CompanyId == (companyId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryName, filterData.MakeLikePattern())) ||
                w.Branchs.Any(b =>
                    !b.IsDeleted &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(b.BranchName, filterData.MakeLikePattern()) ||
                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(b.BranchCode, filterData.MakeLikePattern()) ||
                     b.Seasons.Any(s =>
                         !s.IsDeleted &&
                         (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(s.SeasonCode, filterData.MakeLikePattern()) ||
                          string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(s.SeasonName, filterData.MakeLikePattern()))))));
        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsByFilterDataResponseModel> Data, int RowCount)> GetsByFilterDataForResponse(
        string? filterData,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Include(i => i.Branchs)
            .ThenInclude(t => t.Seasons)
            .Where(w =>
                (companyId == null || w.CompanyId == (companyId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryName, filterData.MakeLikePattern())) ||
                w.Branchs.Any(b =>
                    !b.IsDeleted &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(b.BranchName, filterData.MakeLikePattern()) ||
                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(b.BranchCode, filterData.MakeLikePattern()) ||
                     b.Seasons.Any(s =>
                         !s.IsDeleted &&
                         (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(s.SeasonCode, filterData.MakeLikePattern()) ||
                          string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(s.SeasonName, filterData.MakeLikePattern()))))));
        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .Select(e => new GetsByFilterDataResponseModel
            {
                Id = e.Id,
                AlternativeId = $"Category{e.Id}",
                CategoryCode = e.CategoryCode,
                CategoryName = e.CategoryName,
                IsActive = e.IsActive,
                Branchs = e.Branchs.Where(b => !b.IsDeleted).Select(b => new BranchModel(
                    b.Id,
                    $"Branch{b.Id}",
                    b.BranchName,
                    b.BranchCode,
                    b.IsActive,
                    b.Seasons.Where(s => !s.IsDeleted).Select(s => new SeasonModel(
                        s.Id,
                        $"Season{s.Id}",
                        s.SeasonName,
                        s.SeasonCode,
                        s.IsActive)).ToList()
                )).ToList()
            })
            .ToListAsync(ct);
        return (items, count);
    }


    public async Task<(List<Category> Data, int RowCount)> GetsActiveCategories(
        string? filterData,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.IsActive &&
                (companyId == null || w.CompanyId == (companyId)) &&
                (code == null || w.CategoryCode.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryName, filterData.MakeLikePattern())) &&
                (name == null || w.CategoryName.Contains(name)));
        query = query.Include(i => i.Branchs);
        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsActiveCategoriesResponseModel> Data, int RowCount)> GetsActiveCategoriesForResponse(
    string? filterData,
    string? code,
    string? name,
    long? companyId,
    int pageIndex,
    int pageSize, CT ct)
    {
        var query = DbSet

            .Where(w =>
                w.IsActive &&
                (companyId == null || w.CompanyId == (companyId)) &&
                (code == null || w.CategoryCode.Contains(code)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryCode, filterData.MakeLikePattern()) ||
                 string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(w.CategoryName, filterData.MakeLikePattern())) &&
                (name == null || w.CategoryName.Contains(name)));
        query = query.Include(i => i.Branchs);
        query = query.OrderBy(o => !o.IsActive)
                     .ThenByDescending(t => t.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
        .Select(e => new GetsActiveCategoriesResponseModel
        {
            Id = e.Id,
            CategoryName = e.CategoryName,
            CategoryCode = e.CategoryCode
        })
        .ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<Category>?> GetCategoryByNames(
        List<string> names, CT ct)
    {
        var query = DbSet

            .Include(i => i.Branchs)
                .ThenInclude(i => i.Seasons)
            .Where(w =>
                !w.IsDeleted && names.Contains(w.CategoryName));

        var item = await query.ToListAsync(ct);
        return item;
    }

    public async Task AddRangeAsync(IEnumerable<Category> categories, CT ct)
    {
        await DbSet.AddRangeAsync(categories, ct);
    }

    public async Task<List<Category>> GetForRasteReshteImport(List<string> categoryCodes, long companyId, CT ct)
    {
        return await DbSet
            .Where(x =>
                x.CompanyId == companyId &&
                !x.IsDeleted &&
                categoryCodes.Contains(x.CategoryCode))
            .ToListAsync(ct);
    }


}