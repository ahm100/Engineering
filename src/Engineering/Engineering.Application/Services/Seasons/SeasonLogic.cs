using Engineering.Application.Services.Branchs.Queries.GetBranchById;
using Engineering.Application.Services.Branchs.Queries.GetBranchWithoutInclude;
using Engineering.Application.Services.Branchs.Queries.GetsBranchByCodes;
using Engineering.Application.Services.Categories.Queries.GetCategoryByCode;
using Engineering.Application.Services.Seasons.Commands.ActiveSeason;
using Engineering.Application.Services.Seasons.Commands.CodeCreator;
using Engineering.Application.Services.Seasons.Commands.CreateSeason;
using Engineering.Application.Services.Seasons.Commands.DisableSeason;
using Engineering.Application.Services.Seasons.Commands.InactiveSeason;
using Engineering.Application.Services.Seasons.Commands.StateChangerSeasons;
using Engineering.Application.Services.Seasons.Commands.UpdateSeason;
using Engineering.Application.Services.Seasons.Models.ActiveSeason;
using Engineering.Application.Services.Seasons.Models.CodeCreator;
using Engineering.Application.Services.Seasons.Models.CreateSeason;
using Engineering.Application.Services.Seasons.Models.DisableSeason;
using Engineering.Application.Services.Seasons.Models.GetActiveSeasons;
using Engineering.Application.Services.Seasons.Models.GetsByBranchId;
using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;
using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;
using Engineering.Application.Services.Seasons.Models.GetSeasonById;
using Engineering.Application.Services.Seasons.Models.GetSeasonByName;
using Engineering.Application.Services.Seasons.Models.GetSeasons;
using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelEnum;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;
using Engineering.Application.Services.Seasons.Models.InactiveSeason;
using Engineering.Application.Services.Seasons.Models.SeasonExcelImports;
using Engineering.Application.Services.Seasons.Models.SeasonGroupDelete;
using Engineering.Application.Services.Seasons.Models.SeasonModels;
using Engineering.Application.Services.Seasons.Models.StateChangerSeasons;
using Engineering.Application.Services.Seasons.Models.UpdateSeason;
using Engineering.Application.Services.Seasons.Queries.FindSeasonByNamesOrCodes;
using Engineering.Application.Services.Seasons.Queries.GetActiveSeasons;
using Engineering.Application.Services.Seasons.Queries.GetActiveSeasonsForResponse;
using Engineering.Application.Services.Seasons.Queries.GetsByBranchId;
using Engineering.Application.Services.Seasons.Queries.GetsByBranchIdForResponse;
using Engineering.Application.Services.Seasons.Queries.GetsByBranchIdWhithOperationInfo;
using Engineering.Application.Services.Seasons.Queries.GetSeasonByCode;
using Engineering.Application.Services.Seasons.Queries.GetSeasonByCodeForResponse;
using Engineering.Application.Services.Seasons.Queries.GetSeasonById;
using Engineering.Application.Services.Seasons.Queries.GetSeasonByIdForResponse;
using Engineering.Application.Services.Seasons.Queries.GetSeasonByName;
using Engineering.Application.Services.Seasons.Queries.GetSeasonByNameForRespone;
using Engineering.Application.Services.Seasons.Queries.GetSeasons;
using Engineering.Application.Services.Seasons.Queries.GetSeasonsByBranchId;
using Engineering.Application.Services.Seasons.Queries.GetsSeasonByBranchIds;
using Engineering.Application.Services.Seasons.Queries.GetsSeasonByBranchIdsForResponse;
using Engineering.Application.Services.Seasons.Queries.GetsSeasonByIds;
using Engineering.Domain.Constants;
using Engineering.Domain.Entities.Seasons;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Commands.UpdatePreferentialNameFaAndStatus;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.PreferentialTemporary.Commands.CreatePreferentialTemporary;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.Seasons;

