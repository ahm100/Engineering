using Engineering.Application.Services.CostOvers.Commands.ActiveCostOver;
using Engineering.Application.Services.CostOvers.Commands.CodeCreator;
using Engineering.Application.Services.CostOvers.Commands.CreateCostOver;
using Engineering.Application.Services.CostOvers.Commands.DisableCostOver;
using Engineering.Application.Services.CostOvers.Commands.InactiveCostOver;
using Engineering.Application.Services.CostOvers.Commands.StateChangerCostOvers;
using Engineering.Application.Services.CostOvers.Commands.UpdateCostOver;
using Engineering.Application.Services.CostOvers.Models.ActiveCostOver;
using Engineering.Application.Services.CostOvers.Models.CodeCreator;
using Engineering.Application.Services.CostOvers.Models.CostOverExcelImports;
using Engineering.Application.Services.CostOvers.Models.CostOverGroupDelete;
using Engineering.Application.Services.CostOvers.Models.CreateCostOver;
using Engineering.Application.Services.CostOvers.Models.DisableCostOver;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;
using Engineering.Application.Services.CostOvers.Models.GetCostOverById;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelEnum;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelExporter;
using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;
using Engineering.Application.Services.CostOvers.Models.InactiveCostOver;
using Engineering.Application.Services.CostOvers.Models.StateChangerCostOvers;
using Engineering.Application.Services.CostOvers.Models.UpdateCostOver;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverByCode;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverByCodeForResponse;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverById;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverByIdForResponse;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Queries.GetCostOverByNameForResponse;
using Engineering.Application.Services.CostOvers.Queries.GetsActiveCostOvers;
using Engineering.Application.Services.CostOvers.Queries.GetsCostOverByIds;
using Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNameOrCode;
using Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNamesOrCodes;
using Engineering.Application.Services.CostOvers.Queries.GetsCostOvers;
using IdentityServer.ClientSdk.Services;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.CostOvers;

