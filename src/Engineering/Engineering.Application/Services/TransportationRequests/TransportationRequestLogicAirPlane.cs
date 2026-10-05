using Engineering.Application.Services.TransportationRequests.Commands.CreateAirplane;
using Engineering.Application.Services.TransportationRequests.Commands.DeleteTransportationRequestDocument;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAirplane;
using Engineering.Application.Services.TransportationRequests.Queries.GetAirPlaneByIdWithoutInclude;
using Engineering.Application.Services.TransportationRequests.Queries.GetsTotalAirplanePrice;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    public async Task<Result<CreateAirplaneResponse?>> CreateAirplane(CreateAirplaneRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateAirplane, TripId:{TripId},", request.TripId);

        var validationResult = await ValidateAirplaneRelatedEntities(request, null, ct);
        if (validationResult.IsFailure)
            return Result.Failure<CreateAirplaneResponse>(validationResult.Error!);

        var (startDate, endDate) = PrepareDates(request.StartDate, request.EndDate, request.StartTime, request.EndTime);

        var response = await _mediator.Send(new CreateAirplaneCommand(
            validationResult.Value?.Trip!,
            validationResult.Value?.Transportation!,
            validationResult.Value?.Transportation!.IsPassenger,
            request.StartingCityId,
            request.DestinationCityId,
            startDate ?? request.StartDate,
            endDate ?? request.EndDate,
            request.Description,
            validationResult.Value?.CostCenters!,
            validationResult.Value?.Projects,
            validationResult.Value?.ProjectOperations,
            validationResult.Value?.ProjectOperationDetails,
            request.AccountName,
            request.AccountNumber,
            request.BankId,
            request.TicketPayerId,
            request.CardNumber,
            request.CurrencyUnitId,
            validationResult.Value?.CompanyId,
            request.TransportationCostGroupId,
            request.TransportationCostCategoryId,
            request.FareAmount,
            request.DestinationAddress,
            request.PassengerId,
            request.Passenger,
            request.IBAN),
            ct);
        if (response.IsFailure)
            return Result.Failure<CreateAirplaneResponse>(response.Error!);

        if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
        {
            var documentResult = await AddDocumentsToRequest(request.DocumentUrls, response.Value!, ct);
            if (documentResult.IsFailure)
                return Result.Failure<CreateAirplaneResponse>(documentResult.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateAirplaneResponse(response.Value!.Id, true);
    }

    public async Task<Result<UpdateAirplaneResponse?>> UpdateAirplane(UpdateAirplaneRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateAirplane, TripId:{TripId},", request.TripId);

        var validationResult = await ValidateAirplaneRelatedEntities(null, request, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UpdateAirplaneResponse>(validationResult.Error!);

        var currentUser = _userProfileService.GetProfileInfo();
        if (currentUser is null)
            return Result.Failure<UpdateAirplaneResponse>(TransportationRequestErrors.UserInfoNotFound);

        var airplane = validationResult.Value?.Airplane!;
        if (currentUser.UserId != airplane.CreatorId)
            return Result.Failure<UpdateAirplaneResponse>(TransportationRequestErrors.UserIsUnValid);

        if (ValidateStatusForUpdate(airplane))
            return Result.Failure<UpdateAirplaneResponse>(TransportationRequestErrors.UnValidStatus);

        var (startDate, endDate) = PrepareDates(request.StartDate, request.EndDate, request.StartTime, request.EndTime);

        // Update airplane
        var response = await _mediator.Send(new UpdateAirplaneCommand(
            airplane.Id,
            validationResult.Value?.Trip!,
            airplane.Transportation,
            airplane.Transportation.IsPassenger,
            request.StartingCityId,
            request.DestinationCityId,
            startDate ?? request.StartDate,
            endDate ?? request.EndDate,
            request.Description,
            validationResult.Value?.CostCenters!,
            validationResult.Value?.Projects,
            validationResult.Value?.ProjectOperations,
            validationResult.Value?.ProjectOperationDetails,
            request.AccountName,
            request.AccountNumber,
            request.BankId,
            request.TicketPayerId,
            request.CardNumber,
            request.CurrencyUnitId,
            validationResult.Value?.CompanyId,
            request.TransportationCostGroupId,
            request.TransportationCostCategoryId,
            request.FareAmount,
            request.DestinationAddress,
            request.PassengerId,
            request.Passenger,
            request.IBAN), ct);

        if (response.IsFailure)
            return Result.Failure<UpdateAirplaneResponse>(response.Error!);

        foreach (var document in airplane.TransportationRequestDocuments)
        {
            var deleteResult = await _mediator.Send(new DeleteTransportationRequestDocumentCommand(document.Id), ct);
            if (deleteResult.IsFailure)
                return Result.Failure<UpdateAirplaneResponse>(deleteResult.Error!);
        }

        if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
        {
            var documentResult = await AddDocumentsToRequest(request.DocumentUrls, response.Value!, ct);
            if (documentResult.IsFailure)
                return Result.Failure<UpdateAirplaneResponse>(documentResult.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateAirplaneResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetAirplaneByIdResponse?>> GetAirplaneById(GetAirplaneByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetAirplaneById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetAirplaneByIdValidator, GetAirplaneByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetAirplaneByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetAirPlaneByIdWithoutIncludeQuery(request.Id), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetAirplaneByIdResponse>(response.Error!);
        var value = response!.Value!;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId >= 1)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var dataRecieved = await GetAirPlaneRequestData(value, ct);

        value.Creator = dataRecieved.users?.FirstOrDefault(x => x.UserId == value.CreatorId)?.FullName;
        value.ConfrimName = dataRecieved.users?.FirstOrDefault(x => x.UserId == value.ConfrimUserId)?.FullName;
        value.StartingCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.StartingCityId)?.Name;
        value.DestinationCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.DestinationCityId)?.Name;
        value.BankName = dataRecieved.bankInfo?.Name;
        value.CompanyNameFa = company?.NameFa;
        value.PassengerName = dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == value.PassengerId)?.FullName;
        value.TicketPayer = dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == value.TicketPayerId)?.FullName;
        value.TransportationCostCategoryCode = dataRecieved.costCategory?.CostCategoryCode;
        value.TransportationCostCategoryTitle = dataRecieved.costCategory?.CostCategoryTitle;
        value.TransportationCostGroupCode = dataRecieved.costGroup?.CostGroupCode;
        value.TransportationCostGroupTitle = dataRecieved.costGroup?.CostGroupTitle;
        value.CurrencyUnitName = dataRecieved.currencyInfo?.Name;

        return value;
    }

    public async Task<Result<GetsFilteredAirplaneResponse?>> GetsFilteredAirplane(GetsFilteredAirplaneRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsAirplane pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);
        var isValidRequest = await request.IsValidAsync<GetsFilteredAirplaneValidator, GetsFilteredAirplaneRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredAirplaneResponse>(isValidRequest.Error!);

        var response = await GetFilteredAirPlanes(
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
            request.RequesterById,
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
            return Result.Failure<GetsFilteredAirplaneResponse>(TransportationRequestErrors.FilteredAirPlaneNotFound);
        var values = response.Value.Data;

        return new GetsFilteredAirplaneResponse(values ?? new List<GetsFilteredAirplaneResponseModel>(0), response.Value.RowCount);
    }

    public async Task<Result<GetsAirplaneExcelExporterResponse?>> GetsAirplaneExcelExporter(GetsAirplaneExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsAirplaneExcelExporter");
        var isValidRequest = await request.IsValidAsync<GetsAirplaneExcelExporterValidator, GetsAirplaneExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsAirplaneExcelExporterResponse>(isValidRequest.Error!);

        var response = await GetFilteredAirPlanes(
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
            request.RequesterById,
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
            return Result.Failure<GetsAirplaneExcelExporterResponse>(TransportationRequestErrors.FilteredAirPlaneNotFound);
        var values = response.Value.Data;

        var data = values.Adapt<List<GetsAirplaneExcelExporterResponseModel>>();

        var file = new FileContentResult(TransportationRequestExcels.AirplaneRequestToExcel(data!, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"AirplaneRequests-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsAirplaneExcelExporterResponse(file);
    }

    public async Task<Result<GetsAirplaneExcelEnumResponse?>> GetsAirplaneExcelEnum(GetsAirplaneExcelEnumRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<AirplaneExcelEnum>());
        return new GetsAirplaneExcelEnumResponse(result);
    }

    public async Task<Result<GetsTotalAirplanePriceResponse?>> GetsTotalAirplanePrice(GetsTotalAirplanePriceRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTotalAirplanePrice");
        var isValidRequest = await request.IsValidAsync<GetsTotalAirplanePriceValidator, GetsTotalAirplanePriceRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalAirplanePriceResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsTotalAirplanePriceQuery(null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TripIds,
            request.TransportationIds,
            request.PassengerIds,
            request.RequesterById,
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
            0, 0), ct);

        return response.Value;
    }
}