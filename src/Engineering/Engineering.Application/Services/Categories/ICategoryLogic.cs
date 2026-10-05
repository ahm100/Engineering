using Engineering.Application.Services.Categories.Models.ActiveCategory;
using Engineering.Application.Services.Categories.Models.CategoryExcelImports;
using Engineering.Application.Services.Categories.Models.CategoryGroupDelete;
using Engineering.Application.Services.Categories.Models.CodeCreator;
using Engineering.Application.Services.Categories.Models.CreateCategory;
using Engineering.Application.Services.Categories.Models.DisableCategory;
using Engineering.Application.Services.Categories.Models.GetCategoryByCode;
using Engineering.Application.Services.Categories.Models.GetCategoryById;
using Engineering.Application.Services.Categories.Models.GetCategoryByName;
using Engineering.Application.Services.Categories.Models.GetsActiveCategories;
using Engineering.Application.Services.Categories.Models.GetsByFilterData;
using Engineering.Application.Services.Categories.Models.GetsCategories;
using Engineering.Application.Services.Categories.Models.GetsCategoryExcelEnum;
using Engineering.Application.Services.Categories.Models.GetsCategoryExcelExporter;
using Engineering.Application.Services.Categories.Models.InactiveCategory;
using Engineering.Application.Services.Categories.Models.StateChangerCategories;
using Engineering.Application.Services.Categories.Models.UpdateCategory;

namespace Engineering.Application.Services.Categories;

public interface ICategoryLogic
{
    ///Commands
    Task<Result<ActiveCategoryResponse?>> ActiveCategory(
        ActiveCategoryRequest request, CT ct);

    Task<Result<UpdateCategoryResponse?>> UpdateCategory(
        UpdateCategoryRequest request, CT ct);

    Task<Result<CreateCategoryResponse?>> CreateCategory(
        CreateCategoryRequest request, CT ct);

    Task<Result<DisableCategoryResponse?>> DisableCategory(
        DisableCategoryRequest request, CT ct);

    Task<Result<InactiveCategoryResponse?>> InactiveCategory(
        InactiveCategoryRequest request, CT ct);

    Task<Result<CategoryCodeCreatorResponse?>> CodeCreator(
        CategoryCodeCreatorRequest request, CT ct);

    Task<Result<CategoryGroupDeleteResponse?>> CategoryGroupDelete(
        CategoryGroupDeleteRequest request, CT ct);

    Task<Result<CategoryExcelImportsResponse?>> CategoryExcelImports(
        CategoryExcelImportsRequest request, CT ct);

    Task<Result<StateChangerCategoriesResponse?>> StateChangerCategories(
        StateChangerCategoriesRequest request, CT ct);

    ///Queries
    Task<Result<GetsCategoriesResponse?>> GetsCategories(
        GetsCategoriesRequest request, CT ct);

    Task<Result<GetCategoryByIdResponse?>> GetCategoryById(
        GetCategoryByIdRequest request, CT ct);

    Task<Result<GetsByFilterDataResponse?>> GetsByFilterData(
        GetsByFilterDataRequest request, CT ct);

    Task<Result<GetsByFilterDataResponse?>> NewGetsByFilterData(
        GetsCategoriesRequest request, CT ct);

    Task<Result<GetCategoryByNameResponse?>> GetCategoryByName(
        GetCategoryByNameRequest request, CT ct);

    Task<Result<GetCategoryByCodeResponse?>> GetCategoryByCode(
        GetCategoryByCodeRequest request, CT ct);

    Task<Result<GetsActiveCategoriesResponse?>> GetsActiveCategories(
        GetsActiveCategoriesRequest request, CT ct);

    Task<Result<GetsCategoryExcelEnumResponse?>> GetsCategoryExcelEnum(
        GetsCategoryExcelEnumRequest request, CT ct);

    Task<Result<GetsCategoryExcelExporterResponse?>> GetsCategoryExcelExporter(
        GetsCategoryExcelExporterRequest request, CT ct);
}