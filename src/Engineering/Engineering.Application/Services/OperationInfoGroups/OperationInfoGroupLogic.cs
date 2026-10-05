using Engineering.Application.Services.OperationInfoGroups.Commands.ActiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Commands.CodeCreator;
using Engineering.Application.Services.OperationInfoGroups.Commands.CreateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Commands.DisableOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Commands.InactiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Commands.StateChangerOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Commands.UpdateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.ActiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.CodeCreator;
using Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.DisableOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetActiveOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByCode;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelEnum;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;
using Engineering.Application.Services.OperationInfoGroups.Models.InactiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupExcelImports;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupGroupDelete;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupModels;
using Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Queries.FindOperationInfoGroupByNamesOrCodes;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetActiveOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetByCode;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetByName;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroupByIds;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.OperationInfoGroups;

public class OperationInfoGroupLogic : IOperationInfoGroupLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoGroupLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public OperationInfoGroupLogic(
        IMediator mediator,
        ILogger<OperationInfoGroupLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateOperationInfoGroupResponse?>> CreateOperationInfoGroup(
        CreateOperationInfoGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationInfoGroup, OperationInfoGroupName:{OperationInfoGroupName}, OperationInfoGroupCode:{OperationInfoGroupCode},",
            request.OperationInfoGroupName, request.OperationInfoGroupCode);

        var isValidRequest = await request.IsValidAsync<CreateOperationInfoGroupValidator, CreateOperationInfoGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateOperationInfoGroupResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateOperationInfoGroupResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetOperationInfoGroupByNameQuery(request.OperationInfoGroupName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateOperationInfoGroupResponse>(OperationInfoGroupErrors.GroupNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetOperationInfoGroupByCodeQuery(request.OperationInfoGroupCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateOperationInfoGroupResponse>(OperationInfoGroupErrors.GroupCodeIsDuplicate);

        var response = await _mediator.Send(new CreateOperationInfoGroupCommand(request.OperationInfoGroupName, request.OperationInfoGroupCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateOperationInfoGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateOperationInfoGroupResponse>();
    }

    public async Task<Result<OperationInfoGroupExcelImportsResponse?>> OperationInfoGroupExcelImports(
        OperationInfoGroupExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfoGroupExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<OperationInfoGroupExcelImportsValidator, OperationInfoGroupExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<OperationInfoGroupExcelImportsResponse>(isValidRequest.Error!);

        var operationInfoGroups = ExcelImporter.Import<OperationInfoGroupExcelImportsModel>(request.DocumentFile);
        if (operationInfoGroups is null)
            return Result.Failure<OperationInfoGroupExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (operationInfoGroups.Count != operationInfoGroups.Select(x => x.OperationInfoGroupName).Distinct().Count())
            return Result.Failure<OperationInfoGroupExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (operationInfoGroups.Count != operationInfoGroups.Select(x => x.OperationInfoGroupCode).Distinct().Count())
            return Result.Failure<OperationInfoGroupExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<OperationInfoGroupExcelImportsResponse>(companyResponse.Error!);
        }

        var names = operationInfoGroups.Select(x => x.OperationInfoGroupName).ToList();
        var codes = operationInfoGroups.Select(x => x.OperationInfoGroupCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindOperationInfoGroupByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<OperationInfoGroupExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in operationInfoGroups)
        {
            var response = await _mediator.Send(new CreateOperationInfoGroupCommand(item.OperationInfoGroupName, item.OperationInfoGroupCode, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<OperationInfoGroupExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new OperationInfoGroupExcelImportsResponse(true);
    }

    public async Task<Result<OperationInfoGroupCodeCreatorResponse?>> OperationInfoGroupCodeCreator(
        OperationInfoGroupCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<OperationInfoGroupCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<OperationInfoGroupCodeCreatorResponse>(response.Error!);

        return new OperationInfoGroupCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerOperationInfoGroupsResponse?>> StateChangerOperationInfoGroups(
        StateChangerOperationInfoGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerOperationInfoGroups, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerOperationInfoGroupsValidator, StateChangerOperationInfoGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerOperationInfoGroupsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerOperationInfoGroupsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsOperationInfoGroupByIdsQuery(request.Ids, 1, request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerOperationInfoGroupsResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerOperationInfoGroupsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerOperationInfoGroupsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerOperationInfoGroupsResponse(true);
    }

    public async Task<Result<UpdateOperationInfoGroupResponse?>> UpdateOperationInfoGroup(
        UpdateOperationInfoGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateOperationInfoGroup, Id:{Id}, OperationInfoGroupName:{OperationInfoGroupName}, OperationInfoGroupCode:{OperationInfoGroupCode}",
           request.Id, request.OperationInfoGroupName, request.OperationInfoGroupCode);

        var isValidRequest = await request.IsValidAsync<UpdateOperationInfoGroupValidator, UpdateOperationInfoGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateOperationInfoGroupResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateOperationInfoGroupResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetOperationInfoGroupByNameQuery(request.OperationInfoGroupName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateOperationInfoGroupResponse>(OperationInfoGroupErrors.GroupNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetOperationInfoGroupByCodeQuery(request.OperationInfoGroupCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateOperationInfoGroupResponse>(OperationInfoGroupErrors.GroupCodeIsDuplicate);

        var response = await _mediator.Send(new UpdateOperationInfoGroupCommand(request.Id, request.OperationInfoGroupName, request.OperationInfoGroupCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateOperationInfoGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateOperationInfoGroupResponse>();
    }

    public async Task<Result<DisableOperationInfoGroupResponse?>> DisableOperationInfoGroup(
        DisableOperationInfoGroupRequest request, CT ct)
    {
        _logger.LogInformation("Disable OperationInfoGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableOperationInfoGroupValidator, DisableOperationInfoGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableOperationInfoGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableOperationInfoGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableOperationInfoGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableOperationInfoGroupResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<OperationInfoGroupGroupDeleteResponse?>> OperationInfoGroupGroupDelete(
        OperationInfoGroupGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for OperationInfoGroupGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<OperationInfoGroupGroupDeleteValidator, OperationInfoGroupGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<OperationInfoGroupGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableOperationInfoGroupCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<OperationInfoGroupGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new OperationInfoGroupGroupDeleteResponse(true);
    }

    public async Task<Result<InactiveOperationInfoGroupResponse?>> InactiveOperationInfoGroup(
        InactiveOperationInfoGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveOperationInfoGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveOperationInfoGroupValidator, InactiveOperationInfoGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveOperationInfoGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveOperationInfoGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveOperationInfoGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveOperationInfoGroupResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveOperationInfoGroupResponse?>> ActiveOperationInfoGroup(
        ActiveOperationInfoGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveOperationInfoGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveOperationInfoGroupValidator, ActiveOperationInfoGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveOperationInfoGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveOperationInfoGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveOperationInfoGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveOperationInfoGroupResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<GetOperationInfoGroupByIdResponse?>> GetOperationInfoGroupById(
        GetOperationInfoGroupByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoGroupById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetOperationInfoGroupByIdValidator, GetOperationInfoGroupByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoGroupByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationInfoGroupByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationInfoGroupByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetOperationInfoGroupByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetsOperationInfoGroupResponse?>> GetsOperationInfoGroup(
        GetsOperationInfoGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoGroup, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoGroupValidator, GetsOperationInfoGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoGroupResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsOperationInfoGroupQuery(null, request.FilterData, request.IsActive, companyId, request.OrderBy,
            request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoGroupResponse>(response.Error!);
        var values = response.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsOperationInfoGroupModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsOperationInfoGroupResponse(data ?? new List<GetsOperationInfoGroupModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetOperationInfoGroupByNameResponse?>> GetOperationInfoGroupByName(
        GetOperationInfoGroupByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoGroupByName, OperationInfoGroupName:{OperationInfoGroupName}", request.OperationInfoGroupName);

        var isValidRequest = await request.IsValidAsync<GetOperationInfoGroupByNameValidator, GetOperationInfoGroupByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoGroupByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationInfoGroupByNameQuery(request.OperationInfoGroupName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationInfoGroupByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetOperationInfoGroupByNameResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetOperationInfoGroupByCodeResponse?>> GetOperationInfoGroupByCode(
        GetOperationInfoGroupByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetOperationInfoGroupByCode, OperationInfoGroupCode:{OperationInfoGroupCode}", request.OperationInfoGroupCode);

        var isValidRequest = await request.IsValidAsync<GetOperationInfoGroupByCodeValidator, GetOperationInfoGroupByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationInfoGroupByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationInfoGroupByCodeQuery(request.OperationInfoGroupCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationInfoGroupByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetOperationInfoGroupByCodeResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetActiveOperationInfoGroupsResponse?>> GetActiveOperationInfoGroups(
        GetActiveOperationInfoGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveOperationInfoGroups,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.Code, request.Name, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveOperationInfoGroupsValidator, GetActiveOperationInfoGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveOperationInfoGroupsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveOperationInfoGroupsQuery(request.FilterData, request.Code, request.Name, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveOperationInfoGroupsResponse>(response.Error!);
        var values = response.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveOperationInfoGroupModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetActiveOperationInfoGroupsResponse(data ?? new List<GetsActiveOperationInfoGroupModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsOperationInfoGroupExcelExporterResponse?>> GetsOperationInfoGroupExcelExporter(
        GetsOperationInfoGroupExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoGroupExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoGroupExcelExporterValidator, GetsOperationInfoGroupExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoGroupExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsOperationInfoGroupQuery(request.Ids, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsOperationInfoGroupExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsOperationInfoGroupExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(OperationInfoGroupExcels.OperationInfoGroupToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"OperationInfoGroups-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsOperationInfoGroupExcelExporterResponse(file);
    }

    public async Task<Result<GetsOperationInfoGroupExcelEnumResponse?>> GetsOperationInfoGroupExcelEnum(
        GetsOperationInfoGroupExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoGroupExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<OperationInfoGroupExcelEnum>());
        return new GetsOperationInfoGroupExcelEnumResponse(response);
    }
}