public class SeasonLogic : ISeasonLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<SeasonLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public SeasonLogic(IMediator mediator, ILogger<SeasonLogic> logger, IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateSeasonResponse?>> CreateSeason(CreateSeasonRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateSeason, SeasonName:{SeasonName}, SeasonCode:{SeasonCode},",
            request.SeasonName, request.SeasonCode);

        var isValidRequest = await request.IsValidAsync<CreateSeasonValidator, CreateSeasonRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateSeasonResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateSeasonResponse>(companyResponse.Error!);
        }

        var branchSeasonNames = await _mediator.Send(new GetSeasonsByBranchIdQuery(request.BranchId, companyId.Value!));
        if (branchSeasonNames.IsBad())
            return branchSeasonNames.Failure<CreateSeasonResponse>()!;
        var nameIsDuplicate = branchSeasonNames.Value.Any(x => x.SeasonName == request.SeasonName);
        if (nameIsDuplicate)
            return Result.Failure<CreateSeasonResponse>(SeasonErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetSeasonByCodeQuery(request.SeasonCode, request.BranchId, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateSeasonResponse>(SeasonErrors.CodeIsDuplicate);

        var branch = await _mediator.Send(new GetBranchByIdQuery(request.BranchId), ct);
        if (branch.IsFailure)
            return Result.Failure<CreateSeasonResponse>(branch.Error!);

        var response = await _mediator.Send(new CreateSeasonCommand(branch.Value!, request.SeasonCode, request.SeasonName, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateSeasonResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        try
        {
            var createPreferentialTemporary = new CreatePreferentialTemporaryCommand(response.Value!.GetPreferentialName(),
                DepartmentNames.Engineering.Season, response.Value!.PreferentialReferenceCode, true, null, response.Value.Branch.PreferentialReferenceCode);

            var createPreferentialTemporaryResponse = await _mediator.Send(createPreferentialTemporary, ct);
            if (createPreferentialTemporaryResponse.IsFailure)
                _logger.LogError("CreatePreferentialTemporary for Season with id of {SeasonId} failed with Code:{Code}, Msg:{Msg}", response.Value!.Id, createPreferentialTemporaryResponse.Error!.Code, createPreferentialTemporaryResponse.Error!.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        var value = response.Value!;
        return new CreateSeasonResponse(value.Id, value.SeasonCode, value.SeasonName, branch.Value!.Id, branch.Value.BranchName, value.IsActive);
    }

    public async Task<Result<SeasonExcelImportsResponse?>> SeasonExcelImports(SeasonExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for SeasonExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<SeasonExcelImportsValidator, SeasonExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SeasonExcelImportsResponse>(isValidRequest.Error!);

        var seasons = ExcelImporter.Import<SeasonExcelImportsModel>(request.DocumentFile);
        if (seasons is null)
            return Result.Failure<SeasonExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (seasons.Count != seasons.Select(x => x.SeasonName).Distinct().Count())
            return Result.Failure<SeasonExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (seasons.Count != seasons.Select(x => x.SeasonCode).Distinct().Count())
            return Result.Failure<SeasonExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<SeasonExcelImportsResponse>(companyResponse.Error!);
        }

        var branchCode = seasons.Select(x => x.BranchCode).Distinct().ToList();
        var category = seasons.FirstOrDefault().CategoryCode;
        var categoryValue = await _mediator.Send(new GetCategoryByCodeQuery(category));
        if (categoryValue.IsBad())
            return categoryValue.Failure<SeasonExcelImportsResponse>()!;
        var categoryId = categoryValue.Value.Id;
        var branchs = await _mediator.Send(new GetsBranchByCodesQuery(branchCode, categoryId, null), ct);
        if (branchs.IsBad())
            return branchs.Failure<SeasonExcelImportsResponse>()!;
        var names = seasons.Select(x => x.SeasonName).ToList();
        var codes = seasons.Select(x => x.SeasonCode).ToList();
        foreach (var branch in branchs.Value.Data)
        {
            var seasonNames = seasons.Where(x => x.BranchCode == branch.BranchCode).Select(x => x.SeasonName);
            var seasonCodes = seasons.Where(x => x.BranchCode == branch.BranchCode).Select(x => x.SeasonCode);
            var codeIsDuplicate = await _mediator.Send(new FindSeasonByNamesOrCodesQuery(names, codes, categoryId, companyId), ct);
            if (codeIsDuplicate.Value)
                return Result.Failure<SeasonExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);
        }
        var branchCodes = seasons.Select(x => x.BranchCode).Distinct().ToList();
        var branchResponse = await _mediator.Send(new GetsBranchByCodesQuery(branchCodes, categoryId, companyId), ct);
        if (branchResponse.IsFailure)
            return Result.Failure<SeasonExcelImportsResponse>(SeasonErrors.SeasonBranchNotFoundWithCode);
        var categories = branchResponse.Value!.Data;
        if (branchCodes.Count != categories!.Count)
            return Result.Failure<SeasonExcelImportsResponse>(SeasonErrors.SeasonBranchNotFoundWithCode);

        foreach (var item in seasons)
        {
            var branch = categories.Where(x => x.BranchCode == item.BranchCode).FirstOrDefault();
            if (branch is null)
                return Result.Failure<SeasonExcelImportsResponse>(SeasonErrors.SeasonBranchNotFoundWithCode);

            var response = await _mediator.Send(new CreateSeasonCommand(branch!, item.SeasonName, item.SeasonCode, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<SeasonExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new SeasonExcelImportsResponse(true);
    }

    public async Task<Result<SeasonCodeCreatorResponse?>> CodeCreator(SeasonCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<SeasonCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<SeasonCodeCreatorResponse>(response.Error!);

        return new SeasonCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerSeasonsResponse?>> StateChangerSeasons(StateChangerSeasonsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerSeasons, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerSeasonsValidator, StateChangerSeasonsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerSeasonsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerSeasonsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsSeasonByIdsQuery(request.Ids, 1, request.Ids.Count()), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerSeasonsResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerSeasonsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerSeasonsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerSeasonsResponse(true);
    }

    public async Task<Result<DisableSeasonResponse?>> DisableSeason(DisableSeasonRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableSeason, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableSeasonValidator, DisableSeasonRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableSeasonResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableSeasonCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableSeasonResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new DisableSeasonResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<SeasonGroupDeleteResponse?>> SeasonGroupDelete(SeasonGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for SeasonGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<SeasonGroupDeleteValidator, SeasonGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SeasonGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableSeasonCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<SeasonGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new SeasonGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateSeasonResponse?>> UpdateSeason(UpdateSeasonRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateSeason, id:{Id}, Season:{SeasonName} , description:{SeasonCode},",
            request.Id, request.SeasonName, request.SeasonCode);
        //Validate data
        var isValidRequest = await request.IsValidAsync<UpdateSeasonValidator, UpdateSeasonRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateSeasonResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        Company? company = null;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateSeasonResponse>(companyResponse.Error!);
            company = companyResponse.Value;
        }

        //Validate code
        var codeIsDuplicate = await _mediator.Send(new GetSeasonByCodeQuery(request.SeasonCode, request.BranchId, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateSeasonResponse>(SeasonErrors.CodeIsDuplicate);
        //Validate name
        var nameIsDuplicate = await _mediator.Send(new GetSeasonByNameQuery(request.SeasonName, request.BranchId, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateSeasonResponse>(SeasonErrors.NameIsDuplicate);
        //Find Branch
        var branch = await _mediator.Send(new GetBranchWithoutIncludeQuery(request.BranchId), ct);
        if (branch.IsFailure)
            return Result.Failure<UpdateSeasonResponse>(SeasonErrors.SeasonBranchNotFound);

        var response = await _mediator.Send(new UpdateSeasonCommand(request.Id, branch.Value!, request.SeasonCode, request.SeasonName, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateSeasonResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        var value = response.Value!;
        return new UpdateSeasonResponse(value.Id, value.SeasonName, value.SeasonCode, branch.Value!.Id, branch.Value!.BranchName, branch.Value!.BranchCode, request.IsActive, companyId, company?.NameFa);
    }

    public async Task<Result<InactiveSeasonResponse?>> InactiveSeason(InactiveSeasonRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveSeason, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveSeasonValidator, InactiveSeasonRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveSeasonResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveSeasonCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveSeasonResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new InactiveSeasonResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveSeasonResponse?>> ActiveSeason(ActiveSeasonRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveSeason, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveSeasonValidator, ActiveSeasonRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveSeasonResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveSeasonCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveSeasonResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        await UpdatePreferential(response.Value!, ct);
        return new ActiveSeasonResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<GetSeasonByIdResponse?>> GetSeasonById(GetSeasonByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetSeasonById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetSeasonByIdValidator, GetSeasonByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSeasonByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetSeasonByIdForResponseQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetSeasonByIdResponse>(response.Error!);

        Company? company = null;
        if (response.Value!.CompanyId is not null && response.Value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(response.Value.CompanyId, _mediator, ct);

        response.Value!.CompanyNameFa = company?.NameFa;

        return response;
    }

    public async Task<Result<GetSeasonByNameResponse?>> GetSeasonByName(GetSeasonByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetSeasonByName, SeasonName:{SeasonName}", request.SeasonName);

        var isValidRequest = await request.IsValidAsync<GetSeasonByNameValidator, GetSeasonByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSeasonByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetSeasonByNameForResponeQuery(request.SeasonName, request.BranchId), ct);
        if (response.IsFailure)
            return Result.Failure<GetSeasonByNameResponse>(response.Error!);

        Company? company = null;
        if (response.Value!.CompanyId is not null && response.Value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(response.Value!.CompanyId, _mediator, ct);

        response.Value.CompanyNameFa = company?.NameFa;

        return response;
    }

    public async Task<Result<GetSeasonByCodeResponse?>> GetSeasonByCode(GetSeasonByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetSeasonByCode, SeasonCode:{SeasonCode}", request.SeasonCode);

        var isValidRequest = await request.IsValidAsync<GetSeasonByCodeValidator, GetSeasonByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSeasonByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetSeasonByCodeForResponseQuery(request.SeasonCode, request.BranchId), ct);
        if (response.IsFailure)
            return Result.Failure<GetSeasonByCodeResponse>(response.Error!);

        Company? company = null;
        if (response.Value!.CompanyId is not null && response.Value!.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(response.Value!.CompanyId, _mediator, ct);

        response.Value.CompanyNameFa = company?.NameFa;

        return response.Value;
    }

    public async Task<Result<GetActiveSeasonsResponse?>> GetActiveSeasons(GetActiveSeasonsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveSeasons,SeasonCode:{SeasonCode} , SeasonName:{SeasonName}, pageIndex:{PageIndex} , pageSize:{PageSize}",
           request.SeasonCode, request.SeasonName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveSeasonsValidator, GetActiveSeasonsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveSeasonsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveSeasonsForResponseQuery(request.FilterData, request.branchId, request.SeasonCode, request.SeasonName, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveSeasonsResponse>(response.Error!);
        var values = response.Value?.Data;

        return new GetActiveSeasonsResponse(response.Value!.Data ?? new List<GetsActiveSeasonModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetSeasonsResponse?>> GetSeasons(GetSeasonsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetSeasons, SeasonCode:{SeasonCode} , SeasonName:{SeasonName}, IsActive:{IsActive} ,pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.SeasonCode, request.SeasonName, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetSeasonsValidator, GetSeasonsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSeasonsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetSeasonsForResponseQuery(null, request.FilterData, request.BranchId, null, request.SeasonCode,
            request.SeasonName, request.IsActive, request.OrderBy, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetSeasonsResponse>(response.Error!);

        var companyIds = response.Value!.Data?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = new List<GetSeasonsModel>();
        foreach (var seasonData in response.Value.Data!)
        {
            var company = companies?.Where(x => x.Id == seasonData.CompanyId).FirstOrDefault();
            data.Add(new GetSeasonsModel(seasonData.Id, seasonData.SeasonName, seasonData.SeasonCode, seasonData.BranchId, seasonData.BranchName,
                seasonData.BranchCode, seasonData.IsActive, seasonData.CompanyId, company?.NameFa));
        }

        return new GetSeasonsResponse(response.Value.Data ?? new List<GetSeasonsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByBranchIdResponse?>> GetsByBranchId(GetsByBranchIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByBranchId, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByBranchIdValidator, GetsByBranchIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByBranchIdResponse>(isValidRequest.Error!);

        var getBranch = await _mediator.Send(new GetBranchWithoutIncludeQuery(request.BranchId), ct);
        if (getBranch.IsFailure)
            return Result.Failure<GetsByBranchIdResponse>(getBranch.Error!);

        var response = await _mediator.Send(new GetsByBranchIdForResponseQuery(request.BranchId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByBranchIdResponse>(response.Error!);

        var haveChild = false;
        if (response.Value!.RowCount != 0)
            haveChild = true;
        var childCount = response.Value.RowCount;

        var companyIds = response.Value.Data?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        foreach (var item in response.Value.Data!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByBranchIdResponse(response.Value.Data ?? new List<GetsByBranchIdModel>(0), haveChild, childCount, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsSeasonByBranchIdsResponse?>> GetsSeasonByBranchIds(GetsSeasonByBranchIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSeasonByBranchIds, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsSeasonByBranchIdsValidator, GetsSeasonByBranchIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSeasonByBranchIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsSeasonByBranchIdsForResponseQuery(request.BranchIds, request.FilterData, request.IsActive, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsSeasonByBranchIdsResponse>(response.Error!);

        var haveChild = false;
        if (response.Value!.RowCount != 0)
            haveChild = true;
        var childCount = response.Value.RowCount;

        return new GetsSeasonByBranchIdsResponse(response.Value.Data ?? new List<GetsSeasonByBranchIdsModel>(0), haveChild, childCount, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByBranchIdWhithOperationInfoResponse?>> GetsByBranchIdWhithOperationInfo(GetsByBranchIdWhithOperationInfoRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByBranchIdWhithOperationInfo, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByBranchIdWhithOperationInfoValidator, GetsByBranchIdWhithOperationInfoRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByBranchIdWhithOperationInfoResponse>(isValidRequest.Error!);

        var getBranch = await _mediator.Send(new GetBranchWithoutIncludeQuery(request.BranchId), ct);
        if (getBranch.IsFailure)
            return Result.Failure<GetsByBranchIdWhithOperationInfoResponse>(getBranch.Error!);

        var response = await _mediator.Send(new GetsByBranchIdWhithOperationInfoQuery(
            request.BranchId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByBranchIdWhithOperationInfoResponse>(response.Error!);

        var companyIds = response.Value!.Data?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        foreach (var item in response.Value!.Data!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByBranchIdWhithOperationInfoResponse(response.Value.Data! ?? new List<GetsByBranchIdWhithOperationInfoModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsSeasonExcelExporterResponse?>> GetsSeasonExcelExporter(GetsSeasonExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSeasonExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsSeasonExcelExporterValidator, GetsSeasonExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSeasonExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetSeasonsQuery(request.Ids, request.FilterData, request.BranchId, null, request.SeasonCode,
            request.SeasonName, request.IsActive, request.OrderBy, companyId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsSeasonExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsSeasonExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(SeasonExcels.SeasonToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Seasons-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsSeasonExcelExporterResponse(file);
    }

    public async Task<Result<GetsSeasonExcelEnumResponse?>> GetsSeasonExcelEnum(GetsSeasonExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSeasonExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<SeasonExcelEnum>());
        return new GetsSeasonExcelEnumResponse(response);
    }

    private async Task UpdatePreferential(Season value, CT ct)
    {
        try
        {
            var command = new UpdatePreferentialNameFaAndStatusCommand(
                value.PreferentialReferenceCode,
                value.SeasonName,
                value.IsActive,
                value.IsDeleted);

            var response = await _mediator.Send(command, ct);
            if (response.IsFailure)
            {
                _logger.LogError(
                    "Update Preferential NameFa And Status for Season with id:{SeasonId} failed. Code:{Code}, Msg:{Msg}",
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