public partial class CostOverLogic : ICostOverLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CostOverLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserInfoProvider _userInfoProvider;
    public CostOverLogic(
        IMediator mediator,
        ILogger<CostOverLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IUserInfoProvider userInfoProvider)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateCostOverResponse?>> CreateCostOver(
        CreateCostOverRequest request, CT ct)
    {
        _logger.LogInformation("Request for new CostOver, CostOverName:{CostOverName}, CostOverCode:{CostOverCode},", request.CostOverName, request.CostOverCode);

        var isValidRequest = await request.IsValidAsync<CreateCostOverValidator, CreateCostOverRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCostOverResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateCostOverResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateCostOverResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCostOverByNameQuery(
            request.CostOverName,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCostOverResponse>(CostOverErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetCostOverByCodeQuery(
            request.CostOverCode,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCostOverResponse>(CostOverErrors.CodeIsDuplicate);

        var response = await _mediator.Send(new CreateCostOverCommand(
            request.CostOverName,
            request.CostOverCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateCostOverResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateCostOverResponse(
            response.Value!.Id,
            response.Value!.CostOverCode,
            response.Value!.CostOverName,
            response.Value!.IsActive);
    }

    public async Task<Result<CostOverExcelImportsResponse?>> CostOverExcelImports(
        CostOverExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for CostOverExcelImports");

        var isValidRequest = await request.IsValidAsync<CostOverExcelImportsValidator, CostOverExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CostOverExcelImportsResponse>(isValidRequest.Error!);

        var costOvers = ExcelImporter.Import<CostOverExcelImportsModel>(request.DocumentFile);
        if (costOvers is null)
            return Result.Failure<CostOverExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (costOvers.Count != costOvers.Select(s => s.CostOverName).Distinct().Count())
            return Result.Failure<CostOverExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (costOvers.Count != costOvers.Select(s => s.CostOverCode).Distinct().Count())
            return Result.Failure<CostOverExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CostOverExcelImportsResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CostOverExcelImportsResponse>(companyResponse.Error!);
        }

        var names = costOvers.Select(s => s.CostOverName).ToList();
        var codes = costOvers.Select(s => s.CostOverCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new GetsCostOverByNamesOrCodesQuery(
            names,
            codes,
            companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<CostOverExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in costOvers)
        {
            var response = await _mediator.Send(new CreateCostOverCommand(
                item.CostOverName,
                item.CostOverCode,
                item.IsActive,
                companyId), ct);
            if (response.IsFailure)
                return Result.Failure<CostOverExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CostOverExcelImportsResponse(true);
    }

    public async Task<Result<CostOverCodeCreatorResponse?>> CostOverCodeCreator(
        CostOverCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CostOverCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CostOverCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CostOverCodeCreatorResponse>(response.Error!);

        return new CostOverCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<UpdateCostOverResponse?>> UpdateCostOver(
        UpdateCostOverRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateCostOver, Id:{Id}, CostOverName:{CostOverName}, CostOverCode:{CostOverCode}, IsActive:{IsActive}", request.Id, request.CostOverName, request.IsActive, request.CostOverCode);

        var isValidRequest = await request.IsValidAsync<UpdateCostOverValidator, UpdateCostOverRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCostOverResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateCostOverResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateCostOverResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCostOverByNameQuery(
            request.CostOverName,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCostOverResponse>(CostOverErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetCostOverByCodeQuery(
            request.CostOverCode,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCostOverResponse>(CostOverErrors.CodeIsDuplicate);

        var response = await _mediator.Send(new UpdateCostOverCommand(
            request.Id,
            request.CostOverName,
            request.CostOverCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateCostOverResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        var value = response.Value!;
        return new UpdateCostOverResponse(value.Id, value.CostOverCode, value.CostOverName, request.IsActive);
    }

    public async Task<Result<InactiveCostOverResponse?>> InactiveCostOver(
        InactiveCostOverRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveCostOver, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveCostOverValidator, InactiveCostOverRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveCostOverResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCostOverByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<InactiveCostOverResponse>(responseGet.Error!);

        var response = await _mediator.Send(new InactiveCostOverCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveCostOverResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveCostOverResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveCostOverResponse?>> ActiveCostOver(
        ActiveCostOverRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveCostOver, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveCostOverValidator, ActiveCostOverRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveCostOverResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCostOverByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<ActiveCostOverResponse>(responseGet.Error!);

        var response = await _mediator.Send(new ActiveCostOverCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveCostOverResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveCostOverResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerCostOversResponse?>> StateChangerCostOvers(
        StateChangerCostOversRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerCostOvers, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerCostOversValidator, StateChangerCostOversRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerCostOversResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerCostOversResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsCostOverByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerCostOversResponse>(responses.Error!);

        var response = await _mediator.Send(new StateChangerCostOversCommand(
            responses.Value,
            request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerCostOversResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerCostOversResponse(true);
    }

    public async Task<Result<DisableCostOverResponse?>> DisableCostOver(
        DisableCostOverRequest request, CT ct)
    {
        _logger.LogInformation("Disable CostOver, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableCostOverValidator, DisableCostOverRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableCostOverResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCostOverByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<DisableCostOverResponse>(responseGet.Error!);

        var response = await _mediator.Send(new DisableCostOverCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<DisableCostOverResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableCostOverResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<CostOverGroupDeleteResponse?>> CostOverGroupDelete(
        CostOverGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for CostOverGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<CostOverGroupDeleteValidator, CostOverGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CostOverGroupDeleteResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetsCostOverByIdsQuery(request.Ids), ct);
        if (responseGet.IsFailure)
            return Result.Failure<CostOverGroupDeleteResponse>(responseGet.Error!);

        foreach (var item in responseGet.Value!)
        {
            var response = await _mediator.Send(new DisableCostOverCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<CostOverGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CostOverGroupDeleteResponse(true);
    }

    public async Task<Result<GetCostOverByIdResponse?>> GetCostOverById(
        GetCostOverByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostOverById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetCostOverByIdValidator, GetCostOverByIdRequest>(ct);
        if (isValidRequest.IsFailure)   
            return Result.Failure<GetCostOverByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostOverByIdForResponseQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostOverByIdResponse>(response.Error!);

        Company? company = null;
        if (response.Value!.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(response.Value.CompanyId, _mediator, ct);

        response.Value.CompanyNameFa = company?.NameFa;
        return response.Value;
    }

    public async Task<Result<GetCostOverByNameResponse?>> GetCostOverByName(
        GetCostOverByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostOverByName, CostOverName:{CostOverName}", request.CostOverName);

        var isValidRequest = await request.IsValidAsync<GetCostOverByNameValidator, GetCostOverByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostOverByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostOverByNameForResponseQuery(request.CostOverName), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostOverByNameResponse>(response.Error!);

        Company? company = null;
        if (response.Value!.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(response.Value.CompanyId, _mediator, ct);

        response.Value.CompanyNameFa = company?.NameFa;
        return response;
    }

    public async Task<Result<GetCostOverByCodeResponse?>> GetCostOverByCode(
        GetCostOverByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostOverByCode, CostOverCode:{CostOverCode}", request.CostOverCode);

        var isValidRequest = await request.IsValidAsync<GetCostOverByCodeValidator, GetCostOverByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostOverByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostOverByCodeForResponseQuery(request.CostOverCode), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostOverByCodeResponse>(response.Error!);

        Company? company = null;
        if (response.Value!.CompanyId is not null && response.Value!.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(response.Value.CompanyId, _mediator, ct);

        response.Value.CompanyNameFa = company?.NameFa;
        return response.Value;
    }

    public async Task<Result<GetsActiveCostOversResponse?>> GetsActiveCostOvers(
        GetsActiveCostOversRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveCostOvers,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}", request.Code, request.Name, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GestActiveCostOversValidator, GetsActiveCostOversRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveCostOversResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsActiveCostOversQuery(
            request.FilterData,
            request.Code,
            request.Name,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveCostOversResponse>(response.Error!);

        return new GetsActiveCostOversResponse(response.Value!.Data ?? new List<GetsActiveCostOversModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostOversResponse?>> GetsCostOvers(
        GetsCostOversRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostOvers, Code:{Code} , Name:{Name}, IsActive:{IsActive} , pageSize:{PageSize}, pageSize:{PageSize}", request.Code, request.Name, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostOversValidator, GetsCostOversRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostOversResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCostOversResponse>(GlobalErrors.InvalidCompany);

        var response = await _mediator.Send(new GetsCostOversQuery(
            null,
            request.FilterData,
            request.Code,
            request.Name,
            request.IsActive,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsCostOversResponse>(response.Error!);

        var values = response.Value!.Data;
        var companyIds = values?.Where(w => w.CompanyId != null && w.CompanyId > 0).Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        foreach (var item in response.Value.Data!)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsCostOversResponse(response.Value.Data ?? new List<GetsCostOversModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostOverByNameOrCodeResponse?>> GetsCostOverByNameOrCode(
        GetsCostOverByNameOrCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostOverByNameOrCode, FilterData:{FilterData} , pageSize:{PageSize}, pageSize:{PageSize}", request.FilterData, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostOverByNameOrCodeValidator, GetsCostOverByNameOrCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostOverByNameOrCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsCostOverByNameOrCodeQuery(
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsCostOverByNameOrCodeResponse>(response.Error!);

        return new GetsCostOverByNameOrCodeResponse(response.Value.Data ?? new List<GetsCostOverByNameOrCodeModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostOverExcelExporterResponse?>> GetsCostOverExcelExporter(
        GetsCostOverExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostOverExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostOverExcelExporterValidator, GetsCostOverExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostOverExcelExporterResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCostOverExcelExporterResponse>(GlobalErrors.InvalidCompany);

        var responses = await _mediator.Send(new GetsCostOversQuery(
            request.Ids,
            request.FilterData,
            request.CostOverCode,
            request.CostOverName,
            request.IsActive,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsCostOverExcelExporterResponse>(responses.Error!);

        var values = responses.Value.Data;
        var companyIds = values?.Where(w => w.CompanyId != null && w.CompanyId > 0).Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);
        var data = values.Adapt<List<GetsCostOverExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(CostOverExcels.CostOverToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CostOvers-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsCostOverExcelExporterResponse(file);
    }

    public async Task<Result<GetsCostOverExcelEnumResponse?>> GetsCostOverExcelEnum(
        GetsCostOverExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostOverExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<CostOverExcelEnum>());
        return new GetsCostOverExcelEnumResponse(response);
    }
}
