using Engineering.Application.Services.CabinTypes.Commands.ActiveCabinType;
using Engineering.Application.Services.CabinTypes.Commands.CabinTypeCodeCreator;
using Engineering.Application.Services.CabinTypes.Commands.CreateCabinType;
using Engineering.Application.Services.CabinTypes.Commands.DeleteCabinType;
using Engineering.Application.Services.CabinTypes.Commands.InactiveCabinType;
using Engineering.Application.Services.CabinTypes.Commands.StateChangerCabinTypes;
using Engineering.Application.Services.CabinTypes.Commands.UpdateCabinType;
using Engineering.Application.Services.CabinTypes.Models.ActiveCabinType;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeCodeCreator;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeExcelImports;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeGroupDelete;
using Engineering.Application.Services.CabinTypes.Models.CreateCabinType;
using Engineering.Application.Services.CabinTypes.Models.DeleteCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;
using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelEnum;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelExporter;
using Engineering.Application.Services.CabinTypes.Models.InactiveCabinType;
using Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;
using Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeById;
using Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByName;
using Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByNamesOrCodes;
using Engineering.Application.Services.CabinTypes.Queries.GetsActiveCabinTypes;
using Engineering.Application.Services.CabinTypes.Queries.GetsCabinType;
using Engineering.Application.Services.CabinTypes.Queries.GetsCabinTypeByIds;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.CabinTypes;

