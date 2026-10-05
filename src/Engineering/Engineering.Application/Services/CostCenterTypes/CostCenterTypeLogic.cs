using Engineering.Application.Services.CostCenterTypes.Commands.ActiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Commands.CodeCreator;
using Engineering.Application.Services.CostCenterTypes.Commands.CreateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Commands.DisableCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Commands.InactiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Commands.StateChangerCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Commands.UpdateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.ActiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.CodeCreator;
using Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeExcelImports;
using Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeGroupDelete;
using Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.DisableCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelEnum;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;
using Engineering.Application.Services.CostCenterTypes.Models.InactiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCodeForResponse;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByIdForResponse;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNameForResponse;
using Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNamesOrCodes;
using Engineering.Application.Services.CostCenterTypes.Queries.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterTypeByIds;

namespace Engineering.Application.Services.CostCenterTypes;

public class CostCenterTypeLogic : ICostCenterTypeLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CostCenterTypeLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public CostCenterTypeLogic(
        IMediator mediator,
        ILogger<CostCenterTypeLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateCostCenterTypeResponse?>> CreateCostCenterType(
        CreateCostCenterTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateCostCenterType, CostCenterTypeName:{CostCenterTypeName}, CostCenterTypeCode:{CostCenterTypeCode},", request.CostCenterTypeName, request.CostCenterTypeCode);

        var isValidRequest = await request.IsValidAsync<CreateCostCenterTypeValidator, CreateCostCenterTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCostCenterTypeResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateCostCenterTypeResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateCostCenterTypeResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCostCenterTypeByNameQuery(
            request.CostCenterTypeName,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCostCenterTypeResponse>(CostCenterErrors.TypeNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetCostCenterTypeByCodeQuery(
            request.CostCenterTypeCode,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCostCenterTypeResponse>(CostCenterErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new CreateCostCenterTypeCommand(
            request.CostCenterTypeName,
            request.CostCenterTypeCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateCostCenterTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateCostCenterTypeResponse>();
    }

    public async Task<Result<CostCenterTypeExcelImportsResponse?>> CostCenterTypeExcelImports(
        CostCenterTypeExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for CostCenterTypeExcelImports");

        var isValidRequest = await request.IsValidAsync<CostCenterTypeExcelImportsValidator, CostCenterTypeExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CostCenterTypeExcelImportsResponse>(isValidRequest.Error!);

        var CostCenterTypes = ExcelImporter.Import<CostCenterTypeExcelImportsModel>(request.DocumentFile);
        if (CostCenterTypes is null)
            return Result.Failure<CostCenterTypeExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (CostCenterTypes.Count != CostCenterTypes.Select(s => s.CostCenterTypeName).Distinct().Count())
            return Result.Failure<CostCenterTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (CostCenterTypes.Count != CostCenterTypes.Select(s => s.CostCenterTypeCode).Distinct().Count())
            return Result.Failure<CostCenterTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CostCenterTypeExcelImportsResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CostCenterTypeExcelImportsResponse>(companyResponse.Error!);
        }

        var names = CostCenterTypes.Select(s => s.CostCenterTypeName).ToList();
        var codes = CostCenterTypes.Select(s => s.CostCenterTypeCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new GetCostCenterTypeByNamesOrCodesQuery(
            names,
            codes,
            companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<CostCenterTypeExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in CostCenterTypes)
        {
            var response = await _mediator.Send(new CreateCostCenterTypeCommand(
                item.CostCenterTypeName,
                item.CostCenterTypeCode,
                item.IsActive,
                companyId), ct);
            if (response.IsFailure)
                return Result.Failure<CostCenterTypeExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CostCenterTypeExcelImportsResponse(true);
    }

    public async Task<Result<CostCenterTypeCodeCreatorResponse?>> CostCenterTypeCodeCreator(
        CostCenterTypeCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CostCenterTypeCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CostCenterTypeCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CostCenterTypeCodeCreatorResponse>(response.Error!);

        return new CostCenterTypeCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<StateChangerCostCenterTypesResponse?>> StateChangerCostCenterTypes(
        StateChangerCostCenterTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerCostCenterTypes, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerCostCenterTypesValidator, StateChangerCostCenterTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerCostCenterTypesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerCostCenterTypesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsCostCenterTypeByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerCostCenterTypesResponse>(responses.Error!);

        var response = await _mediator.Send(new StateChangerCostCenterTypesCommand(
                    responses.Value,
                    request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerCostCenterTypesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerCostCenterTypesResponse(true);
    }

    public async Task<Result<UpdateCostCenterTypeResponse?>> UpdateCostCenterType(
        UpdateCostCenterTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateCostCenterType, Id:{Id}, CostCenterTypeName:{CostCenterTypeName}, CostCenterTypeCode:{CostCenterTypeCode}", request.Id, request.CostCenterTypeName, request.CostCenterTypeCode);

        var isValidRequest = await request.IsValidAsync<UpdateCostCenterTypeValidator, UpdateCostCenterTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCostCenterTypeResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateCostCenterTypeResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateCostCenterTypeResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCostCenterTypeByNameQuery(
            request.CostCenterTypeName,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCostCenterTypeResponse>(CostCenterErrors.TypeNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetCostCenterTypeByCodeQuery(
            request.CostCenterTypeCode,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCostCenterTypeResponse>(CostCenterErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new UpdateCostCenterTypeCommand(
            request.Id,
            request.CostCenterTypeName,
            request.CostCenterTypeCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateCostCenterTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateCostCenterTypeResponse>();
    }

    public async Task<Result<InactiveCostCenterTypeResponse?>> InactiveCostCenterType(
        InactiveCostCenterTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveCostCenterType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveCostCenterTypeValidator, InactiveCostCenterTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveCostCenterTypeResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCostCenterTypeByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<InactiveCostCenterTypeResponse>(responseGet.Error!);

        var response = await _mediator.Send(new InactiveCostCenterTypeCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveCostCenterTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveCostCenterTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveCostCenterTypeResponse?>> ActiveCostCenterType(
        ActiveCostCenterTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveCostCenterType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveCostCenterTypeValidator, ActiveCostCenterTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveCostCenterTypeResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCostCenterTypeByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<ActiveCostCenterTypeResponse>(responseGet.Error!);

        var response = await _mediator.Send(new ActiveCostCenterTypeCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveCostCenterTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveCostCenterTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<DisableCostCenterTypeResponse?>> DisableCostCenterType(
        DisableCostCenterTypeRequest request, CT ct)
    {
        _logger.LogInformation("Disable CostCenterType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableCostCenterTypeValidator, DisableCostCenterTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableCostCenterTypeResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetCostCenterTypeByIdQuery(request.Id), ct);
        if (responseGet.IsFailure)
            return Result.Failure<DisableCostCenterTypeResponse>(responseGet.Error!);

        var response = await _mediator.Send(new DisableCostCenterTypeCommand(responseGet.Value!), ct);
        if (response.IsFailure)
            return Result.Failure<DisableCostCenterTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableCostCenterTypeResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<CostCenterTypeGroupDeleteResponse?>> CostCenterTypeGroupDelete(
        CostCenterTypeGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for CostCenterTypeGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<CostCenterTypeGroupDeleteValidator, CostCenterTypeGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CostCenterTypeGroupDeleteResponse>(isValidRequest.Error!);

        var responseGet = await _mediator.Send(new GetsCostCenterTypeByIdsQuery(request.Ids), ct);
        if (responseGet.IsFailure)
            return Result.Failure<CostCenterTypeGroupDeleteResponse>(responseGet.Error!);

        foreach (var item in responseGet.Value!)
        {
            var response = await _mediator.Send(new DisableCostCenterTypeCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<CostCenterTypeGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CostCenterTypeGroupDeleteResponse(true);
    }

    public async Task<Result<GetCostCenterTypeByIdResponse?>> GetCostCenterTypeById(
        GetCostCenterTypeByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterTypeById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetCostCenterTypeByIdValidator, GetCostCenterTypeByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterTypeByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterTypeByIdForResponseQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterTypeByIdResponse>(response.Error!);

        return response.Value;
    }

    public async Task<Result<GetsCostCenterTypeResponse?>> GetsCostCenterType(
        GetsCostCenterTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterType, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterTypeValidator, GetsCostCenterTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterTypeResponse>(isValidRequest.Error!);


        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCostCenterTypeResponse>(GlobalErrors.InvalidCompany);

        var response = await _mediator.Send(new GetsCostCenterTypeQuery(
            null,
            request.FilterData,
            request.IsActive,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsCostCenterTypeResponse>(response.Error!);

        var values = response.Value!.Data!;
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

        return new GetsCostCenterTypeResponse(response.Value.Data ?? new List<GetsCostCenterTypeModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetCostCenterTypeByNameResponse?>> GetCostCenterTypeByName(
        GetCostCenterTypeByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterTypeByName, CostCenterTypeName:{CostCenterTypeName}", request.CostCenterTypeName);

        var isValidRequest = await request.IsValidAsync<GetCostCenterTypeByNameValidator, GetCostCenterTypeByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterTypeByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterTypeByNameForResponseQuery(request.CostCenterTypeName), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterTypeByNameResponse>(response.Error!);

        return response.Value;
    }

    public async Task<Result<GetCostCenterTypeByCodeResponse?>> GetCostCenterTypeByCode(
        GetCostCenterTypeByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterTypeByCode, CostCenterTypeCode:{CostCenterTypeCode}", request.CostCenterTypeCode);

        var isValidRequest = await request.IsValidAsync<GetCostCenterTypeByCodeValidator, GetCostCenterTypeByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterTypeByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterTypeByCodeForResponseQuery(request.CostCenterTypeCode), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterTypeByCodeResponse>(response.Error!);

        return response.Value;
    }

    public async Task<Result<GetsActiveCostCenterTypesResponse?>> GetsActiveCostCenterTypes(
        GetsActiveCostCenterTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveCostCenterTypes,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}", request.Code, request.Name, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveCostCenterTypesValidator, GetsActiveCostCenterTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveCostCenterTypesResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsActiveCostCenterTypesQuery(
            request.FilterData,
            request.Code,
            request.Name,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveCostCenterTypesResponse>(response.Error!);

        return new GetsActiveCostCenterTypesResponse(response.Value!.Data ?? new List<GetsActiveCostCenterTypesModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterTypeExcelExporterResponse?>> GetsCostCenterTypeExcelExporter(
        GetsCostCenterTypeExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterTypeExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterTypeExcelExporterValidator, GetsCostCenterTypeExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterTypeExcelExporterResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCostCenterTypeExcelExporterResponse>(GlobalErrors.InvalidCompany);

        var responses = await _mediator.Send(new GetsCostCenterTypeQuery(
            request.Ids,
            request.FilterData,
            request.IsActive,
            request.OrderBy,
            companyId,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsCostCenterTypeExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(w =>
                                    w.CompanyId != null &&
                                    w.CompanyId > 0)
                                .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsCostCenterTypeExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(CostCenterTypeExcels.CostCenterTypeToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CostCenterTypes-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsCostCenterTypeExcelExporterResponse(file);
    }

    public async Task<Result<GetsCostCenterTypeExcelEnumResponse?>> GetsCostCenterTypeExcelEnum(
        GetsCostCenterTypeExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterTypeExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<CostCenterTypeExcelEnum>());
        return new GetsCostCenterTypeExcelEnumResponse(response);
    }
}