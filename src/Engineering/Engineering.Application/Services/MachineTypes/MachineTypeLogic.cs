using Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCodes;
using Engineering.Application.Services.MachineTypes.Commands.ActiveMachineType;
using Engineering.Application.Services.MachineTypes.Commands.CreateMachineType;
using Engineering.Application.Services.MachineTypes.Commands.DisableMachineType;
using Engineering.Application.Services.MachineTypes.Commands.InactiveMachineType;
using Engineering.Application.Services.MachineTypes.Commands.MachineTypeCodeCreator;
using Engineering.Application.Services.MachineTypes.Commands.StateChangerMachineTypes;
using Engineering.Application.Services.MachineTypes.Commands.UpdateMachineType;
using Engineering.Application.Services.MachineTypes.Models.ActiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.CreateMachineType;
using Engineering.Application.Services.MachineTypes.Models.DisableMachineType;
using Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;
using Engineering.Application.Services.MachineTypes.Models.GetMachineTypes;
using Engineering.Application.Services.MachineTypes.Models.GetsActiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelEnum;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;
using Engineering.Application.Services.MachineTypes.Models.InactiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeCodeCreator;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeExcelImports;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeGroupDelete;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeModels;
using Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;
using Engineering.Application.Services.MachineTypes.Models.UpdateMachineType;
using Engineering.Application.Services.MachineTypes.Queries.FindDuplicateMachineType;
using Engineering.Application.Services.MachineTypes.Queries.GetByCode;
using Engineering.Application.Services.MachineTypes.Queries.GetDuplicateMachineType;
using Engineering.Application.Services.MachineTypes.Queries.GetMachineTypeById;
using Engineering.Application.Services.MachineTypes.Queries.GetMachineTypes;
using Engineering.Application.Services.MachineTypes.Queries.GetsActiveMachineType;
using Engineering.Application.Services.MachineTypes.Queries.GetsMachineTypeByIds;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.MachineTypes;

