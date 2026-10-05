using Engineering.Application.Services.Transportations.Commands.Active;
using Engineering.Application.Services.Transportations.Commands.CodeCreator;
using Engineering.Application.Services.Transportations.Commands.Create;
using Engineering.Application.Services.Transportations.Commands.Disable;
using Engineering.Application.Services.Transportations.Commands.Inactive;
using Engineering.Application.Services.Transportations.Commands.StateChangerTransportations;
using Engineering.Application.Services.Transportations.Commands.Update;
using Engineering.Application.Services.Transportations.Models.Active;
using Engineering.Application.Services.Transportations.Models.CodeCreator;
using Engineering.Application.Services.Transportations.Models.Create;
using Engineering.Application.Services.Transportations.Models.Disable;
using Engineering.Application.Services.Transportations.Models.GetByCode;
using Engineering.Application.Services.Transportations.Models.GetById;
using Engineering.Application.Services.Transportations.Models.GetByName;
using Engineering.Application.Services.Transportations.Models.GetsActive;
using Engineering.Application.Services.Transportations.Models.GetsFiltered;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelEnum;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;
using Engineering.Application.Services.Transportations.Models.GetTransportationTypes;
using Engineering.Application.Services.Transportations.Models.Inactive;
using Engineering.Application.Services.Transportations.Models.StateChangerTransportations;
using Engineering.Application.Services.Transportations.Models.TransportationExcelImports;
using Engineering.Application.Services.Transportations.Models.TransportationGroupDelete;
using Engineering.Application.Services.Transportations.Models.Update;
using Engineering.Application.Services.Transportations.Queries.FindTransportationByNamesOrCodes;
using Engineering.Application.Services.Transportations.Queries.GetByCode;
using Engineering.Application.Services.Transportations.Queries.GetById;
using Engineering.Application.Services.Transportations.Queries.GetByName;
using Engineering.Application.Services.Transportations.Queries.GetsActive;
using Engineering.Application.Services.Transportations.Queries.GetsFiltered;
using Engineering.Application.Services.Transportations.Queries.GetsTransportationByIds;
using Engineering.Domain.Entities.Transportations.Enums;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;


namespace Engineering.Application.Services.Transportations;

