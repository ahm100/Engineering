using Engineering.Application.Extensions.ServiceModeling;
using Engineering.Application.Services.Branchs.Queries.GetsBranchs;
using Engineering.Application.Services.CabinTypes.Queries.GetCategoryByCodeForResponse;
using Engineering.Application.Services.CabinTypes.Queries.GetsCategoriesForResponse;
using Engineering.Application.Services.Categories.Commands.ActiveCategory;
using Engineering.Application.Services.Categories.Commands.CodeCreator;
using Engineering.Application.Services.Categories.Commands.CreateCategory;
using Engineering.Application.Services.Categories.Commands.DisableCategory;
using Engineering.Application.Services.Categories.Commands.InactiveCategory;
using Engineering.Application.Services.Categories.Commands.StateChangerCategories;
using Engineering.Application.Services.Categories.Commands.UpdateCategory;
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
using Engineering.Application.Services.Categories.Queries.GetCategoryById;
using Engineering.Application.Services.Categories.Queries.GetCategoryByIdForResponse;
using Engineering.Application.Services.Categories.Queries.GetCategoryByName;
using Engineering.Application.Services.Categories.Queries.GetCategoryByNamesOrCodes;
using Engineering.Application.Services.Categories.Queries.GetsActiveCategories;
using Engineering.Application.Services.Categories.Queries.GetsByFilterData;
using Engineering.Application.Services.Categories.Queries.GetsCategories;
using Engineering.Application.Services.Categories.Queries.GetsCategoryByIds;
using Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByCode;
using Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByName;
using Engineering.Application.Services.Seasons.Queries.GetSeasons;
using Engineering.Domain.Constants;
using Engineering.Domain.Entities.Categories;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Commands.UpdatePreferentialNameFaAndStatus;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.PreferentialTemporary.Commands.CreatePreferentialTemporary;

namespace Engineering.Application.Services.Categories;

