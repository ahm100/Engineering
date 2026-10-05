using Engineering.Application.Services.Machineries.Commands.ActiveMachinery;
using Engineering.Application.Services.Machineries.Commands.CreateMachinery;
using Engineering.Application.Services.Machineries.Commands.DisableMachinery;
using Engineering.Application.Services.Machineries.Commands.InactiveMachinery;
using Engineering.Application.Services.Machineries.Commands.MachineryCodeCreator;
using Engineering.Application.Services.Machineries.Commands.StateChangerMachineries;
using Engineering.Application.Services.Machineries.Commands.UpdateMachinery;
using Engineering.Application.Services.Machineries.Models.ActiveMachinery;
using Engineering.Application.Services.Machineries.Models.CreateMachinery;
using Engineering.Application.Services.Machineries.Models.DisableMachinery;
using Engineering.Application.Services.Machineries.Models.GetActiveMachineries;
using Engineering.Application.Services.Machineries.Models.GetMachineries;
using Engineering.Application.Services.Machineries.Models.GetMachineryByCode;
using Engineering.Application.Services.Machineries.Models.GetMachineryById;
using Engineering.Application.Services.Machineries.Models.GetMachineryByName;
using Engineering.Application.Services.Machineries.Models.GetsByMachineriesGroupId;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelEnum;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelExporter;
using Engineering.Application.Services.Machineries.Models.GetsMachineryForRequestMachinery;
using Engineering.Application.Services.Machineries.Models.InactiveMachinery;
using Engineering.Application.Services.Machineries.Models.MachineryCodeCreator;
using Engineering.Application.Services.Machineries.Models.MachineryExcelImports;
using Engineering.Application.Services.Machineries.Models.MachineryGroupDelete;
using Engineering.Application.Services.Machineries.Models.MachineryModels;
using Engineering.Application.Services.Machineries.Models.StateChangerMachineries;
using Engineering.Application.Services.Machineries.Models.UpdateMachinery;
using Engineering.Application.Services.Machineries.Queries.FindMachineryByNamesOrCodes;
using Engineering.Application.Services.Machineries.Queries.GetActiveMachineries;
using Engineering.Application.Services.Machineries.Queries.GetMachineries;
using Engineering.Application.Services.Machineries.Queries.GetMachineryByCode;
using Engineering.Application.Services.Machineries.Queries.GetMachineryById;
using Engineering.Application.Services.Machineries.Queries.GetMachineryByName;
using Engineering.Application.Services.Machineries.Queries.GetsByMachineriesGroupId;
using Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;
using Engineering.Application.Services.Machineries.Queries.GetsMachineryForRequestMachinery;
using Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupById;
using Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByCodes;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.Machineries;

