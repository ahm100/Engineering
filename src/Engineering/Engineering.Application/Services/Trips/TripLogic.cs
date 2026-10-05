using Engineering.Application.Services.Trips.Commands.Active;
using Engineering.Application.Services.Trips.Commands.CodeCreator;
using Engineering.Application.Services.Trips.Commands.Create;
using Engineering.Application.Services.Trips.Commands.Disable;
using Engineering.Application.Services.Trips.Commands.Inactive;
using Engineering.Application.Services.Trips.Commands.StateChangerTrips;
using Engineering.Application.Services.Trips.Commands.Update;
using Engineering.Application.Services.Trips.Models.Active;
using Engineering.Application.Services.Trips.Models.CodeCreator;
using Engineering.Application.Services.Trips.Models.Create;
using Engineering.Application.Services.Trips.Models.Disable;
using Engineering.Application.Services.Trips.Models.GetByCode;
using Engineering.Application.Services.Trips.Models.GetById;
using Engineering.Application.Services.Trips.Models.GetByName;
using Engineering.Application.Services.Trips.Models.GetsActive;
using Engineering.Application.Services.Trips.Models.GetsFiltered;
using Engineering.Application.Services.Trips.Models.GetsTripExcelEnum;
using Engineering.Application.Services.Trips.Models.GetsTripExcelExporter;
using Engineering.Application.Services.Trips.Models.Inactive;
using Engineering.Application.Services.Trips.Models.StateChangerTrips;
using Engineering.Application.Services.Trips.Models.TripExcelImports;
using Engineering.Application.Services.Trips.Models.TripGroupDelete;
using Engineering.Application.Services.Trips.Models.Update;
using Engineering.Application.Services.Trips.Queries.FindTripByNamesOrCodes;
using Engineering.Application.Services.Trips.Queries.GetByCode;
using Engineering.Application.Services.Trips.Queries.GetById;
using Engineering.Application.Services.Trips.Queries.GetByName;
using Engineering.Application.Services.Trips.Queries.GetsActive;
using Engineering.Application.Services.Trips.Queries.GetsFiltered;
using Engineering.Application.Services.Trips.Queries.GetsTripByIds;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.Trips;

