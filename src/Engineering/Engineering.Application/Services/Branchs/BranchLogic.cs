using Engineering.Application.Services.Branchs.Commands.ActiveBranch;
using Engineering.Application.Services.Branchs.Commands.CodeCreator;
using Engineering.Application.Services.Branchs.Commands.CreateBranch;
using Engineering.Application.Services.Branchs.Commands.DisableBranch;
using Engineering.Application.Services.Branchs.Commands.InactiveBranch;
using Engineering.Application.Services.Branchs.Commands.StateChangerBranchs;
using Engineering.Application.Services.Branchs.Commands.UpdateBranch;
using Engineering.Application.Services.Branchs.Models.ActiveBranch;
using Engineering.Application.Services.Branchs.Models.BranchExcelImports;
using Engineering.Application.Services.Branchs.Models.BranchGroupDelete;
using Engineering.Application.Services.Branchs.Models.BranchModels;
using Engineering.Application.Services.Branchs.Models.CodeCreator;
using Engineering.Application.Services.Branchs.Models.CreateBranch;
using Engineering.Application.Services.Branchs.Models.DisableBranch;
using Engineering.Application.Services.Branchs.Models.GetBranchByCode;
using Engineering.Application.Services.Branchs.Models.GetBranchById;
using Engineering.Application.Services.Branchs.Models.GetBranchByName;
using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;
using Engineering.Application.Services.Branchs.Models.GetsBranchByFilterData;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;
using Engineering.Application.Services.Branchs.Models.GetsBranchs;
using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;
using Engineering.Application.Services.Branchs.Models.InactiveBranch;
using Engineering.Application.Services.Branchs.Models.StateChangerBranchs;
using Engineering.Application.Services.Branchs.Models.UpdateBranch;
using Engineering.Application.Services.Branchs.Queries.GetBranchByCode;
using Engineering.Application.Services.Branchs.Queries.GetBranchById;
using Engineering.Application.Services.Branchs.Queries.GetBranchByIdForResponse;
using Engineering.Application.Services.Branchs.Queries.GetBranchByName;
using Engineering.Application.Services.Branchs.Queries.GetBranchByNamesOrCodes;
using Engineering.Application.Services.Branchs.Queries.GetsActiveBranchs;
using Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryId;
using Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryIds;
using Engineering.Application.Services.Branchs.Queries.GetsBranchByIds;
using Engineering.Application.Services.Branchs.Queries.GetsBranchs;
using Engineering.Application.Services.Branchs.Queries.GetsBranchsForResponse;
using Engineering.Application.Services.Branchs.Queries.GetsBranchsResponseModel;
using Engineering.Application.Services.Categories.Queries.GetCategoryWithoutInclude;
using Engineering.Application.Services.Categories.Queries.GetsCategoryByCodes;
using Engineering.Application.Services.Seasons.Queries.GetSeasons;
using Engineering.Domain.Constants;
using Engineering.Domain.Entities.Branchs;
using Engineering.Domain.Entities.Seasons;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Commands.UpdatePreferentialNameFaAndStatus;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.PreferentialTemporary.Commands.CreatePreferentialTemporary;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.Branchs;