public class MachineTypeLogic : IMachineTypeLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<MachineTypeLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public MachineTypeLogic(
        IMediator mediator,
        ILogger<MachineTypeLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateMachineTypeResponse?>> CreateMachineType(
        CreateMachineTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateMachineType, MachineTypeTitle:{MachineTypeTitle}, MachineTypeCode:{MachineTypeCode},",
           request.MachineTypeTitle, request.MachineTypeCode);

        var isValidRequest = await request.IsValidAsync<CreateMachineTypeValidator, CreateMachineTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateMachineTypeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateMachineTypeResponse>(companyResponse.Error!);
        }

        var getCabinType = await _mediator.Send(new GetCabinTypeByCodeQuery(request.CabinTypeCode, companyId), ct);
        if (getCabinType.IsFailure)
            return Result.Failure<CreateMachineTypeResponse>(CabinTypeErrors.CabinTypeWithCodeNotFound);

        var isDuplicate = await _mediator.Send(new GetDuplicateMachineTypeQuery(request.MachineTypeTitle, request.FromWeight,
           request.UntilWeight, request.CabinTypeCode, companyId), ct);
        if (isDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateMachineTypeResponse>(MachineTypeErrors.MachineTypeIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetMachineTypeByCodeQuery(request.MachineTypeCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null })
            return Result.Failure<CreateMachineTypeResponse>(MachineTypeErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new CreateMachineTypeCommand(
            request.MachineTypeTitle, request.MachineTypeCode, request.FromWeight, request.UntilWeight,
            getCabinType.Value!.Id, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateMachineTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!;
    }

    public async Task<Result<MachineTypeExcelImportsResponse?>> MachineTypeExcelImports(
        MachineTypeExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineTypeExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<MachineTypeExcelImportsValidator, MachineTypeExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineTypeExcelImportsResponse>(isValidRequest.Error!);

        var MachineTypes = ExcelImporter.Import<MachineTypeExcelImportsModel>(request.DocumentFile);
        if (MachineTypes is null)
            return Result.Failure<MachineTypeExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (MachineTypes.Count != MachineTypes.Select(x => x.MachineTypeName).Distinct().Count())
            return Result.Failure<MachineTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (MachineTypes.Count != MachineTypes.Select(x => x.MachineTypeCode).Distinct().Count())
            return Result.Failure<MachineTypeExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<MachineTypeExcelImportsResponse>(companyResponse.Error!);
        }

        var names = MachineTypes.Select(x => x.MachineTypeName).ToList();
        var codes = MachineTypes.Select(x => x.MachineTypeCode).ToList();
        var untilWeights = MachineTypes.Select(x => x.UntilWeight).ToList();
        var fromWeights = MachineTypes.Select(x => x.FromWeight).ToList();
        var cabinTypeCodes = MachineTypes.Select(x => x.CabinTypeCode).ToList();
        var isDuplicate = await _mediator.Send(new FindDuplicateMachineTypeQuery(names, codes, fromWeights, untilWeights, cabinTypeCodes, companyId), ct);
        if (isDuplicate.Value)
            return Result.Failure<MachineTypeExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        var getCabinType = await _mediator.Send(new GetCabinTypeByCodesQuery(cabinTypeCodes, companyId), ct);
        if (getCabinType.IsFailure)
            return Result.Failure<MachineTypeExcelImportsResponse>(CabinTypeErrors.CabinTypeWithCodeNotFound);
        var cabinTypes = getCabinType.Value;

        foreach (var item in MachineTypes)
        {
            var cabinType = cabinTypes?.Where(x => x.CabinTypeCode == item.CabinTypeCode).FirstOrDefault();

            var response = await _mediator.Send(new CreateMachineTypeCommand(item.MachineTypeName, item.MachineTypeCode, item.FromWeight, item.UntilWeight,
                cabinType!.Id, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<MachineTypeExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineTypeExcelImportsResponse(true);
    }

    public async Task<Result<UpdateMachineTypeResponse?>> UpdateMachineType(
        UpdateMachineTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateMachineType, Id:{Id}, MachineTypeTitle:{MachineTypeTitle}, MachineTypeCode:{MachineTypeCode}",
          request.Id, request.MachineTypeTitle, request.MachineTypeCode);

        var isValidRequest = await request.IsValidAsync<UpdateMachineTypeValidator, UpdateMachineTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateMachineTypeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateMachineTypeResponse>(companyResponse.Error!);
        }

        var getCabinType = await _mediator.Send(new GetCabinTypeByCodeQuery(request.CabinTypeCode, companyId), ct);
        if (getCabinType.IsFailure)
            return Result.Failure<UpdateMachineTypeResponse>(CabinTypeErrors.CabinTypeWithIdNotFound);
        var cabinType = getCabinType.Value;

        var isDuplicate = await _mediator.Send(new GetDuplicateMachineTypeQuery(request.MachineTypeTitle, request.FromWeight,
           request.UntilWeight, request.CabinTypeCode, companyId), ct);
        if (isDuplicate is { IsSuccess: true, Value: not null } && isDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateMachineTypeResponse>(MachineTypeErrors.MachineTypeIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetMachineTypeByCodeQuery(request.MachineTypeCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateMachineTypeResponse>(MachineTypeErrors.TypeCodeIsDuplicate);

        var response = await _mediator.Send(new UpdateMachineTypeCommand(
            request.Id, request.MachineTypeTitle, request.MachineTypeCode, request.FromWeight,
            request.UntilWeight, cabinType!.Id, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateMachineTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value;
    }

    public async Task<Result<StateChangerMachineTypesResponse?>> StateChangerMachineTypes(
        StateChangerMachineTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerMachineTypes, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerMachineTypesValidator, StateChangerMachineTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerMachineTypesResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerMachineTypesResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsMachineTypeByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerMachineTypesResponse>(responses.Error!);
        var values = responses.Value;

        var response = await _mediator.Send(new StateChangerMachineTypesCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerMachineTypesResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerMachineTypesResponse(true);
    }

    public async Task<Result<MachineTypeCodeCreatorResponse?>> MachineTypeCodeCreator(
        MachineTypeCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineTypeCodeCreator");

        var isValidRequest = await request.IsValidAsync<MachineTypeCodeCreatorValidator, MachineTypeCodeCreatorRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineTypeCodeCreatorResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<MachineTypeCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new MachineTypeCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<MachineTypeCodeCreatorResponse>(response.Error!);

        return new MachineTypeCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<InactiveMachineTypeResponse?>> InactiveMachineType(
        InactiveMachineTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveMachineType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveMachineTypeValidator, InactiveMachineTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveMachineTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveMachineTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveMachineTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveMachineTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveMachineTypeResponse?>> ActiveMachineType(
        ActiveMachineTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveMachineType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveMachineTypeValidator, ActiveMachineTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveMachineTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveMachineTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveMachineTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveMachineTypeResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<DisableMachineTypeResponse?>> DisableMachineType(
        DisableMachineTypeRequest request, CT ct)
    {
        _logger.LogInformation("Disable MachineType, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableMachineTypeValidator, DisableMachineTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableMachineTypeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableMachineTypeCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableMachineTypeResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableMachineTypeResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<MachineTypeGroupDeleteResponse?>> MachineTypeGroupDelete(
        MachineTypeGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineTypeGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<MachineTypeGroupDeleteValidator, MachineTypeGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineTypeGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableMachineTypeCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<MachineTypeGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineTypeGroupDeleteResponse(true);
    }

    public async Task<Result<GetMachineTypeByIdResponse?>> GetMachineTypeById(
        GetMachineTypeByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineTypeById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetMachineTypeByIdValidator, GetMachineTypeByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineTypeByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineTypeByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineTypeByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetMachineTypeByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetMachineTypesResponse?>> GetMachineTypes(
        GetMachineTypesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineTypes, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetMachineTypesValidator, GetMachineTypesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineTypesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetMachineTypesQuery(null, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineTypesResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetMachineTypesModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetMachineTypesResponse(data ?? new List<GetMachineTypesModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsActiveMachineTypeResponse?>> GetsActiveMachineType(
        GetsActiveMachineTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsActiveMachineType, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveMachineTypeValidator, GetsActiveMachineTypeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveMachineTypeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsActiveMachineTypeQuery(request.FilterData, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveMachineTypeResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveMachineTypeModel>>();
        foreach (var item in data)
        {
            var value = values?.FirstOrDefault(x => x.Id == item.Id);
            item.MachineTypeTitle = $"{value?.MachineTypeTitle} {value?.CabinType.CabinTypeName} وزن {value?.FromWeight} تا {value?.UntilWeight}";

            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }
        return new GetsActiveMachineTypeResponse(data ?? new List<GetsActiveMachineTypeModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsMachineTypeExcelExporterResponse?>> GetsMachineTypeExcelExporter(
        GetsMachineTypeExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineTypeExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsMachineTypeExcelExporterValidator, GetsMachineTypeExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsMachineTypeExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetMachineTypesQuery(request.Ids, request.FilterData, request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsMachineTypeExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsMachineTypeExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(MachineTypeExcels.MachineTypeToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"MachineTypes-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsMachineTypeExcelExporterResponse(file);
    }

    public async Task<Result<GetsMachineTypeExcelEnumResponse?>> GetsMachineTypeExcelEnum(
        GetsMachineTypeExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineTypeExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<MachineTypeExcelEnum>());
        return new GetsMachineTypeExcelEnumResponse(response);
    }

}