public class TripLogic : ITripLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TripLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public TripLogic(IMediator mediator, ILogger<TripLogic> logger, IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateTripResponse?>> CreateTrip(CreateTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateTrip, TripName:{TripName}, TripCode:{TripCode},",
            request.TripName, request.TripCode);
        //Validate data
        var isValidRequest = await request.IsValidAsync<CreateTripValidator, CreateTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateTripResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateTripResponse>(companyResponse.Error!);
        }

        //Validate name
        var nameIsDuplicate = await _mediator.Send(new GetTripByNameQuery(request.TripName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateTripResponse>(TripErrors.NameIsDuplicate);
        //Validate code
        var codeIsDuplicate = await _mediator.Send(new GetTripByCodeQuery(request.TripCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateTripResponse>(TripErrors.CodeIsDuplicate);
        //create Trip
        var response = await _mediator.Send(new CreateTripCommand(request.TripCode, request.TripName, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateTripResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateTripResponse>();
    }

    public async Task<Result<TripExcelImportsResponse?>> TripExcelImports(TripExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for TripExcelImports");

        //Validate data
        var isValidRequest = await request.IsValidAsync<TripExcelImportsValidator, TripExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<TripExcelImportsResponse>(isValidRequest.Error!);

        var Trips = ExcelImporter.Import<TripExcelImportsModel>(request.DocumentFile);
        if (Trips is null)
            return Result.Failure<TripExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);
        if (Trips.Count != Trips.Select(x => x.TripName).Distinct().Count())
            return Result.Failure<TripExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveNameDuplicate);
        if (Trips.Count != Trips.Select(x => x.TripCode).Distinct().Count())
            return Result.Failure<TripExcelImportsResponse>(GlobalErrors.ExcelImporteredHaveCodeDuplicate);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<TripExcelImportsResponse>(companyResponse.Error!);
        }

        var names = Trips.Select(x => x.TripName).ToList();
        var codes = Trips.Select(x => x.TripCode).ToList();
        var codeIsDuplicate = await _mediator.Send(new FindTripByNamesOrCodesQuery(names, codes, companyId), ct);
        if (codeIsDuplicate.Value)
            return Result.Failure<TripExcelImportsResponse>(GlobalErrors.HaveDuplicateKey);

        foreach (var item in Trips)
        {
            var response = await _mediator.Send(new CreateTripCommand(item.TripName, item.TripCode, item.IsActive, companyId), ct);
            if (response.IsFailure)
                return Result.Failure<TripExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new TripExcelImportsResponse(true);
    }

    public async Task<Result<TripCodeCreatorResponse?>> TripCodeCreator(TripCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("Request for CodeCreator");

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<TripCodeCreatorResponse>(companyResponse.Error!);
        }

        var response = await _mediator.Send(new TripCodeCreatorCommand(companyId), ct);
        if (response.IsFailure)
            return Result.Failure<TripCodeCreatorResponse>(response.Error!);

        return new TripCodeCreatorResponse(response.Value!);
    }

    public async Task<Result<DisableTripResponse?>> DisableTrip(DisableTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableTrip, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<DisableTripValidator, DisableTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableTripResponse>(isValidRequest.Error!);
        //disable Trip
        var response = await _mediator.Send(new DisableTripCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableTripResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableTripResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<TripGroupDeleteResponse?>> TripGroupDelete(TripGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for TripGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<TripGroupDeleteValidator, TripGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<TripGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableTripCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<TripGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new TripGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateTripResponse?>> UpdateTrip(UpdateTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateTrip, id:{Id}, Trip:{TripName} , description:{TripCode},", request.Id, request.TripName, request.TripCode);

        var isValidRequest = await request.IsValidAsync<UpdateTripValidator, UpdateTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateTripResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateTripResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetTripByNameQuery(request.TripName, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateTripResponse>(TripErrors.NameIsDuplicate);

        var codeIsDuplicate = await _mediator.Send(new GetTripByCodeQuery(request.TripCode, companyId), ct);
        if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
            return Result.Failure<UpdateTripResponse>(TripErrors.CodeIsDuplicate);

        var response = await _mediator.Send(new UpdateTripCommand(request.Id, request.TripName, request.TripCode, request.IsActive, companyId), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTripResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateTripResponse>();
    }

    public async Task<Result<InactiveTripResponse?>> InactiveTrip(InactiveTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveTrip, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveTripValidator, InactiveTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveTripResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveTripCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveTripResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveTripResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveTripResponse?>> ActiveTrip(ActiveTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveTrip, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveTripValidator, ActiveTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveTripResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveTripCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveTripResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveTripResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerTripsResponse?>> StateChangerTrips(StateChangerTripsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerTrips, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerTripsValidator, StateChangerTripsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerTripsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerTripsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsTripByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerTripsResponse>(responses.Error!);
        var values = responses.Value;
        foreach (var item in values)
            if (item.IsLock != null && item.IsLock == true)
                return Result.Failure<StateChangerTripsResponse>(TripErrors.IsLockData);

        var response = await _mediator.Send(new StateChangerTripsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerTripsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerTripsResponse(true);
    }

    public async Task<Result<GetTripByIdResponse?>> GetTripById(GetTripByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTripById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetTripByIdValidator, GetTripByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTripByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTripByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetTripByIdResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetTripByIdResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetTripByNameResponse?>> GetTripByName(GetTripByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTripByName TripName:{TripName}", request.TripName);

        var isValidRequest = await request.IsValidAsync<GetTripByNameValidator, GetTripByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTripByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTripByNameQuery(request.TripName, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetTripByNameResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetTripByNameResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetTripByCodeResponse?>> GetTripByCode(GetTripByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTripByCode, TripCode:{TripCode}", request.TripCode);

        var isValidRequest = await request.IsValidAsync<GetTripByCodeValidator, GetTripByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTripByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTripByCodeQuery(request.TripCode, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetTripByCodeResponse>(response.Error!);
        var value = response.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var data = value.Adapt<GetTripByCodeResponse>();
        data.CompanyNameFa = company?.NameFa;

        return data;
    }

    public async Task<Result<GetsActiveTripResponse?>> GetsActiveTrip(GetsActiveTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveTrip, TripCode:{TripCode} , TripName:{TripName}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.TripCode, request.TripName, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveTripValidator, GetsActiveTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveTripResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsActiveTripQuery(request.FilterData, request.TripCode, request.TripName, companyId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveTripResponse>(response.Error!);
        var values = response.Value!.Data!;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveTripResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsActiveTripResponse(data ?? new List<GetsActiveTripResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsFilteredTripResponse?>> GetsFilteredTrip(GetsFilteredTripRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTrip,TripName:{TripName} , TripCode:{TripCode}, IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.TripName, request.TripCode, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsFilteredTripValidator, GetsFilteredTripRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredTripResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsFilteredTripQuery(null, request.FilterData, request.TripName, request.TripCode,
            request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredTripResponse>(response.Error!);
        var values = response.Value!.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsFilteredTripResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsFilteredTripResponse(data ?? new List<GetsFilteredTripResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsTripExcelExporterResponse?>> GetsTripExcelExporter(GetsTripExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTripExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsTripExcelExporterValidator, GetsTripExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTripExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetsFilteredTripQuery(request.Ids, request.FilterData, request.TripName, request.TripCode,
            request.IsActive, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsTripExcelExporterResponse>(responses.Error!);
        var values = responses.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsTripExcelExporterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }
        var file = new FileContentResult(TripExcels.TripToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Trips-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsTripExcelExporterResponse(file);
    }

    public async Task<Result<GetsTripExcelEnumResponse?>> GetsTripExcelEnum(GetsTripExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTripExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<TripExcelEnum>());
        return new GetsTripExcelEnumResponse(response);
    }
}