public class MachineryLogic : IMachineryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<MachineryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public MachineryLogic(
        IMediator mediator,
        ILogger<MachineryLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateMachineryResponse?>> CreateMachinery(
        CreateMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateMachinery, MachineryName:{MachineryName}, MachineryCode:{MachineryCode},", request.MachineryName, request.MachineryCode);

        var isValidRequest = await request.IsValidAsync<CreateMachineryValidator, CreateMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateMachineryResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetMachineryByNameQuery(request.MachineryName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateMachineryResponse>(MachineryErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetMachineryByCodeQuery(request.MachineryCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateMachineryResponse>(MachineryErrors.CodeIsDuplicate);

        var machineriesGroup = await _mediator.Send(new GetMachineriesGroupByIdQuery(request.MachineriesGroupId), ct);
        if (machineriesGroup.IsFailure)
            return Result.Failure<CreateMachineryResponse>(MachineryErrors.MachineryMachineriesGroupNotFound);

        var response = await _mediator.Send(new CreateMachineryCommand(machineriesGroup.Value!, request.MachineryCode, request.MachineryName, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateMachineryResponse>();
    }

    public async Task<Result<MachineryExcelImportsResponse?>> MachineryExcelImports(
        MachineryExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<MachineryExcelImportsValidator, MachineryExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineryExcelImportsResponse>(isValidRequest.Error!);

        var machineries = ExcelImporter.Import<MachineryExcelImportsModel>(request.DocumentFile);
        if (machineries is null)
            return Result.Failure<MachineryExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (machineries.Count != machineries.Select(x => x.MachineryName).Distinct().Count())
            return Result.Failure<MachineryExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (machineries.Count != machineries.Select(x => x.MachineryCode).Distinct().Count())
            return Result.Failure<MachineryExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<MachineryExcelImportsResponse>(companyResponse.Error!);
        }

        var names = machineries.Select(x => x.MachineryName).ToList();
        var codes = machineries.Select(x => x.MachineryCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindMachineryByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<MachineryExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        var groupCodes = machineries.Select(x => x.GroupCode).Distinct().ToList();
        var groupResponse = await _mediator.Send(new GetsMachineriesGroupByCodesQuery(groupCodes, companyId), ct);
        if (groupResponse.IsFailure)
            return Result.Failure<MachineryExcelImportsResponse>(MachineryErrors.MachineryGroupNotFoundWithCode);
        var groups = groupResponse.Value!.Data;
        if (groupCodes.Count != groups!.Count)
            return Result.Failure<MachineryExcelImportsResponse>(MachineryErrors.MachineryGroupNotFoundWithCode);

        foreach (var item in machineries)
        {
            var group = groups.Where(x => x.GroupCode == item.GroupCode).FirstOrDefault();
            if (group is null)
                return Result.Failure<MachineryExcelImportsResponse>(MachineryErrors.MachineryGroupNotFoundWithCode);

            var response = await _mediator.Send(new CreateMachineryCommand(group!, item.MachineryName, item.MachineryCode, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<MachineryExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineryExcelImportsResponse(true);
    }

    public async Task<Result<MachineryCodeCreatorResponse?>> MachineryCodeCreator(
        MachineryCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<MachineryCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new MachineryCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<MachineryCodeCreatorResponse>(response.Error!);

        return new MachineryCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<DisableMachineryResponse?>> DisableMachinery(
        DisableMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableMachineryValidator, DisableMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableMachineryResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<MachineryGroupDeleteResponse?>> MachineryGroupDelete(
        MachineryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<MachineryGroupDeleteValidator, MachineryGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineryGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableMachineryCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<MachineryGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineryGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateMachineryResponse?>> UpdateMachinery(
        UpdateMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateMachinery, id:{Id}, Machinery:{MachineryName} , description:{MachineryCode},", request.Id, request.MachineryName, request.MachineryCode);

        var isValidRequest = await request.IsValidAsync<UpdateMachineryValidator, UpdateMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateMachineryResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetMachineryByNameQuery(request.MachineryName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateMachineryResponse>(MachineryErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetMachineryByCodeQuery(request.MachineryCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateMachineryResponse>(MachineryErrors.CodeIsDuplicate);

        var machineriesGroup = await _mediator.Send(new GetMachineriesGroupByIdQuery(request.MachineriesGroupId), ct);
        if (machineriesGroup.IsFailure)
            return Result.Failure<UpdateMachineryResponse>(MachineryErrors.MachineryMachineriesGroupNotFound);

        var response = await _mediator.Send(new UpdateMachineryCommand(request.Id, machineriesGroup.Value!, request.MachineryName, request.MachineryCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateMachineryResponse>();
    }

    public async Task<Result<InactiveMachineryResponse?>> InactiveMachinery(
        InactiveMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveMachineryValidator, InactiveMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveMachineryResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveMachineryResponse?>> ActiveMachinery(
        ActiveMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveMachinery, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveMachineryValidator, ActiveMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveMachineryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveMachineryCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveMachineryResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveMachineryResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerMachineriesResponse?>> StateChangerMachineries(
        StateChangerMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerMachineries, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerMachineriesValidator, StateChangerMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerMachineriesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerMachineriesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsMachineryByIdsQuery(request.Ids, 1, request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerMachineriesResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerMachineriesCommand(values!, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerMachineriesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerMachineriesResponse(true);
    }

    public async Task<Result<GetMachineryByIdResponse?>> GetMachineryById(
        GetMachineryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineryById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetMachineryByIdValidator, GetMachineryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineryByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineryByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineryByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineryByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetMachineryByNameResponse?>> GetMachineryByName(
        GetMachineryByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineryByName MachineryName:{MachineryName}", request.MachineryName);

        var isValidRequest = await request.IsValidAsync<GetMachineryByNameValidator, GetMachineryByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineryByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineryByNameQuery(request.MachineryName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineryByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineryByNameResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetMachineryByCodeResponse?>> GetMachineryByCode(
        GetMachineryByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineryByCode, MachineryCode:{MachineryCode}", request.MachineryCode);

        var isValidRequest = await request.IsValidAsync<GetMachineryByCodeValidator, GetMachineryByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineryByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineryByCodeQuery(request.MachineryCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineryByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineryByCodeResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetActiveMachineriesResponse?>> GetsActiveMachinery(
        GetActiveMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveMachinery, MachineryCode:{MachineryCode} , MachineryName:{MachineryName}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.MachineryCode, request.MachineryName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveMachineriesValidator, GetActiveMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveMachineriesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveMachineriesQuery(request.FilterData, request.MachineriesGroupId,
            request.MachineryCode, request.MachineryName, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveMachineriesResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveMachineryModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetActiveMachineriesResponse(data ?? new List<GetsActiveMachineryModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetMachineriesResponse?>> GetsMachinery(
        GetMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachinery,MachineryName:{MachineryName} , MachineryCode:{MachineryCode}, IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.MachineryName, request.MachineryCode, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetMachineriesValidator, GetMachineriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineriesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetMachineriesQuery(null, request.FilterData, request.MachineriesGroupId, request.MachineryName, request.MachineryCode, request.IsActive,
             companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineriesResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetMachineriesModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetMachineriesResponse(data ?? new List<GetMachineriesModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByMachineriesGroupIdResponse?>> GetsByMachineriesGroupId(
        GetsByMachineriesGroupIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByMachineriesGroupId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByMachineriesGroupIdValidator, GetsByMachineriesGroupIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByMachineriesGroupIdResponse>(isValidRequest.Error!);

        var machineriesGroup = await _mediator.Send(new GetMachineriesGroupByIdQuery(request.MachineriesGroupId), ct);
        if (machineriesGroup.IsFailure)
            return Result.Failure<GetsByMachineriesGroupIdResponse>(machineriesGroup.Error!);

        var response = await _mediator.Send(new GetsByMachineriesGroupIdQuery(request.MachineriesGroupId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByMachineriesGroupIdResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsByMachineriesGroupIdModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsByMachineriesGroupIdResponse(data ?? new List<GetsByMachineriesGroupIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsMachineryForRequestMachineryResponse?>> GetsMachineryForRequestMachinery(
        GetsMachineryForRequestMachineryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineryForRequestMachinery, IsActive:{IsActive} , pageSize:{PageSize}, pageSize:{PageSize}", request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsMachineryForRequestMachineryValidator, GetsMachineryForRequestMachineryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMachineryForRequestMachineryResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsMachineryForRequestMachineryQuery(request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, request.MachineriesGroupId,
            request.FilterData, request.IsActive, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsMachineryForRequestMachineryResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsMachineryForRequestMachineryModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsMachineryForRequestMachineryResponse(data ?? new List<GetsMachineryForRequestMachineryModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsMachineryExcelExporterResponse?>> GetsMachineryExcelExporter(
        GetsMachineryExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineryExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsMachineryExcelExporterValidator, GetsMachineryExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMachineryExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetMachineriesQuery(request.Ids, request.FilterData, request.MachineriesGroupId, request.MachineryName, request.MachineryCode, request.IsActive,
                companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsMachineryExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsMachineryExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(MachineryExcels.MachineryToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Machinerys-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsMachineryExcelExporterResponse(file);
    }

    public async Task<Result<GetsMachineryExcelEnumResponse?>> GetsMachineryExcelEnum(
        GetsMachineryExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineryExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<MachineryExcelEnum>());
        return new GetsMachineryExcelEnumResponse(response);
    }
}