public class TransportationLogic : ITransportationLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransportationLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public TransportationLogic(IMediator mediator, ILogger<TransportationLogic> logger, IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateTransportationResponse?>> CreateTransportation(CreateTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateTransportation, TransportationName:{TransportationName}, TransportationCode:{TransportationCode},",
            request.TransportationName, request.TransportationCode);
        //Validate data
        var isValidRequest = await request.IsValidAsync<CreateTransportationValidator, CreateTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateTransportationResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateTransportationResponse>(companyResponse.Error!);
        }

        //Validate name
        var nameIsDuplicate = await _mediator.Send(new GetTransportationByNameQuery(request.TransportationName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateTransportationResponse>(TransportationErrors.NameIsDuplicate);
        //Validate code
        var codeIsDuplicate = await _mediator.Send(new GetTransportationByCodeQuery(request.TransportationCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateTransportationResponse>(TransportationErrors.CodeIsDuplicate);
        //create Transportation
        var response = await _mediator.Send(new CreateTransportationCommand(request.TransportationCode, request.TransportationName, request.IsPassenger, request.IsActive, companyId, request.TransportationType), ct);
        if (response.IsFailure)
            return Result.Failure<CreateTransportationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateTransportationResponse>();
    }

    public async Task<Result<TransportationExcelImportsResponse?>> TransportationExcelImports(TransportationExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for TransportationExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<TransportationExcelImportsValidator, TransportationExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<TransportationExcelImportsResponse>(isValidRequest.Error!);

        var Transportations = ExcelImporter.Import<TransportationExcelImportsModel>(request.DocumentFile);
        if (Transportations is null)
            return Result.Failure<TransportationExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (Transportations.Count != Transportations.Select(x => x.TransportationName).Distinct().Count())
            return Result.Failure<TransportationExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (Transportations.Count != Transportations.Select(x => x.TransportationCode).Distinct().Count())
            return Result.Failure<TransportationExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<TransportationExcelImportsResponse>(companyResponse.Error!);
        }

        var names = Transportations.Select(x => x.TransportationName).ToList();
        var codes = Transportations.Select(x => x.TransportationCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindTransportationByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<TransportationExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in Transportations)
        {
            var response = await _mediator.Send(new CreateTransportationCommand(item.TransportationName, item.TransportationCode, item.IsPassenger, item.IsActive, companyId, item.TransportationType), ct);
            if (response.IsFailure)
                return Result.Failure<TransportationExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new TransportationExcelImportsResponse(true);
    }

    public async Task<Result<TransportationCodeCreatorResponse?>> TransportationCodeCreator(TransportationCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<TransportationCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new TransportationCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<TransportationCodeCreatorResponse>(response.Error!);

        return new TransportationCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<TransportationGroupDeleteResponse?>> TransportationGroupDelete(TransportationGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for TransportationGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<TransportationGroupDeleteValidator, TransportationGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<TransportationGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableTransportationCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<TransportationGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new TransportationGroupDeleteResponse(true);
    }

    public async Task<Result<StateChangerTransportationsResponse?>> StateChangerTransportations(StateChangerTransportationsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerTransportations, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerTransportationsValidator, StateChangerTransportationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerTransportationsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerTransportationsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsTransportationByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerTransportationsResponse>(responses.Error!);
        var values = responses.Value;

        foreach (var item in values)
            if (item.IsLock != null && item.IsLock == true)
                return Result.Failure<StateChangerTransportationsResponse>(TransportationErrors.IsLockData);

        var response = await _mediator.Send(new StateChangerTransportationsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerTransportationsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerTransportationsResponse(true);
    }

    public async Task<Result<DisableTransportationResponse?>> DisableTransportation(DisableTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableTransportation, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<DisableTransportationValidator, DisableTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableTransportationResponse>(isValidRequest.Error!);
        //disable Transportation
        var response = await _mediator.Send(new DisableTransportationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableTransportationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableTransportationResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<UpdateTransportationResponse?>> UpdateTransportation(UpdateTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTransportation, id:{Id}, Transportation:{TransportationName} , description:{TransportationCode},", request.Id, request.TransportationName, request.TransportationCode);

        var isValidRequest = await request.IsValidAsync<UpdateTransportationValidator, UpdateTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateTransportationResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateTransportationResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetTransportationByNameQuery(request.TransportationName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateTransportationResponse>(TransportationErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetTransportationByCodeQuery(request.TransportationCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateTransportationResponse>(TransportationErrors.CodeIsDuplicate);

        var response = await _mediator.Send(new UpdateTransportationCommand(request.Id, request.TransportationName, request.TransportationCode, request.IsPassenger, request.IsActive, companyId, request.TransportationType), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTransportationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateTransportationResponse>();
    }

    public async Task<Result<InactiveTransportationResponse?>> InactiveTransportation(InactiveTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveTransportation, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveTransportationValidator, InactiveTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveTransportationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveTransportationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveTransportationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveTransportationResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveTransportationResponse?>> ActiveTransportation(ActiveTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveTransportation, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveTransportationValidator, ActiveTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveTransportationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveTransportationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveTransportationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveTransportationResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<GetTransportationByIdResponse?>> GetTransportationById(GetTransportationByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetTransportationByIdValidator, GetTransportationByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTransportationByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTransportationByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetTransportationByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetTransportationByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetTransportationByNameResponse?>> GetTransportationByName(GetTransportationByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationByName TransportationName:{TransportationName}", request.TransportationName);

        var isValidRequest = await request.IsValidAsync<GetTransportationByNameValidator, GetTransportationByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTransportationByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTransportationByNameQuery(request.TransportationName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetTransportationByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetTransportationByNameResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetTransportationByCodeResponse?>> GetTransportationByCode(GetTransportationByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationByCode, TransportationCode:{TransportationCode}", request.TransportationCode);

        var isValidRequest = await request.IsValidAsync<GetTransportationByCodeValidator, GetTransportationByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTransportationByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTransportationByCodeQuery(request.TransportationCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetTransportationByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetTransportationByCodeResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetsActiveTransportationResponse?>> GetsActiveTransportation(GetsActiveTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveTransportation, TransportationCode:{TransportationCode} , TransportationName:{TransportationName}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.TransportationCode, request.TransportationName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveTransportationValidator, GetsActiveTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveTransportationResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsActiveTransportationQuery(request.FilterData, request.TransportationCode, request.TransportationName, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveTransportationResponse>(response.Error!);
        var values = response.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveTransportationResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsActiveTransportationResponse(data ?? new List<GetsActiveTransportationResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredTransportationResponse?>> GetsFilteredTransportation(GetsFilteredTransportationRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportation,TransportationName:{TransportationName} , TransportationCode:{TransportationCode}, IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.TransportationName, request.TransportationCode, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsFilteredTransportationValidator, GetsFilteredTransportationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredTransportationResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsFilteredTransportationQuery(null, request.FilterData, request.TransportationName, request.TransportationCode, request.IsPassenger,
            request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredTransportationResponse>(response.Error!);
        var values = response.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsFilteredTransportationResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsFilteredTransportationResponse(data ?? new List<GetsFilteredTransportationResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsTransportationExcelExporterResponse?>> GetsTransportationExcelExporter(GetsTransportationExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsTransportationExcelExporterValidator, GetsTransportationExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTransportationExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsFilteredTransportationQuery(request.Ids, request.FilterData, request.TransportationName, request.TransportationCode, request.IsPassenger,
            request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsTransportationExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsTransportationExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        var file = new FileContentResult(TransportationExcels.TransportationToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Transportations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsTransportationExcelExporterResponse(file);
    }

    public async Task<Result<GetsTransportationExcelEnumResponse?>> GetsTransportationExcelEnum(GetsTransportationExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationExcelEnum>());
        return new GetsTransportationExcelEnumResponse(response);
    }
    public async Task<Result<GetTransportationTypeResponse?>> GetTransportationTypes(GetTransportationTypeRequest request, CT ct)
    {
        return await Task.FromResult(new GetTransportationTypeResponse(EnumExt.GetEnumObjectList<TransportationType>()));
    }
}