public class BranchLogic : IBranchLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<BranchLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public BranchLogic(
        IMediator mediator,
        ILogger<BranchLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateBranchResponse?>> CreateBranch(
        CreateBranchRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateBranch, BranchName:{BranchName}, BranchCode:{BranchCode},", request.BranchName, request.BranchCode);

        var isValidRequest = await request.IsValidAsync<CreateBranchValidator, CreateBranchRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateBranchResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateBranchResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateBranchResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetBranchByNameQuery(
            request.BranchName,
            request.CategoryId,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateBranchResponse>(BranchErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetBranchByCodeQuery(
            request.BranchCode,
            request.CategoryId,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateBranchResponse>(BranchErrors.CodeIsDuplicate);

        var getCategory = await _mediator.Send(new GetCategoryWithoutIncludeQuery(request.CategoryId), ct);
        if (getCategory.IsFailure)
            return Result.Failure<CreateBranchResponse>(BranchErrors.BranchCategoryNotFound);

        var response = await _mediator.Send(new CreateBranchCommand(
            getCategory!.Value!,
            request.BranchName,
            request.BranchCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateBranchResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        try
        {
            var createPreferentialTemporary = new CreatePreferentialTemporaryCommand(
                response.Value!.GetPreferentialName(),
                DepartmentNames.Engineering.Branch,
                response.Value!.PreferentialReferenceCode,
                true,
                null,
                response.Value.Category.PreferentialReferenceCode);

            var createPreferentialTemporaryResponse = await _mediator.Send(createPreferentialTemporary, ct);
            if (createPreferentialTemporaryResponse.IsFailure)
                _logger.LogError("CreatePreferentialTemporary for Branch with id of {BranchId} failed with Code:{Code}, Msg:{Msg}", response.Value!.Id, createPreferentialTemporaryResponse.Error!.Code, createPreferentialTemporaryResponse.Error!.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return new CreateBranchResponse(response.Value!.Id);
    }

    public async Task<Result<BranchExcelImportsResponse?>> BranchExcelImports(
        BranchExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for BranchExcelImports");

        var isValidRequest =
            await request.IsValidAsync<BranchExcelImportsValidator, BranchExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<BranchExcelImportsResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (!await CompanyValidator.IsCompanyValid(companyId, _mediator, ct))
            return Result.Failure<BranchExcelImportsResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse =
                await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<BranchExcelImportsResponse>(companyResponse.Error!);
        }

        var branchs = ExcelImporter.Import<BranchExcelImportsModel>(request.DocumentFile);
        if (branchs is null)
            return Result.Failure<BranchExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);

        if (branchs.Count != branchs.Select(s => s.BranchName).Distinct().Count())
            return Result.Failure<BranchExcelImportsResponse>(
                GlobalErrors.ExcelImporteredHaveNameDuplicate);

        if (branchs.Count != branchs.Select(s => s.BranchCode).Distinct().Count())
            return Result.Failure<BranchExcelImportsResponse>(
                GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        var categoryCodes = branchs.Select(s => s.CategoryCode).Distinct().ToList();
        var categoryResponse =
            await _mediator.Send(new GetsCategoryByCodesQuery(categoryCodes, companyId), ct);

        if (categoryResponse.IsFailure || categoryResponse.Value?.Data is null)
            return Result.Failure<BranchExcelImportsResponse>(
                BranchErrors.BranchCategoryNotFoundWithCode);

        var categories = categoryResponse.Value.Data;
        if (categoryCodes.Count != categories.Count)
            return Result.Failure<BranchExcelImportsResponse>(
                BranchErrors.BranchCategoryNotFoundWithCode);

        var categoryLookup =
            categories.ToDictionary(c => c.CategoryCode, c => c.Id);

        var branchesByCategory = branchs.GroupBy(b => b.CategoryCode);

        foreach (var group in branchesByCategory)
        {
            if (!categoryLookup.TryGetValue(group.Key, out var categoryId))
                return Result.Failure<BranchExcelImportsResponse>(
                    BranchErrors.BranchCategoryNotFoundWithCode);

            var names = group.Select(s => s.BranchName).ToList();
            var codes = group.Select(s => s.BranchCode).ToList();

            var hasDuplicate = await _mediator.Send(
                new GetBranchByNamesOrCodesQuery(
                    names,
                    categoryId,
                    codes,
                    companyId), ct);

            if (hasDuplicate.Value)
                return Result.Failure<BranchExcelImportsResponse>(
                    GlobalErrors.HaveDuplicateKey);
        }

        foreach (var item in branchs)
        {
            var category = categories
                .FirstOrDefault(w => w.CategoryCode == item.CategoryCode);

            if (category is null)
                return Result.Failure<BranchExcelImportsResponse>(
                    BranchErrors.BranchCategoryNotFoundWithCode);

            var response = await _mediator.Send(new CreateBranchCommand(
                category,
                item.BranchName,
                item.BranchCode,
                item.IsActive,
                companyId), ct);

            if (response.IsFailure)
                return Result.Failure<BranchExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new BranchExcelImportsResponse(true);
    }

    public async Task<Result<BranchCodeCreatorResponse?>> BranchCodeCreator(
        BranchCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for BranchCodeCreator");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<BranchCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<BranchCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new BranchCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<BranchCodeCreatorResponse>(response.Error!);

        return new BranchCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<DisableBranchResponse?>> DisableBranch(
        DisableBranchRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableBranch, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableBranchValidator, DisableBranchRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableBranchResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetBranchByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<DisableBranchResponse>(responseGet.Error!);

        var response = await _mediator.Send(new DisableBranchCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<DisableBranchResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new DisableBranchResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<BranchGroupDeleteResponse?>> BranchGroupDelete(
        BranchGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for BranchGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<BranchGroupDeleteValidator, BranchGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<BranchGroupDeleteResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetsBranchByIdsQuery(request.Ids), ct);
        if (responseGet.IsFailure)
            return Result.Failure<BranchGroupDeleteResponse>(responseGet.Error!);

        foreach (var item in responseGet.Value!)
        {
            var response = await _mediator.Send(new DisableBranchCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<BranchGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new BranchGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateBranchResponse?>> UpdateBranch(
        UpdateBranchRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateBranch, id:{Id}, branch:{BranchName} , description:{BranchCode},", request.Id, request.BranchName, request.BranchCode);

        var isValidRequest = await request.IsValidAsync<UpdateBranchValidator, UpdateBranchRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateBranchResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateBranchResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateBranchResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetBranchByNameQuery(
            request.BranchName,
            request.CategoryId,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateBranchResponse>(BranchErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetBranchByCodeQuery(
            request.BranchCode,
            request.CategoryId,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateBranchResponse>(BranchErrors.CodeIsDuplicate);

        var category = await _mediator.Send(new GetCategoryWithoutIncludeQuery(request.CategoryId), ct);
        if (category.IsFailure)
            return Result.Failure<UpdateBranchResponse>(BranchErrors.BranchCategoryNotFound);

        var response = await _mediator.Send(new UpdateBranchCommand(
            request.Id,
            category.Value!,
            request.BranchName,
            request.BranchCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateBranchResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new UpdateBranchResponse(response.Value!.Id);
    }

    public async Task<Result<InactiveBranchResponse?>> InactiveBranch(
        InactiveBranchRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveBranch, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveBranchValidator, InactiveBranchRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveBranchResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetBranchByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<InactiveBranchResponse>(responseGet.Error!);

        var response = await _mediator.Send(new InactiveBranchCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveBranchResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new InactiveBranchResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveBranchResponse?>> ActiveBranch(
        ActiveBranchRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveBranch, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveBranchValidator, ActiveBranchRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveBranchResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetBranchByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<ActiveBranchResponse>(responseGet.Error!);

        var response = await _mediator.Send(new ActiveBranchCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveBranchResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new ActiveBranchResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerBranchsResponse?>> StateChangerBranchs(
        StateChangerBranchsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerBranchs, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerBranchsValidator, StateChangerBranchsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerBranchsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerBranchsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsBranchByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerBranchsResponse>(responses.Error!);

        var response = await _mediator.Send(new StateChangerBranchsCommand(responses.Value, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerBranchsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerBranchsResponse(true);
    }

    public async Task<Result<GetBranchByIdResponse?>> GetBranchById(
        GetBranchByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetBranchById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetBranchByIdValidator, GetBranchByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetBranchByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetBranchByIdForResponseQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetBranchByIdResponse>(response.Error!);

        var value = response.Value!;
        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        response.Value!.CompanyNameFa = company?.NameFa;
        return response;
    }

    public async Task<Result<GetBranchByNameResponse?>> GetBranchByName(
        GetBranchByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetBranchByName branchName:{BranchName}", request.BranchName);

        var isValidRequest = await request.IsValidAsync<GetBranchByNameValidator, GetBranchByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetBranchByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetBranchByNameQuery(request.BranchName, request.CategoryId, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetBranchByNameResponse>(response.Error!);

        return response.Value;
    }

    public async Task<Result<GetBranchByCodeResponse?>> GetBranchByCode(
        GetBranchByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetBranchByCode, branchCode:{BranchCode}", request.BranchCode);

        var isValidRequest = await request.IsValidAsync<GetBranchByCodeValidator, GetBranchByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetBranchByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetBranchByCodeQuery(request.BranchCode, request.CategoryId, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetBranchByCodeResponse>(response.Error!);

        return response.Value!;
    }

    public async Task<Result<GetsActiveBranchsResponse?>> GetsActiveBranch(
        GetsActiveBranchsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveBranch, BranchCode:{BranchCode} , BranchName:{BranchName}, pageIndex:{PageIndex} , pageSize:{PageSize}", request.BranchCode, request.BranchName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveBranchsValidator, GetsActiveBranchsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveBranchsResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsActiveBranchsResponse>(GlobalErrors.InvalidCompany);

        var response = await _mediator.Send(new GetsActiveBranchsQuery(
            request.FilterData,
            request.CategoryId,
            request.BranchCode,
            request.BranchName,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveBranchsResponse>(response.Error!);

        return new GetsActiveBranchsResponse(response.Value!.Data ?? new List<GetsActiveBranchsResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsBranchsResponse?>> GetsBranch(
        GetsBranchsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsBranch,BranchName:{BranchName} , BranchCode:{BranchCode}, IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}", request.BranchName, request.BranchCode, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsBranchsValidator, GetsBranchsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsBranchsResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsBranchsResponse>(GlobalErrors.InvalidCompany);
        var response = await _mediator.Send(new GetsBranchsResponseModelQuery(
            request.FilterData,
            request.CategoryId,
            request.BranchName,
            request.BranchCode,
            request.IsActive,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsBranchsResponse>(response.Error!);

        return new GetsBranchsResponse(response.Value!.Data, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsBranchByFilterDataResponse?>> GetsBranchByFilterData(
        GetsBranchsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsBranchByFilterData, FilterData:{FilterData} , pageSize:{PageSize}, pageSize:{PageSize}", request.FilterData, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsBranchsValidator, GetsBranchsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsBranchByFilterDataResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsBranchByFilterDataResponse>(GlobalErrors.InvalidCompany);

        var branchResponse = await _mediator.Send(new GetsBranchsQuery(
            null,
            request.FilterData,
            request.CategoryId,
            null, null, null, null,
            companyId,
            1, 100), ct);
        var branchData = branchResponse.Value?.Data;

        var seasonResponse = await _mediator.Send(new GetSeasonsQuery(
            null,
            request.FilterData,
            null,
            request.CategoryId,
            null, null, null, null,
            companyId,
            1, 100), ct);
        var seasonData = seasonResponse.Value?.Data;

        if (branchResponse.IsFailure && seasonResponse.IsFailure)
            return Result.Failure<GetsBranchByFilterDataResponse>(branchResponse.Error!);

        var companyIds = branchData?.Where(w =>
                                        w.CompanyId != null &&
                                        w.CompanyId > 0)
                                    .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);
        var data = GetsBranchByFilterDataModeling(branchData, seasonData, companies);
        return new GetsBranchByFilterDataResponse(data ?? new List<GetsBranchByFilterDataResponseModel>(0), data!.Count());
    }

    public async Task<Result<GetsByCategoryIdResponse?>> GetsByCategoryId(
        GetsByCategoryIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByCategoryId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByCategoryIdValidator, GetsByCategoryIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByCategoryIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsBranchByCategoryIdQuery(
            request.CategoryId,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByCategoryIdResponse>(response.Error!);

        var values = response.Value!.Data;
        var companyIds = values?.Where(w =>
                                    w.CompanyId != null &&
                                    w.CompanyId > 0)
                                .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        foreach (var item in response.Value.Data!)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByCategoryIdResponse(response.Value.Data ?? new List<GetsBranchByCategoryIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsBranchByCategoryIdsResponse?>> GetsBranchByCategoryIds(
        GetsBranchByCategoryIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsBranchByCategoryIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsBranchByCategoryIdsValidator, GetsBranchByCategoryIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsBranchByCategoryIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsBranchByCategoryIdsQuery(
            request.CategoryIds,
            request.FilterData,
            request.IsActive,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsBranchByCategoryIdsResponse>(response.Error!);

        return new GetsBranchByCategoryIdsResponse(response.Value!.Data ?? new List<GetsBranchByCategoryIdsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsBranchExcelExporterResponse?>> GetsBranchExcelExporter(
        GetsBranchExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsBranchExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsBranchExcelExporterValidator, GetsBranchExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsBranchExcelExporterResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsBranchExcelExporterResponse>(GlobalErrors.InvalidCompany);

        var responses = await _mediator.Send(new GetsBranchsForResponseQuery(
            request.Ids,
            request.FilterData,
            request.CategoryId,
            request.BranchName,
            request.BranchCode,
            request.IsActive,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null)
            return Result.Failure<GetsBranchExcelExporterResponse>(responses.Error!);

        var values = responses.Value;
        var companyIds = values?.Where(w =>
                                    w.CompanyId != null &&
                                    w.CompanyId > 0)
                                .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        foreach (var item in responses.Value)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(BranchExcels.BranchToExcel(responses.Value, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Branchs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsBranchExcelExporterResponse(file);
    }

    public async Task<Result<GetsBranchExcelEnumResponse?>> GetsBranchExcelEnum(
        GetsBranchExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsBranchExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<BranchExcelEnum>());
        return new GetsBranchExcelEnumResponse(response);
    }

    #region ModelCollector

    private List<GetsBranchByFilterDataResponseModel>? GetsBranchByFilterDataModeling(
        List<Branch>? branchData,
        List<Season>? seasonData,
        List<Company>? companies)
    {
        var result = new List<GetsBranchByFilterDataResponseModel>();

        if (branchData is not null && branchData.Count > 0)
            foreach (var item in branchData)
            {
                var company = companies?.Where(w =>
                                            w.Id == item.CompanyId)
                                        .FirstOrDefault();
                result.Add(new()
                {
                    BranchCode = item.BranchCode,
                    BranchName = item.BranchName,
                    ChildCount = item.Seasons.Count,
                    CompanyId = item.CompanyId,
                    CompanyNameFa = company?.NameFa,
                    Id = item.Id,
                    HaveChild = item.Seasons.Any(),
                    IsActive = item.IsActive,
                    Seasons = new()
                });
            }

        if (seasonData is not null && seasonData.Count > 0)
            foreach (var item in seasonData)
            {
                var company = companies?.Where(w =>
                                            w.Id == item.CompanyId)
                                        .FirstOrDefault();
                var season = new SeasonModel(
                    item.Id,
                    item.SeasonName,
                    item.SeasonCode,
                    item.IsActive);

                if (result.Any(a => a.Id == item.Branch.Id))
                    result.Where(w => w.Id == item.Branch.Id)
                        .FirstOrDefault()!.Seasons?
                        .Add(season);
                else
                {
                    result.Add(new()
                    {
                        BranchCode = item.Branch.BranchCode,
                        BranchName = item.Branch.BranchName,
                        ChildCount = item.Branch.Seasons.Count,
                        CompanyId = item.CompanyId,
                        CompanyNameFa = company?.NameFa,
                        Id = item.Id,
                        HaveChild = item.Branch.Seasons.Any(),
                        IsActive = item.Branch.IsActive,
                        Seasons = new()
                    });
                    result.Where(w =>
                              w.Id == item.Branch.Id)
                          .FirstOrDefault()!.Seasons?
                          .Add(season);
                }
            }
        return result;
    }

    private async Task UpdatePreferential(
        Branch value, CT ct)
    {
        try
        {
            var command = new UpdatePreferentialNameFaAndStatusCommand(
                value.PreferentialReferenceCode,
                value.BranchName,
                value.IsActive,
                value.IsDeleted);

            var response = await _mediator.Send(command, ct);
            if (response.IsFailure)
            {
                _logger.LogError(
                    "Update Preferential NameFa And Status for Branch with id:{BranchId} failed. Code:{Code}, Msg:{Msg}",
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

    #endregion

}