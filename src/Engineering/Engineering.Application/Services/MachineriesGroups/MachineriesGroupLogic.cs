using Engineering.Application.Services.MachineriesGroups.Commands.ActiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Commands.CreateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Commands.DisableMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Commands.InactiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Commands.MachineriesGroupCodeCreator;
using Engineering.Application.Services.MachineriesGroups.Commands.StateChangerMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Commands.UpdateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.ActiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.DisableMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.GetActiveMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByCode;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupById;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelEnum;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineryGroupsForRequestMachinery;
using Engineering.Application.Services.MachineriesGroups.Models.InactiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupCodeCreator;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupExcelImports;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupGroupDelete;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupModels;
using Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Queries.FindMachineriesGroupByNamesOrCodes;
using Engineering.Application.Services.MachineriesGroups.Queries.GetActiveMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByCode;
using Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupById;
using Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByName;
using Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByIds;
using Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineryGroupsForRequestMachinery;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.MachineriesGroups;

public class MachineriesGroupLogic : IMachineriesGroupLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<MachineriesGroupLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public MachineriesGroupLogic(
        IMediator mediator,
        ILogger<MachineriesGroupLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateMachineriesGroupResponse?>> CreateMachineriesGroup(
        CreateMachineriesGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for new MachineriesGroup, GroupName:{GroupName}, GroupCode:{GroupCode},", request.GroupName, request.GroupCode);

        var isValidRequest = await request.IsValidAsync<CreateMachineriesGroupValidator, CreateMachineriesGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateMachineriesGroupResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateMachineriesGroupResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetMachineriesGroupByNameQuery(request.GroupName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateMachineriesGroupResponse>(MachineriesGroupErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetMachineriesGroupByCodeQuery(request.GroupCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateMachineriesGroupResponse>(MachineriesGroupErrors.CodeIsDuplicate);

        var response = await _mediator.Send(new CreateMachineriesGroupCommand(request.GroupName, request.GroupCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateMachineriesGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateMachineriesGroupResponse>();
    }

    public async Task<Result<MachineriesGroupExcelImportsResponse?>> MachineriesGroupExcelImports(
        MachineriesGroupExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineriesGroupExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<MachineriesGroupExcelImportsValidator, MachineriesGroupExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineriesGroupExcelImportsResponse>(isValidRequest.Error!);

        var machineriesGroups = ExcelImporter.Import<MachineriesGroupExcelImportsModel>(request.DocumentFile);
        if (machineriesGroups is null)
            return Result.Failure<MachineriesGroupExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (machineriesGroups.Count != machineriesGroups.Select(x => x.MachineriesGroupName).Distinct().Count())
            return Result.Failure<MachineriesGroupExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (machineriesGroups.Count != machineriesGroups.Select(x => x.MachineriesGroupCode).Distinct().Count())
            return Result.Failure<MachineriesGroupExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<MachineriesGroupExcelImportsResponse>(companyResponse.Error!);
        }

        var names = machineriesGroups.Select(x => x.MachineriesGroupName).ToList();
        var codes = machineriesGroups.Select(x => x.MachineriesGroupCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindMachineriesGroupByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<MachineriesGroupExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in machineriesGroups)
        {
            var response = await _mediator.Send(new CreateMachineriesGroupCommand(item.MachineriesGroupName, item.MachineriesGroupCode, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<MachineriesGroupExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineriesGroupExcelImportsResponse(true);
    }

    public async Task<Result<MachineriesGroupCodeCreatorResponse?>> MachineriesGroupCodeCreator(
        MachineriesGroupCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineriesGroupCodeCreator");

        var isValidRequest = await request.IsValidAsync<MachineriesGroupCodeCreatorValidator, MachineriesGroupCodeCreatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineriesGroupCodeCreatorResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<MachineriesGroupCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new MachineriesGroupCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<MachineriesGroupCodeCreatorResponse>(response.Error!);

        return new MachineriesGroupCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerMachineriesGroupsResponse?>> StateChangerMachineriesGroups(
        StateChangerMachineriesGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerMachineriesGroups, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerMachineriesGroupsValidator, StateChangerMachineriesGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerMachineriesGroupsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerMachineriesGroupsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsMachineriesGroupByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerMachineriesGroupsResponse>(responses.Error!);
        var values = responses.Value;

        var response = await _mediator.Send(new StateChangerMachineriesGroupsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerMachineriesGroupsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerMachineriesGroupsResponse(true);
    }

    public async Task<Result<UpdateMachineriesGroupResponse?>> UpdateMachineriesGroup(
        UpdateMachineriesGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateMachineriesGroup, Id:{Id}, GroupName:{GroupName}, GroupCode:{GroupCode}, IsActive:{IsActive}", request.Id, request.GroupName, request.IsActive, request.GroupCode);

        var isValidRequest = await request.IsValidAsync<UpdateMachineriesGroupValidator, UpdateMachineriesGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateMachineriesGroupResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateMachineriesGroupResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetMachineriesGroupByNameQuery(request.GroupName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateMachineriesGroupResponse>(MachineriesGroupErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetMachineriesGroupByCodeQuery(request.GroupCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateMachineriesGroupResponse>(MachineriesGroupErrors.CodeIsDuplicate);

        var response = await _mediator.Send(new UpdateMachineriesGroupCommand(request.Id, request.GroupName, request.GroupCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateMachineriesGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateMachineriesGroupResponse>();
    }

    public async Task<Result<InactiveMachineriesGroupResponse?>> InactiveMachineriesGroup(
        InactiveMachineriesGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveMachineriesGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveMachineriesGroupValidator, InactiveMachineriesGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveMachineriesGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveMachineriesGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveMachineriesGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveMachineriesGroupResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveMachineriesGroupResponse?>> ActiveMachineriesGroup(
        ActiveMachineriesGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveMachineriesGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveMachineriesGroupValidator, ActiveMachineriesGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveMachineriesGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveMachineriesGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveMachineriesGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveMachineriesGroupResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<DisableMachineriesGroupResponse?>> DisableMachineriesGroup(
        DisableMachineriesGroupRequest request, CT ct)
    {
        _logger.LogInformation("Disable MachineriesGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableMachineriesGroupValidator, DisableMachineriesGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableMachineriesGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableMachineriesGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableMachineriesGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableMachineriesGroupResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<MachineriesGroupGroupDeleteResponse?>> MachineriesGroupGroupDelete(
        MachineriesGroupGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineriesGroupGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<MachineriesGroupGroupDeleteValidator, MachineriesGroupGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineriesGroupGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableMachineriesGroupCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<MachineriesGroupGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineriesGroupGroupDeleteResponse(true);
    }

    public async Task<Result<GetMachineriesGroupByIdResponse?>> GetMachineriesGroupById(
        GetMachineriesGroupByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineriesGroupById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetMachineriesGroupByIdValidator, GetMachineriesGroupByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesGroupByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineriesGroupByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesGroupByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineriesGroupByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetMachineriesGroupByNameResponse?>> GetMachineriesGroupByName(
        GetMachineriesGroupByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineriesGroupByName, GroupName:{GroupName}", request.GroupName);

        var isValidRequest = await request.IsValidAsync<GetMachineriesGroupByNameValidator, GetMachineriesGroupByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesGroupByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineriesGroupByNameQuery(request.GroupName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesGroupByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineriesGroupByNameResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetMachineriesGroupByCodeResponse?>> GetMachineriesGroupByCode(
        GetMachineriesGroupByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineriesGroupByCode, GroupCode:{GroupCode}", request.GroupCode);

        var isValidRequest = await request.IsValidAsync<GetMachineriesGroupByCodeValidator, GetMachineriesGroupByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesGroupByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineriesGroupByCodeQuery(request.GroupCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesGroupByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineriesGroupByCodeResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetActiveMachineriesGroupsResponse?>> GetActiveMachineriesGroups(
        GetActiveMachineriesGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveMachineriesGroups,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}", request.Code, request.Name, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveMachineriesGroupsValidator, GetActiveMachineriesGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveMachineriesGroupsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveMachineriesGroupsQuery(request.FilterData, request.Code, request.Name, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveMachineriesGroupsResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveMachineriesGroupModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }
        return new GetActiveMachineriesGroupsResponse(data ?? new List<GetsActiveMachineriesGroupModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetMachineriesGroupsResponse?>> GetMachineriesGroups(
        GetMachineriesGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineriesGroups, Code:{Code} , Name:{Name}, IsActive:{IsActive} , pageSize:{PageSize}, pageSize:{PageSize}",
            request.Code, request.Name, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetMachineriesGroupsValidator, GetMachineriesGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesGroupsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetMachineriesGroupsQuery(null, request.FilterData, request.Code, request.Name, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesGroupsResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetMachineriesGroupsWithChildModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetMachineriesGroupsResponse(data ?? new List<GetMachineriesGroupsWithChildModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsMachineryGroupsForRequestMachineryResponse?>> GetsMachineryGroupsForRequestMachinery(
        GetsMachineryGroupsForRequestMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineryGroupsForRequestMachinery, IsActive:{IsActive} , pageSize:{PageSize}, pageSize:{PageSize}", request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsMachineryGroupsForRequestMachineryValidator, GetsMachineryGroupsForRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMachineryGroupsForRequestMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsMachineryGroupsForRequestMachineryQuery(request.ProjectId, request.ProjectOperationIds, request.ProjectOperationDetailIds, request.FilterData, request.IsActive, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsMachineryGroupsForRequestMachineryResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsMachineryGroupsForRequestMachineryModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsMachineryGroupsForRequestMachineryResponse(data ?? new List<GetsMachineryGroupsForRequestMachineryModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsMachineriesGroupExcelExporterResponse?>> GetsMachineriesGroupExcelExporter(
        GetsMachineriesGroupExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineriesGroupExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsMachineriesGroupExcelExporterValidator, GetsMachineriesGroupExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMachineriesGroupExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetMachineriesGroupsQuery(request.Ids, request.FilterData, request.Code, request.Name, request.IsActive, companyId,
            request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsMachineriesGroupExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsMachineriesGroupExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(MachineriesGroupExcels.MachineriesGroupToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"MachineriesGroups-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsMachineriesGroupExcelExporterResponse(file);
    }

    public async Task<Result<GetsMachineriesGroupExcelEnumResponse?>> GetsMachineriesGroupExcelEnum(
        GetsMachineriesGroupExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineriesGroupExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<MachineriesGroupExcelEnum>());
        return new GetsMachineriesGroupExcelEnumResponse(response);
    }
}