using Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCodes;
using Engineering.Application.Services.Projects.Queries.GetProjectByCodes;
using Engineering.Application.Services.TransportationRequests.Commands.CreateSnap;
using Engineering.Application.Services.TransportationRequests.Commands.DeleteTransportationRequestDocument;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateSanp;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalSnapPrice;
using Engineering.Application.Services.TransportationRequests.Models.SnapRequestExcelImports;
using Engineering.Application.Services.TransportationRequests.Models.UpdateSnap;
using Engineering.Application.Services.TransportationRequests.Queries.GetSnapByIdWithoutInclude;
using Engineering.Application.Services.TransportationRequests.Queries.GetsTotalSnapPrice;
using Engineering.Application.Services.Transportations.Queries.GetByType;
using Engineering.Application.Services.Trips.Queries.GetByCodes;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCitiesByCodes;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryByCodes;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetsCostGroupByCodes;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCitiesByCodes;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdPartiesForSnap;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredThirdPartiesForSnap;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations.Enums;
using Engineering.Domain.Entities.Trips;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    public async Task<Result<CreateSnapResponse?>> CreateSnap(CreateSnapRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateSnap, TripId:{TripId},", request.TripId);
        var validationResult = await ValidateSnapRequest(request, null, ct);
        if (validationResult.IsFailure)
            return Result.Failure<CreateSnapResponse>(validationResult.Error!);

        if (ValidateNumberPlates(request.NumberPlates))
            return Result.Failure<CreateSnapResponse>(TransportationRequestErrors.UnNumberPlates);

        var (startDate, endDate) = PrepareDates(request.StartDate, request.EndDate, request.StartTime, request.EndTime);

        var response = await _mediator.Send(new CreateSnapCommand(
            validationResult.Value?.Trip!,
            validationResult.Value?.Transportation!,
            request.StartingCityId,
            request.DestinationCityId,
            startDate ?? request.StartDate.Date,
            endDate ?? request.StartDate.Date,
            request.Description,
            validationResult.Value?.CostCenters!,
            validationResult.Value?.Projects,
            request.DriverId,
            request.DriverName,
            request.PhoneNumber,
            request.CarSpecifications,
            request.NumberPlates,
            request.CurrencyUnitId,
            validationResult.Value?.Transportation?.IsPassenger != null ? validationResult.Value.Transportation.IsPassenger : false,
            validationResult.Value?.CompanyId,
            request.TransportationCostGroupId,
            request.TransportationCostCategoryId,
            request.SnapRequester,
            request.SecondDestinationCityId,
            request.FareAmount,
            request.StopRate,
            request.DestinationAddress,
            request.SecondDestinationAddress,
            request.PersonalPayment,
            request.StartingCityAddress,
            request.ReturnToStart,
            request.RecipientName),
            ct);
        if (response.IsFailure)
            return Result.Failure<CreateSnapResponse>(response.Error!);

        if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
        {
            var documentResult = await AddDocumentsToRequest(request.DocumentUrls, response.Value!, ct);
            if (documentResult.IsFailure)
                return Result.Failure<CreateSnapResponse>(documentResult.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateSnapResponse(response.Value!.Id, true);
    }

    public async Task<Result<UpdateSnapResponse?>> UpdateSnap(UpdateSnapRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateSnap, TripId:{TripId},", request.TripId);
        var validationResult = await ValidateSnapRequest(null, request, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UpdateSnapResponse>(validationResult.Error!);

        var currentUser = _userProfileService.GetProfileInfo();
        if (currentUser is null)
            return Result.Failure<UpdateSnapResponse>(TransportationRequestErrors.UserInfoNotFound);

        var snap = validationResult.Value?.Snap!;
        if (currentUser.UserId != snap.CreatorId)
            return Result.Failure<UpdateSnapResponse>(TransportationRequestErrors.UserIsUnValid);

        if (ValidateStatusForUpdate(snap))
            return Result.Failure<UpdateSnapResponse>(TransportationRequestErrors.UnValidStatus);

        if (ValidateNumberPlates(request.NumberPlates))
            return Result.Failure<UpdateSnapResponse>(TransportationRequestErrors.UnNumberPlates);

        var (startDate, endDate) = PrepareDates(request.StartDate, request.EndDate, request.StartTime, request.EndTime);

        var response = await _mediator.Send(new UpdateSanpCommand(
            snap.Id,
            validationResult.Value?.Trip!,
            snap.Transportation,
            request.StartingCityId,
            request.DestinationCityId,
            startDate ?? request.StartDate.Date,
            endDate ?? request.StartDate.Date,
            request.Description,
            validationResult.Value?.CostCenters!,
            validationResult.Value?.Projects,
            request.DriverId,
            request.DriverName,
            request.PhoneNumber,
            request.CarSpecifications,
            request.NumberPlates,
            request.CurrencyUnitId,
            snap.Transportation.IsPassenger,
            validationResult.Value?.CompanyId,
            request.TransportationCostGroupId,
            request.TransportationCostCategoryId,
            request.SnapRequester,
            request.SecondDestinationCityId,
            request.FareAmount,
            request.StopRate,
            request.DestinationAddress,
            request.SecondDestinationAddress,
            request.PersonalPayment,
            request.StartingCityAddress,
            request.ReturnToStart,
            request.RecipientName),
            ct);

        if (response.IsFailure)
            return Result.Failure<UpdateSnapResponse>(response.Error!);

        foreach (var document in snap.TransportationRequestDocuments)
        {
            var deleteResult = await _mediator.Send(new DeleteTransportationRequestDocumentCommand(document.Id), ct);
            if (deleteResult.IsFailure)
                return Result.Failure<UpdateSnapResponse>(deleteResult.Error!);
        }

        if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
        {
            var documentResult = await AddDocumentsToRequest(request.DocumentUrls, response.Value!, ct);
            if (documentResult.IsFailure)
                return Result.Failure<UpdateSnapResponse>(documentResult.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateSnapResponse(response.Value!.Id, true);
    }
    public async Task<Result<SnapRequestExcelImportsResponse?>> SnapRequestExcelImports(SnapRequestExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for SnapRequestExcelImports");

        var isValidRequest = await request.IsValidAsync<SnapRequestExcelImportsValidator, SnapRequestExcelImportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SnapRequestExcelImportsResponse>(isValidRequest.Error!);

        var snaps = ExcelImporter.Import<SnapRequestExcelImportsModel>(request.DocumentFile);
        if (snaps is null)
            return Result.Failure<SnapRequestExcelImportsResponse>(GlobalErrors.ErrorOnReadFile);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(companyResponse.Error!);
        }

        var transportationQuery = await _mediator.Send(new GetTransportationByTypeQuery(TransportationType.SnappPassenger), ct);
        if (transportationQuery.IsFailure)
            return Result.Failure<SnapRequestExcelImportsResponse>(transportationQuery.Error!);
        var transportation = transportationQuery.Value;

        List<Trip>? trips = [];
        var tripCodes = snaps.Where(x => x.TripCode != null && x.TripCode != "").Select(x => x.TripCode).Distinct().ToList();
        if (tripCodes != null && tripCodes.Count > 0)
        {
            var tripsQuery = await _mediator.Send(new GetTripByCodesQuery(tripCodes, null), ct);
            if (tripsQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(tripsQuery.Error!);
            trips = tripsQuery.Value;

            if (trips?.Count != tripCodes?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidTrips);
        }

        List<CostCenter>? costCenters = [];
        var costCenterCodes = snaps.Where(x => x.CostCenterCodes != null && x.CostCenterCodes != "").Select(x => x.CostCenterCodes).Distinct().ToList();
        if (costCenterCodes != null && costCenterCodes.Count > 0)
        {
            var costCodes = GetCodes(costCenterCodes);
            var costCentersQuery = await _mediator.Send(new GetCostCenterByCodesQuery(costCodes!.Distinct().ToList(), null), ct);
            if (costCentersQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(costCentersQuery.Error!);
            costCenters = costCentersQuery.Value;

            if (costCodes!.Count != costCenters?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidCostCenters);
        }

        List<Project>? projects = [];
        var projectCodes = snaps.Where(x => x.ProjectCodes != null && x.ProjectCodes != "").Select(x => (string)x.ProjectCodes!).Distinct().ToList();
        if (projectCodes != null && projectCodes.Count > 0)
        {
            var pCodes = GetCodes(projectCodes);
            var projectsQuery = await _mediator.Send(new GetProjectByCodesQuery(pCodes!.Distinct().ToList(), null, true, false, null), ct);
            if (projectsQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(projectsQuery.Error!);
            projects = projectsQuery.Value;

            if (pCodes!.Count != projects?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidProjects);
        }

        List<CostCategory>? costCategories = [];
        var costCategoryCodes = snaps.Where(x => x.TransportationCostCategoryCode != null && x.TransportationCostCategoryCode != "").Select(x => x.TransportationCostCategoryCode).Distinct().ToList();
        if (costCategoryCodes != null && costCategoryCodes.Count > 0)
        {
            var costCategoryQuery = await _mediator.Send(new GetsCostCategoryByCodesQuery(costCategoryCodes!), ct);
            if (costCategoryQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(costCategoryQuery.Error!);
            costCategories = costCategoryQuery.Value?.Data;

            if (costCategoryCodes.Count != costCategories?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidCostCategories);
        }

        List<CostGroup>? costGroups = [];
        var costGroupCodes = snaps.Where(x => x.TransportationCostGroupCode != null && x.TransportationCostGroupCode != "").Select(x => x.TransportationCostGroupCode).Distinct().ToList();
        if (costGroupCodes != null && costGroupCodes.Count > 0)
        {
            var costGroupQuery = await _mediator.Send(new GetsCostGroupByCodesQuery(costGroupCodes!), ct);
            if (costGroupQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(costGroupQuery.Error!);
            costGroups = costGroupQuery.Value?.Data;

            if (costGroupCodes.Count != costGroups?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidCostGroups);
        }

        List<GetsCityByCodesModel>? cities = [];
        List<string>? cityCodes = [];
        cityCodes.AddRange(snaps.Where(x => x.StartingCityCode != null && x.StartingCityCode != "").Select(x => (string)x.StartingCityCode!).Distinct().ToList());
        cityCodes.AddRange(snaps.Where(x => x.DestinationCityCode != null && x.DestinationCityCode != "").Select(x => (string)x.DestinationCityCode!).Distinct().ToList());
        cityCodes.AddRange(snaps.Where(x => x.SecondDestinationCityCode != null && x.SecondDestinationCityCode != "").Select(x => (string)x.SecondDestinationCityCode!).Distinct().ToList());
        cityCodes = cityCodes.Distinct().ToList();
        if (cityCodes != null && cityCodes.Count > 0)
        {
            var citiesQuery = await _mediator.Send(new GetCitiesByCodesQuery(cityCodes), ct);
            if (citiesQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(citiesQuery.Error!);
            cities = citiesQuery.Value?.Data;

            if (cityCodes.Count != cities?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidCities);
        }

        if (snaps.Any(x => x.SnapRequesterCode != null && x.SnapRequesterPhoneNumber != null) ||
            snaps.Any(x => x.SnapRequesterCode == null && x.SnapRequesterPhoneNumber == null))
            return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnvalidParams);

        List<GetFilteredForSnapModel>? snapRequesters = [];
        List<string>? organizationCodes = [];
        List<string>? phoneNumbers = [];
        organizationCodes.AddRange(snaps.Where(x => x.SnapRequesterCode != null && x.SnapRequesterCode != "").Select(x => (string)x.SnapRequesterCode!).Distinct().ToList());
        phoneNumbers.AddRange(snaps.Where(x => x.SnapRequesterPhoneNumber != null && x.SnapRequesterPhoneNumber != "").Select(x => (string)x.SnapRequesterPhoneNumber!).Distinct().ToList());
        organizationCodes = organizationCodes.Distinct().ToList();
        phoneNumbers = phoneNumbers.Distinct().ToList();
        if ((phoneNumbers != null && phoneNumbers.Count > 0) || (organizationCodes != null && organizationCodes.Count > 0))
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var snapRequesterQuery = await _mediator.Send(new GetFilteredThirdPartiesForSnapQuery(organizationCodes, phoneNumbers,
                null, null, 1, phoneNumbers.Count + organizationCodes.Count), ct);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (snapRequesterQuery.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(snapRequesterQuery.Error!);
            snapRequesters = snapRequesterQuery.Value?.Data;

            if ((phoneNumbers.Count + organizationCodes.Count) != snapRequesters?.Count)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.UnValidThirdParties);
        }

        foreach (var item in snaps)
        {
            var date = DateCovertor(item.StartDate, item.StartDate, item.StartTime, item.StartTime);
            if (date.EndDate.Date < date.StartDate.Date)
                return Result.Failure<SnapRequestExcelImportsResponse>(TransportationRequestErrors.DateTimeNotValid);

            var trip = trips?.FirstOrDefault(x => x.TripCode == item.TripCode);
            var startingCity = cities.FirstOrDefault(x => x.Code == item.StartingCityCode)?.Id;
            var destinationCity = cities.FirstOrDefault(x => x.Code == item.DestinationCityCode)?.Id;
            var secondDestinationCity = cities.FirstOrDefault(x => x.Code == item.SecondDestinationCityCode)?.Id;
            var costGroup = costGroups.FirstOrDefault(x => x.CostGroupCode == item.TransportationCostGroupCode);
            var costCategory = costCategories.FirstOrDefault(x => x.CostCategoryCode == item.TransportationCostCategoryCode);
            var snapRequester = snapRequesters.FirstOrDefault(x => x.OrganizationCode == item.SnapRequesterCode || x.DefaultPhoneNo == item.SnapRequesterPhoneNumber)?.Id;

            var costCentersResponse = new List<CostCenter>();
            string[] codes = item.CostCenterCodes.Split(',');
            costCentersResponse = costCenters.Where(x => codes.Contains(x.CostCenterCode)).ToList();

            var projectsResponse = new List<Project>();
            if (!string.IsNullOrEmpty(item.ProjectCodes))
            {
                string[] pCodes = item.ProjectCodes.Split(',');
                projectsResponse = projects.Where(x => pCodes.Contains(x.ProjectCode)).ToList();
            }

            var response = await _mediator.Send(new CreateSnapCommand(
                trip!, transportation!, startingCity, destinationCity, date.StartDate, date.EndDate, item.Description, costCentersResponse,
                projectsResponse, null, null, null, null, null, null, transportation!.IsPassenger, companyId, costGroup?.Id, costCategory?.Id,
                snapRequester, secondDestinationCity, Convert.ToDecimal(item.FareAmount), Convert.ToInt32(item.StopRate), item.DestinationAddress,
                item.SecondDestinationAddress, item.PersonalPayment, item.StartingCityAddress, item.ReturnToStart, item.RecipientName),
                ct);
            if (response.IsFailure)
                return Result.Failure<SnapRequestExcelImportsResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new SnapRequestExcelImportsResponse(true);
    }

    public async Task<Result<GetSnapByIdResponse?>> GetSnapById(GetSnapByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetSnapById, id:{Id}", request.Id);
        var isValidRequest = await request.IsValidAsync<GetSnapByIdValidator, GetSnapByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSnapByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetSnapByIdWithoutIncludeQuery(request.Id), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetSnapByIdResponse>(response.Error!);
        var value = response!.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId >= 1)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var dataRecieved = await GetSnapRequestData(value, ct);

        value.RequestByName = dataRecieved.users?.FirstOrDefault(x => x.UserId == value.RequestById)?.FullName;
        value.StartingCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.StartingCityId)?.Name;
        value.DestinationCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.DestinationCityId)?.Name;
        value.SecondDestinationCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.SecondDestinationCityId)?.Name;
        value.CurrencyUnitName = dataRecieved.currencyInfo?.Name;
        value.DriverUserName = dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == value.DriverId)?.FullName;
        value.PhoneNumber = value.PhoneNumber is null ? dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == value.DriverId)?.DefaultPhoneNo : value.PhoneNumber;
        value.SnapRequesterName = dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == value.SnapRequester)?.FullName;
        value.ConfrimName = dataRecieved.users?.FirstOrDefault(x => x.UserId == value.ConfrimUserId)?.FullName;
        value.TransportationCostGroupCode = dataRecieved.costGroup?.CostGroupCode;
        value.TransportationCostGroupTitle = dataRecieved.costGroup?.CostGroupTitle;
        value.TransportationCostCategoryCode = dataRecieved.costCategory?.CostCategoryCode;
        value.TransportationCostCategoryTitle = dataRecieved.costCategory?.CostCategoryTitle;
        value.CompanyNameFa = company?.NameFa;

        return value;
    }

    public async Task<Result<GetsFilteredSnapResponse?>> GetsFilteredSnap(GetsFilteredSnapRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSnap pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsFilteredSnapValidator, GetsFilteredSnapRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredSnapResponse>(isValidRequest.Error!);

        var response = await GetFilteredSnaps(
            null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TripIds,
            request.TransportationIds,
            request.PassengerIds,
            request.RequestById,
            request.TransportationRequestStatus,
            request.PaymentType,
            request.StartDate,
            request.EndDate,
            request.FromCreateDate,
            request.ToCreateDate,
            request.RequestNumber,
            request.FromPrice,
            request.ToPrice,
            request.DriverName,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            _mediator,
            ct);
        if (response.IsFailure || response.Value.Data is null)
            return Result.Failure<GetsFilteredSnapResponse>(TransportationRequestErrors.FilteredAirPlaneNotFound);
        var values = response.Value.Data;

        return new GetsFilteredSnapResponse(values ?? new List<GetsFilteredSnapResponseModel>(0), response.Value.RowCount);
    }

    public async Task<Result<GetsSnapExcelExporterResponse?>> GetsSnapExcelExporter(GetsSnapExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSnapExcelExporter");

        var isValidRequest = await request.IsValidAsync<GetsSnapExcelExporterValidator, GetsSnapExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSnapExcelExporterResponse>(isValidRequest.Error!);

        var response = await GetFilteredSnaps(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TripIds,
            request.TransportationIds,
            request.PassengerIds,
            request.RequestById,
            request.TransportationRequestStatus,
            request.PaymentType,
            request.StartDate,
            request.EndDate,
            request.FromCreateDate,
            request.ToCreateDate,
            request.RequestNumber,
            request.FromPrice,
            request.ToPrice,
            request.DriverName,
            request.FilterData,
            request.OrderBy,
            0,
            0,
            _mediator,
            ct);
        if (response.IsFailure || response.Value.Data is null)
            return Result.Failure<GetsSnapExcelExporterResponse>(TransportationRequestErrors.FilteredAirPlaneNotFound);
        var values = response.Value.Data;

        var data = values.Adapt<List<GetsSnapExcelExporterResponseModel>>();

        var file = new FileContentResult(TransportationRequestExcels.SnapRequestToExcel(data!, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"SnapRequests-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsSnapExcelExporterResponse(file);
    }

    public async Task<Result<GetsSnapExcelEnumResponse?>> GetsSnapExcelEnum(GetsSnapExcelEnumRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<SnapExcelEnum>());
        return new GetsSnapExcelEnumResponse(result);
    }

    public async Task<Result<GetsTotalSnapPriceResponse?>> GetsTotalSnapPrice(GetsTotalSnapPriceRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTotalSnapPrice ");
        var isValidRequest = await request.IsValidAsync<GetsTotalSnapPriceValidator, GetsTotalSnapPriceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalSnapPriceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsTotalSnapPriceQuery(
            null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TripIds,
            request.TransportationIds,
            request.PassengerIds,
            request.RequestById,
            request.TransportationRequestStatus,
            request.PaymentType,
            request.StartDate,
            request.EndDate,
            request.FromCreateDate,
            request.ToCreateDate,
            request.RequestNumber,
            request.FromPrice,
            request.ToPrice,
            request.DriverName,
            request.FilterData, 0, 0), ct);

        return response.Value;
    }

}