public class CabinTypeLogic : ICabinTypeLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CabinTypeLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public CabinTypeLogic(
        IMediator mediator,
        ILogger<CabinTypeLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateCabinTypeResponse?>> CreateCabinType(
        CreateCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateCabinType, CabinTypeName:{CabinTypeName}, CabinTypeCode:{CabinTypeCode},", request.CabinTypeName, request.CabinTypeCode);

        var isValidRequest = await request.IsValidAsync<CreateCabinTypeValidator, CreateCabinTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCabinTypeResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateCabinTypeResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateCabinTypeResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCabinTypeByNameQuery(
            request.CabinTypeName,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCabinTypeResponse>(CabinTypeErrors.TypeNameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetCabinTypeByCodeQuery(
            request.CabinTypeCode,
            companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateCabinTypeResponse>(CabinTypeErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new CreateCabinTypeCommand(
            request.CabinTypeName,
            request.CabinTypeCode,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateCabinTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!;
    }

    public async Task<Result<CabinTypeExcelImportsResponse?>> CabinTypeExcelImports(
        CabinTypeExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for CabinTypeExcelImports");

        var isValidRequest = await request.IsValidAsync<CabinTypeExcelImportsValidator, CabinTypeExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CabinTypeExcelImportsResponse>(isValidRequest.Error!);

        var cabinTypes = ExcelImporter.Import<CabinTypeExcelImportsModel>(request.DocumentFile);
        if (cabinTypes is null)
            return Result.Failure<CabinTypeExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (cabinTypes.Count != cabinTypes.Select(s => s.CabinTypeName).Distinct().Count())
            return Result.Failure<CabinTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (cabinTypes.Count != cabinTypes.Select(s => s.CabinTypeCode).Distinct().Count())
            return Result.Failure<CabinTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CabinTypeExcelImportsResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CabinTypeExcelImportsResponse>(companyResponse.Error!);
        }

        var names = cabinTypes.Select(s => s.CabinTypeName).ToList();
        var codes = cabinTypes.Select(s => s.CabinTypeCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new GetCabinTypeByNamesOrCodesQuery(
            names,
            codes,
            companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<CabinTypeExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in cabinTypes)
        {
            var response = await _mediator.Send(new CreateCabinTypeCommand(
                item.CabinTypeName,
                item.CabinTypeCode,
                item.IsActive,
                companyId), ct);
            if (response.IsFailure)
                return Result.Failure<CabinTypeExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CabinTypeExcelImportsResponse(true);
    }

    public async Task<Result<CabinTypeCodeCreatorResponse?>> CabinTypeCodeCreator(
        CabinTypeCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator ");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CabinTypeCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CabinTypeCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new CabinTypeCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CabinTypeCodeCreatorResponse>(response.Error!);

        return new CabinTypeCodeCreatorResponse(response.Value!.Value);
    }

    public async Task<Result<StateChangerCabinTypesResponse?>> StateChangerCabinTypes(
        StateChangerCabinTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerCabinTypes, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerCabinTypesValidator, StateChangerCabinTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerCabinTypesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerCabinTypesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsCabinTypeByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerCabinTypesResponse>(responses.Error!);

        var values = responses.Value;
        var response = await _mediator.Send(new StateChangerCabinTypesCommand(
            values,
            request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerCabinTypesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerCabinTypesResponse(true);
    }

    public async Task<Result<UpdateCabinTypeResponse?>> UpdateCabinType(
        UpdateCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateCabinType, Id:{Id}, CabinTypeName:{CabinTypeName}", request.Id, request.CabinTypeName);

        var isValidRequest = await request.IsValidAsync<UpdateCabinTypeValidator, UpdateCabinTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCabinTypeResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateCabinTypeResponse>(GlobalErrors.InvalidCompany);

        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateCabinTypeResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetCabinTypeByNameQuery(
            request.CabinTypeName,
            companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateCabinTypeResponse>(CabinTypeErrors.TypeNameIsDuplicate);

        var response = await _mediator.Send(new UpdateCabinTypeCommand(
            request.Id,
            request.CabinTypeName,
            request.IsActive,
            companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateCabinTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!;
    }

    public async Task<Result<DeleteCabinTypeResponse?>> DeleteCabinType(
        DeleteCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("Disable CabinType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteCabinTypeValidator, DeleteCabinTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteCabinTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteCabinTypeCommand(request.Id!), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteCabinTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteCabinTypeResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<CabinTypeGroupDeleteResponse?>> CabinTypeGroupDelete(
        CabinTypeGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for CabinTypeGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<CabinTypeGroupDeleteValidator, CabinTypeGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CabinTypeGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var id in request.Ids)
        {
            var response = await _mediator.Send(new DeleteCabinTypeCommand(id), ct);
            if (response.IsFailure)
                return Result.Failure<CabinTypeGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CabinTypeGroupDeleteResponse(true);
    }

    public async Task<Result<InactiveCabinTypeResponse?>> InactiveCabinType(
        InactiveCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveCabinType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveCabinTypeValidator, InactiveCabinTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveCabinTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveCabinTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveCabinTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveCabinTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveCabinTypeResponse?>> ActiveCabinType(
        ActiveCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for ActiveCabinType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveCabinTypeValidator, ActiveCabinTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveCabinTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveCabinTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveCabinTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveCabinTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<GetCabinTypeByIdResponse?>> GetCabinTypeById(
        GetCabinTypeByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCabinTypeById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetCabinTypeByIdValidator,
            GetCabinTypeByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCabinTypeByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCabinTypeByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetCabinTypeByIdResponse>(response.Error!);

        return response;
    }

    public async Task<Result<GetsCabinTypeResponse?>> GetsCabinType(
        GetsCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCabinType, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCabinTypeValidator, GetsCabinTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCabinTypeResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCabinTypeResponse>(GlobalErrors.InvalidCompany);

        var response = await _mediator.Send(new GetsCabinTypeQuery(
            null,
            request.FilterData,
            request.IsActive,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsCabinTypeResponse>(response.Error!);

        var values = response.Value!.Data;
        var companyIds = values?.Where(w =>
                                    w.CompanyId != null &&
                                    w.CompanyId > 0)
                                .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);
        foreach (var item in values!)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsCabinTypeResponse(values, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetCabinTypeByNameResponse?>> GetCabinTypeByName(
        GetCabinTypeByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCabinTypeByName, CabinTypeName:{CabinTypeName}", request.CabinTypeName);

        var isValidRequest = await request.IsValidAsync<GetCabinTypeByNameValidator, GetCabinTypeByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCabinTypeByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCabinTypeByNameQuery(request.CabinTypeName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetCabinTypeByNameResponse>(response.Error!);
        var value = response.Value;

        Company? company = null;
        if (value!.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        response.Value!.CompanyNameFa = company?.NameFa;
        return response;
    }

    public async Task<Result<GetCabinTypeByCodeResponse?>> GetCabinTypeByCode(
        GetCabinTypeByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCabinTypeByCode, CabinTypeCode:{CabinTypeCode}", request.CabinTypeCode);

        var isValidRequest = await request.IsValidAsync<GetCabinTypeByCodeValidator, GetCabinTypeByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCabinTypeByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCabinTypeByCodeQuery(request.CabinTypeCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetCabinTypeByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        response.Value!.CompanyNameFa = company?.NameFa;
        return response;
    }

    public async Task<Result<GetsActiveCabinTypesResponse?>> GetsActiveCabinTypes(
        GetsActiveCabinTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsActiveCabinTypes,Code:{Code} , Name:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}", request.Code, request.Name, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveCabinTypesValidator, GetsActiveCabinTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveCabinTypesResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsActiveCabinTypesQuery(
            request.FilterData,
            request.Code,
            request.Name,
            request.PageIndex,
            request.PageSize), ct);

        if (response.IsFailure)
            return Result.Failure<GetsActiveCabinTypesResponse>(response.Error!);

        return new GetsActiveCabinTypesResponse(response.Value!.Data!, response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCabinTypeExcelExporterResponse?>> GetsCabinTypeExcelExporter(
        GetsCabinTypeExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCabinTypeExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCabinTypeExcelExporterValidator, GetsCabinTypeExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCabinTypeExcelExporterResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsCabinTypeExcelExporterResponse>(GlobalErrors.InvalidCompany);

        var responses = await _mediator.Send(new GetsCabinTypeQuery(
            request.Ids,
            request.FilterData,
            request.IsActive,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsCabinTypeExcelExporterResponse>(responses.Error!);

        var values = responses.Value.Data;
        var companyIds = values?.Where(w =>
                                    w.CompanyId != null &&
                                    w.CompanyId > 0)
                                .Select(s => (long)s.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);
        foreach (var item in values!)
        {
            var company = companies?.Where(w => w.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(CabinTypeExcels.CabinTypeToExcel(values, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CabinTypes-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };
        return new GetsCabinTypeExcelExporterResponse(file);
    }

    public async Task<Result<GetsCabinTypeExcelEnumResponse?>> GetsCabinTypeExcelEnum(
        GetsCabinTypeExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCabinTypeExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<CabinTypeExcelEnum>());
        return new GetsCabinTypeExcelEnumResponse(response);
    }
}