public class CategoryLogic : ICategoryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CategoryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public CategoryLogic(
        IMediator mediator,
        ILogger<CategoryLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateCategoryResponse?>> CreateCategory(
        CreateCategoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for new Category, CategoryName:{CategoryName}, CategoryCode:{CategoryCode},", request.CategoryName, request.CategoryCode);

        var isValidRequest = await request.IsValidAsync<CreateCategoryValidator, CreateCategoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCategoryResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateCategoryResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(
                new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateCategoryResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new IsDuplicateCategoryByNameQuery(request.CategoryName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCategoryResponse>(CategoryErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new IsDuplicateCategoryByCodeQuery(request.CategoryCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCategoryResponse>(CategoryErrors.CodeIsDuplicate);

        var responseCreate = await _mediator.Send(
            new CreateCategoryCommand(
                request.CategoryName,
                request.CategoryCode,
                request.IsActive,
                companyId), ct);
        if (responseCreate.IsFailure)
            return Result.Failure<CreateCategoryResponse>(responseCreate.Error!);

        await _unitOfWork.CommitAsync(ct);

        try
        {
            var Category = await _mediator.Send(new GetCategoryByIdQuery(responseCreate.Value!.Id), ct);

            var createPreferentialTemporary = new CreatePreferentialTemporaryCommand(
                Category.Value!.GetPreferentialName(),
                DepartmentNames.Engineering.Category,
                Category.Value!.PreferentialReferenceCode);

            var createPreferentialTemporaryResponse = await _mediator.Send(createPreferentialTemporary, ct);
            if (createPreferentialTemporaryResponse.IsFailure)
                _logger.LogError("CreatePreferentialTemporary for Category with id of {CategoryId} failed with Code:{Code}, Msg:{Msg}", responseCreate.Value!.Id, createPreferentialTemporaryResponse.Error!.Code, createPreferentialTemporaryResponse.Error!.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return responseCreate.Value;
    }

    public async Task<Result<StateChangerCategoriesResponse?>> StateChangerCategories(
        StateChangerCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerCategories, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerCategoriesValidator, StateChangerCategoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerCategoriesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerCategoriesResponse>(GlobalErrors.IdsNotEqual);

        var responseGet = await _mediator.Send(new GetsCategoryByIdsQuery(request.Ids), ct);
        if (responseGet.IsFailure || responseGet.Value is null || responseGet.Value.Count <= 0)
            return Result.Failure<StateChangerCategoriesResponse>(responseGet.Error!);

        var responseChanger = await _mediator.Send(new StateChangerCategoriesCommand(
            responseGet.Value,
            request.State), ct);
        if (responseChanger.IsFailure)
            return Result.Failure<StateChangerCategoriesResponse>(responseChanger.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerCategoriesResponse(true);
    }

    public async Task<Result<CategoryExcelImportsResponse?>> CategoryExcelImports(
        CategoryExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for CategoryExcelImports");

        var isValidRequest = await request.IsValidAsync<CategoryExcelImportsValidator, CategoryExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CategoryExcelImportsResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CategoryExcelImportsResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var getCompanyResponse = await _mediator.Send(
                new GetCompanyByIdQuery((long)companyId), ct);
            if (getCompanyResponse.IsFailure)
                return Result.Failure<CategoryExcelImportsResponse>(getCompanyResponse.Error!);
        }

        var categories = ExcelImporter.Import<CategoryExcelImportsModel>(request.DocumentFile);
        if (categories is null)
            return Result.Failure<CategoryExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (categories.Count != categories.Select(s => s.CategoryName).Distinct().Count())
            return Result.Failure<CategoryExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (categories.Count != categories.Select(s => s.CategoryCode).Distinct().Count())
            return Result.Failure<CategoryExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        var names = categories.Select(s => s.CategoryName).ToList();
        var codes = categories.Select(s => s.CategoryCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new GetCategoryByNamesOrCodesQuery(
            names,
            codes,
            companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<CategoryExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in categories)
        {
            var responseImporter = await _mediator.Send(new CreateCategoryCommand(
                item.CategoryCode,
                item.CategoryName,
                item.IsActive,
                companyId), ct);
            if (responseImporter.IsFailure)
                return Result.Failure<CategoryExcelImportsResponse>(responseImporter.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CategoryExcelImportsResponse(true);
    }

    public async Task<Result<CategoryCodeCreatorResponse?>> CodeCreator(
        CategoryCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CategoryCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CategoryCodeCreatorResponse>(companyResponse.Error!);
        }

        var responseCreator = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (responseCreator.IsFailure)
            return Result.Failure<CategoryCodeCreatorResponse>(responseCreator.Error!);

        return new CategoryCodeCreatorResponse(responseCreator.Value!);
    }

    public async Task<Result<UpdateCategoryResponse?>> UpdateCategory(
        UpdateCategoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateCategory, Id:{Id}, CategoryName:{CategoryName}, CategoryCode:{CategoryCode}, IsActive:{IsActive}", request.Id, request.CategoryName, request.IsActive, request.CategoryCode);

        var isValidRequest = await request.IsValidAsync<UpdateCategoryValidator, UpdateCategoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCategoryResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateCategoryResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var getCompanyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (getCompanyResponse.IsFailure)
                return Result.Failure<UpdateCategoryResponse>(getCompanyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new IsDuplicateCategoryByNameQuery(request.CategoryName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCategoryResponse>(CategoryErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new IsDuplicateCategoryByCodeQuery(request.CategoryCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCategoryResponse>(CategoryErrors.CodeIsDuplicate);

        var responseUpdate = await _mediator.Send(new UpdateCategoryCommand(
            request.Id,
            request.CategoryName,
            request.CategoryCode,
            request.IsActive,
            companyId), ct);
        if (responseUpdate.IsFailure)
            return Result.Failure<UpdateCategoryResponse>(responseUpdate.Error!);

        var Category = await _mediator.Send(new GetCategoryByIdQuery(request.Id), ct);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(Category.Value!, ct);
        return responseUpdate.Value!;
    }

    public async Task<Result<InactiveCategoryResponse?>> InactiveCategory(
        InactiveCategoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveCategory, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveCategoryValidator, InactiveCategoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveCategoryResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCategoryByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<InactiveCategoryResponse>(responseGet.Error!);

        var responseInactive = await _mediator.Send(new InactiveCategoryCommand(responseGet.Value!), ct);
        if (responseInactive.IsFailure)
            return Result.Failure<InactiveCategoryResponse>(responseInactive.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(responseInactive.Value!, ct);
        return new InactiveCategoryResponse(true);
    }

    public async Task<Result<ActiveCategoryResponse?>> ActiveCategory(
        ActiveCategoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveCategory, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveCategoryValidator, ActiveCategoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveCategoryResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCategoryByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<ActiveCategoryResponse>(responseGet.Error!);

        var responseActive = await _mediator.Send(new ActiveCategoryCommand(responseGet.Value!), ct);
        if (responseActive.IsFailure)
            return Result.Failure<ActiveCategoryResponse>(responseActive.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(responseActive.Value!, ct);
        return new ActiveCategoryResponse(true);
    }

    public async Task<Result<DisableCategoryResponse?>> DisableCategory(
        DisableCategoryRequest request, CT ct)
    {
        _logger.LogInformation("Disable Category, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableCategoryValidator, DisableCategoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableCategoryResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCategoryByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<DisableCategoryResponse>(responseGet.Error!);

        var responseDisable = await _mediator.Send(new DisableCategoryCommand(responseGet.Value!), ct);
        if (responseDisable.IsFailure)
            return Result.Failure<DisableCategoryResponse>(responseDisable.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(responseDisable.Value!, ct);
        return new DisableCategoryResponse(true);
    }

    public async Task<Result<CategoryGroupDeleteResponse?>> CategoryGroupDelete(
        CategoryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for CategoryGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<CategoryGroupDeleteValidator, CategoryGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CategoryGroupDeleteResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetsCategoryByIdsQuery(request.Ids), ct);
        if (responseGet.IsFailure)
            return Result.Failure<CategoryGroupDeleteResponse>(responseGet.Error!);

        foreach (var values in responseGet.Value!)
        {
            var responseDisable = await _mediator.Send(new DisableCategoryCommand(values), ct);
            if (responseDisable.IsFailure)
                return Result.Failure<CategoryGroupDeleteResponse>(responseDisable.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CategoryGroupDeleteResponse(true);
    }

    public async Task<Result<GetCategoryByIdResponse?>> GetCategoryById(
        GetCategoryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCategoryById, id:{Id}", request.Id);

        var isValidRequest = await
            request.IsValidAsync<GetCategoryByIdValidator, GetCategoryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCategoryByIdResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(
            new GetCategoryByIdForResponseQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<GetCategoryByIdResponse>(responseGet.Error!);

        return responseGet.Value!;
    }

    public async Task<Result<GetCategoryByNameResponse?>> GetCategoryByName(
        GetCategoryByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCategoryByName, categoryName:{CategoryName}", request.CategoryName);

        var isValidRequest = await request.IsValidAsync<GetCategoryByNameValidator, GetCategoryByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCategoryByNameResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCategoryByNameQuery(request.CategoryName), ct);
        if (responseGet.IsFailure)
            return Result.Failure<GetCategoryByNameResponse>(responseGet.Error!);

        return responseGet.Value!;
    }

    public async Task<Result<GetCategoryByCodeResponse?>> GetCategoryByCode(
        GetCategoryByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCategoryByCode, categoryCode:{CategoryCode}", request.CategoryCode);

        var isValidRequest = await request.IsValidAsync<GetCategoryByCodeValidator, GetCategoryByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCategoryByCodeResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCategoryByCodeForResponseQuery(request.CategoryCode), ct);
        if (responseGet.IsFailure)
            return Result.Failure<GetCategoryByCodeResponse>(responseGet.Error!);

        return responseGet.Value!;
    }

    public async Task<Result<GetsActiveCategoriesResponse?>> GetsActiveCategories(
        GetsActiveCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsActiveCategories,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.Code,
            request.Name,
            request.PageIndex,
            request.PageSize);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsActiveCategoriesResponse>(GlobalErrors.InvalidCompany);

        var isValidRequest = await request.IsValidAsync<GetsActiveCategoriesValidator, GetsActiveCategoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveCategoriesResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetsActiveCategoriesQuery(
            request.FilterData,
            request.Code,
            request.Name,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (responseGet.IsFailure)
            return Result.Failure<GetsActiveCategoriesResponse>(responseGet.Error!);

        var value = responseGet.Value!;
        return new GetsActiveCategoriesResponse(value.Data!, value.RowCount);
    }

    public async Task<Result<GetsCategoriesResponse?>> GetsCategories(
        GetsCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCategories, Code:{Code} , Name:{Name}, IsActive:{IsActive} , pageSize:{PageSize}, pageSize:{PageSize}", request.Code, request.Name, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await
            request.IsValidAsync<GetsCategoriesValidator, GetsCategoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCategoriesResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCategoriesResponse>(GlobalErrors.InvalidCompany);

        var responseGet = await _mediator.Send(
            new GetsCategoriesForResponseQuery(
                null,
                request.FilterData,
                request.Code,
                request.Name,
                request.IsActive,
                request.OrderBy,
                companyId,
                request.PageIndex,
                request.PageSize), ct);
        if (responseGet.IsFailure)
            return Result.Failure<GetsCategoriesResponse>(responseGet.Error!);

        return new GetsCategoriesResponse(responseGet.Value!.Data!, responseGet.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByFilterDataResponse?>> NewGetsByFilterData(
        GetsCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByFilterData, FilterData:{FilterData} , pageSize:{PageSize}, pageSize:{PageSize}", request.FilterData, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCategoriesValidator, GetsCategoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByFilterDataResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsByFilterDataResponse>(GlobalErrors.InvalidCompany);

        var categoryResponse = await _mediator.Send(
            new GetsCategoriesQuery(
                null,
                request.FilterData!, null, null, null, null,
                companyId,
                1, 100), ct);
        var categoryData = categoryResponse.Value?.Data;

        var branchResponse = await _mediator.Send(
            new GetsBranchsQuery(
                null,
                request.FilterData, null, null, null, null, null,
                companyId,
                1, 100), ct);
        var branchData = branchResponse.Value?.Data;

        var seasonResponse = await _mediator.Send(
            new GetSeasonsQuery(
                null,
                request.FilterData, null, null, null, null, null, null,
                companyId,
                1, 100), ct);
        var seasonData = seasonResponse.Value?.Data;

        if (categoryResponse.IsFailure && branchResponse.IsFailure && seasonResponse.IsFailure)
            return Result.Failure<GetsByFilterDataResponse>(categoryResponse.Error!);

        var companyIds = categoryData?.Where(w =>
                                           w.CompanyId != null &&
                                           w.CompanyId > 0)
                                      .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = DataModeling.GetsCategoryTree(categoryData, branchData, seasonData, companies);
        return new GetsByFilterDataResponse(data!, data!.Count);
    }

    public async Task<Result<GetsByFilterDataResponse?>> GetsByFilterData(
        GetsByFilterDataRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByFilterData, FilterData:{FilterData} , pageSize:{PageSize}, pageSize:{PageSize}", request.FilterData, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByFilterDataValidator, GetsByFilterDataRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByFilterDataResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsByFilterDataResponse>(GlobalErrors.InvalidCompany);

        if (!string.IsNullOrEmpty(request.FilterData))
        {
            var responseGet = await _mediator.Send(new GetsByFilterDataQuery(
                request.FilterData!,
                companyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize), ct);
            if (responseGet.IsFailure)
                return Result.Failure<GetsByFilterDataResponse>(responseGet.Error!);

            var data = responseGet.Value!.Data.Adapt<List<GetsByFilterDataResponseModel>>();
            return new GetsByFilterDataResponse(data, responseGet.Value?.RowCount ?? 0);
        }
        else
        {
            var responseGet = await _mediator.Send(new GetsCategoriesQuery(
                null,
                request.CategoryFilterData,
                null, null, null,
                request.OrderBy,
                companyId,
                request.PageIndex,
                request.PageSize), ct);
            if (responseGet.IsFailure)
                return Result.Failure<GetsByFilterDataResponse>(responseGet.Error!);

            var data = responseGet.Value!.Data.Adapt<List<GetsByFilterDataResponseModel>>();
            return new GetsByFilterDataResponse(data, responseGet.Value?.RowCount ?? 0);
        }
    }

    public async Task<Result<GetsCategoryExcelExporterResponse?>> GetsCategoryExcelExporter(
        GetsCategoryExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCategoryExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCategoryExcelExporterValidator, GetsCategoryExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCategoryExcelExporterResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCategoryExcelExporterResponse>(GlobalErrors.InvalidCompany);

        var responseGet = await _mediator.Send(new GetsCategoriesQuery(
            request.Ids,
            request.FilterData,
            null, null,
            request.IsActive,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (responseGet.IsFailure || responseGet.Value is null || responseGet.Value.Data is null)
            return Result.Failure<GetsCategoryExcelExporterResponse>(responseGet.Error!);

        var values = responseGet.Value.Data;
        var companyIds = values?.Where(w =>
                                    w.CompanyId != null &&
                                    w.CompanyId > 0)
                                .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsCategoryExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(CategoryExcels.CategoryToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Categories-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsCategoryExcelExporterResponse(file);
    }

    public async Task<Result<GetsCategoryExcelEnumResponse?>> GetsCategoryExcelEnum(
        GetsCategoryExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCategoryExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<CategoryExcelEnum>());
        return new GetsCategoryExcelEnumResponse(response);
    }

    private async Task UpdatePreferential(
        Category value, CT ct)
    {
        try
        {
            var command = new UpdatePreferentialNameFaAndStatusCommand(
                value.PreferentialReferenceCode,
                value.CategoryName,
                value.IsActive,
                value.IsDeleted);

            var response = await _mediator.Send(command, ct);
            if (response.IsFailure)
            {
                _logger.LogError(
                    "Update Preferential NameFa And Status for Category with id:{CategoryId} failed. Code:{Code}, Msg:{Msg}",
                    value.Id,
                    response.Error!.Code,
                    response.Error!.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}