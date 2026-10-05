using Engineering.Application.Services.Categories.Models.GetCategoryByCode;
using Engineering.Application.Services.Categories.Models.GetCategoryById;
using Engineering.Application.Services.Categories.Models.GetsActiveCategories;
using Engineering.Application.Services.Categories.Models.GetsByFilterData;
using Engineering.Application.Services.Categories.Models.GetsCategories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Abstractions.Data.Categories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category?> GetCategoryByName(
        string name,
        CT ct);

    Task<Category?> IsDuplicateCategoryByName(
        string name,
        long? companyId,
        CT ct);

    Task<Category?> IsDuplicateCategoryByCode(
        string name,
        long? companyId,
        CT ct);

    Task<bool> GetCategoryByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct);

    Task<Category?> GetCategoryByCode(
        string code,
        CT ct);

    Task<GetCategoryByCodeResponse?> GetCategoryByCodeForResponse(
        string categoryCode, CT ct);

    Task<Category?> HaveCategoryChild(
        long id, CT ct);

    Task<Category?> GetCategoryById(
        long id,
        CT ct);

    Task<GetCategoryByIdResponse?> GetCategoryByIdForResponse(
        long id,
        CT ct);

    Task<Category?> GetCategoryWithoutIncludeById(
        long id, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<List<Category>> GetsCategoryByIds(
        List<long> ids, CT ct);

    Task<(List<Category> Data, int RowCount)> GetsCategoryByCodes(
        List<string> codes,
        long? companyId, CT ct);

    Task<(List<Category> Data, int RowCount)> GetsCategories(
        List<long>? ids,
        string? filterData,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);



    Task<(List<Category> Data, int RowCount)> GetsByFilterData(
        string? filterData,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsByFilterDataResponseModel> Data, int RowCount)> GetsByFilterDataForResponse(
        string? filterData,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsActiveCategoriesResponseModel> Data, int RowCount)> GetsActiveCategoriesForResponse(
        string? filterData,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Category> Data, int RowCount)> GetsActiveCategories(
        string? filterData,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsCategoriesResponseModel> Data, int RowCount)> GetsCategoriesForResponse(
            List<long>? ids,
            string? filterData,
            string? code,
            string? name,
            bool? isActive,
            string[]? orderBy,
            long? companyId,
            int pageIndex,
            int pageSize, CT ct);

    Task<List<Category>?> GetCategoryByNames(
        List<string> names, CT ct);

    Task AddRangeAsync(IEnumerable<Category> categories, CT ct);
    Task<List<Category>> GetForRasteReshteImport(List<string> categoryCodes, long companyId, CT ct);

}