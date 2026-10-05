using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByIds;
using Engineering.Application.Services.MachineTypes.Queries.GetMachineTypeById;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIdsIncludeless;
using Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIdsIncludeless;
using Engineering.Application.Services.Projects.Queries.GetsProjectByIds;
using Engineering.Application.Services.TelegramChats.Queries.GetBySnapRequestIds;
using Engineering.Application.Services.TelegramChats.Queries.GetByTransportationRequestId;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TransportationRequests.Commands.Create;
using Engineering.Application.Services.TransportationRequests.Commands.CreateTransportationRequestDocument;
using Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportExtras;
using Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.CargoReformsTransport;
using Engineering.Application.Services.TransportationRequests.Models.Create;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnap;
using Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.Model;
using Engineering.Application.Services.TransportationRequests.Models.SnapRequestExcelImports;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAfterCargoDeclaration;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.UpdateFreeCargosTransportInfo;
using Engineering.Application.Services.TransportationRequests.Models.UpdateMachineDriver;
using Engineering.Application.Services.TransportationRequests.Models.UpdateSnap;
using Engineering.Application.Services.TransportationRequests.Queries.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Queries.GetsAirplaneFiltered;
using Engineering.Application.Services.TransportationRequests.Queries.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Queries.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Queries.GetsSnapFiltered;
using Engineering.Application.Services.Transportations.Queries.GetById;
using Engineering.Application.Services.Transportations.Queries.GetByType;
using Engineering.Application.Services.Trips.Queries.GetById;
using Engineering.Application.WebServices.MetaDataServices.Banks.Models;
using Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetBankById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCityById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetsCityById;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetCostGroupById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.BillOfLadings;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Engineering.Domain.Entities.Trips;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Models;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Queries.GetActiveFilteredPreferentials;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetUserById;
using Gita.Backend.Shared.Domain.Base;
using IdentityServer.ClientSdk.Models.ThirdParty;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    private async Task<Result<(List<GetsFilteredTransportationRequestResponseModel> Data, int RowCount)>> GetFilteredTransportationRequests(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        long? tripId,
        long? billOfLadingId,
        long? transportationId,
        long? requestById,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        long? requestNumber,
        decimal? FromPrice,
        decimal? ToPrice,
        string? DriverName,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        IMediator _mediator,
        CT ct)
    {
        var response = await _mediator.Send(new GetsFilteredTransportationRequestQuery(
            ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            costGroupIds,
            costCategoryIds,
            transportationRequestStatus,
            paymentType,
            tripId,
            billOfLadingId,
            transportationId,
            requestById,
            startDate,
            endDate,
            fromDate,
            toDate,
            requestNumber,
            FromPrice,
            ToPrice,
            DriverName,
            null,
            filterData,
            orderBy,
            pageIndex,
            pageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value?.Data is null)
            return Result.Failure<(List<GetsFilteredTransportationRequestResponseModel> Data, int RowCount)>(TransportationRequestErrors.FilteredTransportationRequestNotFound);
        var values = response.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).Distinct().ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var dataRecieved = await GetTransportationRequestsData(values!, ct);

        values!.ForEach(item =>
        {
            var driver = dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == item.DriverId);
            var ticketPayer = dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == item.TicketPayerId);

            item.RequestByName = dataRecieved.users?.FirstOrDefault(x => x?.UserId == item.RequestById)?.FullName;
            item.ConfirmUser = dataRecieved.users?.FirstOrDefault(x => x?.UserId == item.ConfirmUserId)?.FullName;
            item.StartingCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.StartingCityId)?.Name;
            item.DestinationCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.DestinationCityId)?.Name;
            item.CompanyNameFa = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
            item.CurrencyUnitName = dataRecieved.currenciesInfo?.FirstOrDefault(x => x.Id == item.CurrencyUnitId)?.Name;
            item.TransportationCostGroupCode = dataRecieved.costGroups?.FirstOrDefault(x => x.Id == item.TransportationCostGroupId)?.CostGroupCode;
            item.TransportationCostGroupTitle = dataRecieved.costGroups?.FirstOrDefault(x => x.Id == item.TransportationCostGroupId)?.CostGroupTitle;
            item.TransportationCostCategoryCode = dataRecieved.costCategories?.FirstOrDefault(x => x.Id == item.TransportationCostCategoryId)?.CostCategoryCode;
            item.TransportationCostCategoryTitle = dataRecieved.costCategories?.FirstOrDefault(x => x.Id == item.TransportationCostCategoryId)?.CostCategoryTitle;
            item.DriverUserName = item.DriverId != null ? driver?.FullName : item.DriverName;
            item.TicketPayer = ticketPayer?.FullName;
            var thirdPartyPayment = item.IsAirPlane == true ? ticketPayer?.PreferentialReferenceCode : driver?.PreferentialReferenceCode;
            item.PrefernialId = dataRecieved.preferentials?.FirstOrDefault(x => x.ReferenceCode == thirdPartyPayment)?.Id;
        });

        return (values, response.Value?.RowCount ?? 0);
    }

    private async Task<Result<(List<GetsFilteredAirplaneResponseModel> Data, int RowCount)>> GetFilteredAirPlanes(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        List<long>? tripIds,
        List<long>? transportationIds,
        List<long>? passengerIds,
        long? requesterById,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromCreateDate,
        DateTime? toCreateDate,
        long? requestNumber,
        decimal? fromPrice,
        decimal? toPrice,
        string? driverName,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        IMediator _mediator,
        CT ct)
    {
        var response = await _mediator.Send(new GetsFilteredAirplaneQuery(ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            costGroupIds,
            costCategoryIds,
            tripIds,
            transportationIds,
            passengerIds,
            requesterById,
            transportationRequestStatus,
            paymentType,
            startDate,
            endDate,
            fromCreateDate,
            toCreateDate,
            requestNumber,
            fromPrice,
            toPrice,
            driverName,
            filterData,
            orderBy,
            pageIndex,
            pageSize), ct);
        if (response.IsFailure || response.Value?.Data is null)
            return Result.Failure<(List<GetsFilteredAirplaneResponseModel> Data, int RowCount)>(TransportationRequestErrors.FilteredAirPlaneNotFound);
        var values = response.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).Distinct().ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var dataRecieved = await GetAirPlaneRequestsData(values!, ct);

        values!.ForEach(item =>
        {
            item.ConfrimName = dataRecieved.users?.FirstOrDefault(x => x?.UserId == item.ConfrimUserId)?.FullName;
            item.Creator = dataRecieved.users?.FirstOrDefault(x => x?.UserId == item.CreatorId)?.FullName;
            item.StartingCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.StartingCityId)?.Name;
            item.DestinationCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.DestinationCityId)?.Name;
            item.CompanyNameFa = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
            item.CurrencyUnitName = dataRecieved.currenciesInfo?.FirstOrDefault(x => x.Id == item.CurrencyUnitId)?.Name;
            item.TransportationCostGroupCode = dataRecieved.costGroups?.FirstOrDefault(x => x.Id == item.TransportationCostGroupId)?.CostGroupCode;
            item.TransportationCostGroupTitle = dataRecieved.costGroups?.FirstOrDefault(x => x.Id == item.TransportationCostGroupId)?.CostGroupTitle;
            item.TransportationCostCategoryCode = dataRecieved.costCategories?.FirstOrDefault(x => x.Id == item.TransportationCostCategoryId)?.CostCategoryCode;
            item.TransportationCostCategoryTitle = dataRecieved.costCategories?.FirstOrDefault(x => x.Id == item.TransportationCostCategoryId)?.CostCategoryTitle;
            var payer = dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.TicketPayerId);
            item.TicketPayer = payer?.FullName;
            item.PassengerName = dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.PassengerId)?.FullName;
            item.PrefrentialId = dataRecieved.preferentials?.FirstOrDefault(x => x.ReferenceCode == payer?.PreferentialReferenceCode)?.Id;
            item.BankName = dataRecieved.bankInfos?.FirstOrDefault(x => x.Id == item.BankId)?.Name;
            item.StartTime = item.StartDate != null ? item.StartDate.Value.TimeOfDay : TimeSpan.Zero;
            item.EndTime = item.EndDate != null ? item.EndDate.Value.TimeOfDay : TimeSpan.Zero;
        });

        return (values, response.Value?.RowCount ?? 0);
    }

    private async Task<Result<(List<GetsFilteredSnapResponseModel> Data, int RowCount)>> GetFilteredSnaps(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        List<long>? tripIds,
        List<long>? transportationIds,
        List<long>? passengerIds,
        long? requestById,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromCreateDate,
        DateTime? toCreateDate,
        long? requestNumber,
        decimal? fromPrice,
        decimal? toPrice,
        string? driverName,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        IMediator _mediator,
        CT ct)
    {
        var response = await _mediator.Send(new GetsFilteredSnapQuery(
            ids,
            costCenterIds,
            projectIds,
            projectOperationIds,
            projectOperationDetailIds,
            costGroupIds,
            costCategoryIds,
            tripIds,
            transportationIds,
            passengerIds,
            requestById,
            transportationRequestStatus,
            paymentType,
            startDate,
            endDate,
            fromCreateDate,
            toCreateDate,
            requestNumber,
            fromPrice,
            toPrice,
            driverName,
            filterData,
            orderBy,
            pageIndex,
            pageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value?.Data is null)
            return Result.Failure<(List<GetsFilteredSnapResponseModel> Data, int RowCount)>(TransportationRequestErrors.FilteredSnapNotFound);
        var values = response.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).Distinct().ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var dataRecieved = await GetSnapRequestsData(values!, ct);

        values.ForEach(item =>
        {
            item.Creator = dataRecieved.users?.FirstOrDefault(x => x?.UserId == item.CreatorId)?.FullName;
            item.ConfrimName = dataRecieved.users?.FirstOrDefault(x => x?.UserId == item.ConfrimUserId)?.FullName;
            item.StartingCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.StartingCityId)?.Name;
            item.DestinationCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.DestinationCityId)?.Name;
            item.SecondDestinationCityName = dataRecieved.citiesInfo?.FirstOrDefault(x => x.Id == item.SecondDestinationCityId)?.Name;
            item.CompanyNameFa = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
            item.CurrencyUnitName = dataRecieved.currenciesInfo?.FirstOrDefault(x => x.Id == item.CurrencyUnitId)?.Name;
            item.TransportationCostGroupCode = dataRecieved.costGroups?.FirstOrDefault(x => x.Id == item.TransportationCostGroupId)?.CostGroupCode;
            item.TransportationCostGroupTitle = dataRecieved.costGroups?.FirstOrDefault(x => x.Id == item.TransportationCostGroupId)?.CostGroupTitle;
            item.TransportationCostCategoryCode = dataRecieved.costCategories?.FirstOrDefault(x => x.Id == item.TransportationCostCategoryId)?.CostCategoryCode;
            item.TransportationCostCategoryTitle = dataRecieved.costCategories?.FirstOrDefault(x => x.Id == item.TransportationCostCategoryId)?.CostCategoryTitle;
            var driver = dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == item.DriverId);
            item.DriverUserName = driver?.FullName;
            item.PrefernialId = dataRecieved.preferentials?.FirstOrDefault(x => x.ReferenceCode == driver?.PreferentialReferenceCode)?.Id;
            item.SnapRequesterName = dataRecieved.thirdParties?.FirstOrDefault(x => x?.Id == item.SnapRequester)?.FullName;
            item.NumberPlatesModel = SnapNumberPlates(item.NumberPlates);
            item.PhoneNumber = item.PhoneNumber is null ? driver?.DefaultPhoneNo : item.PhoneNumber;
            item.StartTime = item.StartDate != null ? item.StartDate.Value.TimeOfDay : TimeSpan.Zero;
            item.EndTime = item.EndDate != null ? item.EndDate.Value.TimeOfDay : TimeSpan.Zero;
        });

        return (values, response.Value?.RowCount ?? 0);
    }

    private async Task<Result<TransportationRequestBaseResponse?>> ValidateTransportationRelatedEntities(
        IMediator mediator,
        IUserInfoService userInfoService,
        TransportationRequestBaseRequest request,
        bool IsUpdated,
        CT ct)
    {
        if (ValidateNumberPlates(request.NumberPlates))
            return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.UnNumberPlates);

        if (request.CompanyId is not null && request.CompanyId >= 1)
        {
            var companyResponse = await mediator.Send(new GetCompanyByIdQuery(request.CompanyId.Value), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(companyResponse.Error!);
        }

        if (IsUpdated == false)
            if (request.DriverId is not null && request.DriverName is not null)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.DriverInfoUnValid);

        if (request.DriverId is not null && request.DriverId != 0)
        {
            var driverId = await mediator.Send(new GetWithSkillOnlyByIdsQuery(
                1, 1, new List<long>() { (long)request.DriverId }, null, false, null), ct);
            if (driverId.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.DriverIdNotValid);
        }

        if (request.PostageDate is not null && request.ReceivedDate < request.PostageDate)
            return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.PostageDateNotValid);

        if (request.BankId is not null && request.BankId > 0)
        {
            var bank = await mediator.Send(new GetBankByIdQuery((long)request.BankId), ct);
            if (bank.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.BankIdNotValid);
        }

        if (request.CurrencyUnitId is not null && request.CurrencyUnitId > 0)
        {
            var queryCurrency =
                await mediator.Send(new GetCurrencyByIdQuery((long)request.CurrencyUnitId), ct);
            if (queryCurrency.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(queryCurrency.Error!);
        }

        if (request.StartingCityId is not null && request.StartingCityId > 0)
        {
            var startingCity = await mediator.Send(new GetCityByIdQuery(request.StartingCityId.Value), ct);
            if (startingCity.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.StartingCityIdNotValid);
        }

        if (request.DestinationCityId is not null && request.DestinationCityId > 0)
        {
            var destinationCity =
                await mediator.Send(new GetCityByIdQuery(request.DestinationCityId.Value), ct);
            if (destinationCity.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.DestinationCityIdNotValid);
        }

        Transportation? transportation = null;
        if (request.TransportationId is not null && request.TransportationId > 0)
        {
            var transportationQuery = await mediator.Send(new GetTransportationByIdQuery(request.TransportationId.Value), ct);
            if (transportationQuery.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(transportationQuery.Error!);
            transportation = transportationQuery.Value;
        }

        Trip? trip = null;
        if (request.TripId is not null && request.TripId > 0)
        {
            var tripQuery = await mediator.Send(new GetTripByIdQuery(request.TripId.Value), ct);
            if (tripQuery.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(tripQuery.Error!);
            trip = tripQuery.Value;
        }

        MachineType? machineType = null;
        if (request.MachineTypeId is not null && request.MachineTypeId > 0)
        {
            var machine = await mediator.Send(new GetMachineTypeByIdQuery(request.MachineTypeId.Value), ct);
            if (machine.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(machine.Error!);
            machineType = machine.Value;
        }

        List<CostCenter>? costCenters = [];
        if (request.CostCenterIds is not null && request.CostCenterIds.Count > 0)
        {
            var costcenterQuery = await mediator.Send(new GetsCostCenterByIdsQuery(
                request.CostCenterIds, null, null, 1, request.CostCenterIds.Count), ct);
            if (costcenterQuery.IsFailure || costcenterQuery.Value?.Data == null)
                return Result.Failure<TransportationRequestBaseResponse>(costcenterQuery.Error!);
            costCenters = costcenterQuery.Value.Data;
        }

        List<Project>? projects = [];
        if (request.ProjectIds is not null && request.ProjectIds.Count > 0)
        {
            var projectQuery = await mediator.Send(new GetsProjectByIdsQuery(
                request.ProjectIds, null, null, true, false, 1, request.ProjectIds.Count), ct);
            if (projectQuery.IsFailure || projectQuery.Value?.Data == null)
                return Result.Failure<TransportationRequestBaseResponse>(projectQuery.Error!);
            projects = projectQuery.Value?.Data;
        }

        if (request.TransportationCostGroupId is not null && request.TransportationCostGroupId > 0)
        {
            var costGroupQuery = await mediator.Send(new GetCostGroupByIdQuery(request.TransportationCostGroupId.Value), ct);
            if (costGroupQuery.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(costGroupQuery.Error!);
        }

        if (request.TransportationCostCategoryId is not null && request.TransportationCostCategoryId > 0)
        {
            var costCategoryQuery = await mediator.Send(new GetCostCategoryByIdQuery(request.TransportationCostCategoryId.Value), ct);
            if (costCategoryQuery.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(costCategoryQuery.Error!);
        }

        BillOfLading? billOfLading = null;
        if (!transportation!.IsPassenger)
            if (request.BillOfLadingId is not null && request.BillOfLadingId > 0)
            {
                var getBillOfLading = await _billLogic.GetBillOfLadingByIdHandle(request.BillOfLadingId.Value, ct);
                if (getBillOfLading.IsFailure)
                    return Result.Failure<TransportationRequestBaseResponse>(getBillOfLading.Error!);
                billOfLading = getBillOfLading.Value;
            }


        List<ProjectOperation>? projectOperations = [];
        if (request.ProjectOperationIds is not null && request.ProjectOperationIds.Count > 0)
        {
            var values = await mediator.Send(new GetsProjectOperationByIdsIncludelessQuery(
                request.ProjectOperationIds, null, null, 1, request.ProjectOperationIds.Count), ct);
            if (values.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.ProjectOperationIdsNotValid);
            projectOperations = values.Value?.Data;
        }

        List<ProjectOperationDetail>? projectOperationDetails = [];
        if (request.ProjectOperationDetailIds is not null && request.ProjectOperationDetailIds.Count > 0)
        {
            var values = await mediator.Send(new GetsProjectOperationDetailByIdsIncludelessQuery(
                request.ProjectOperationDetailIds), ct);
            if (values.IsFailure)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationRequestErrors.ProjectOperationDetailIdsNotValid);
            projectOperationDetails = values.Value?.Data;
        }

        TransportationContractor? transportationContractor = null;
        if (request.TransportationContractorId is not null && request.TransportationContractorId > 0)
        {
            var transportationContractorQuery = await _transportationContractorRepository.FindById(request.TransportationContractorId.Value, ct);
            if (transportationContractorQuery is null)
                return Result.Failure<TransportationRequestBaseResponse>(TransportationContractorErrors.TransportationContractorNotFound);
            transportationContractor = transportationContractorQuery;
        }

        List<CreateTransportationWarehouseCommandModel>? warehousesCommand = [];
        List<UpdateTransportationWarehouseCommandModel>? updateWarehousesCommand = [];

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return Result.Success(new TransportationRequestBaseResponse
        {
            ProjectOperationDetails = projectOperationDetails ?? new List<ProjectOperationDetail>(),
            CostCenters = costCenters ?? new List<CostCenter>(),
            MachineType = machineType,
            ProjectOperations = projectOperations ?? new List<ProjectOperation>(),
            Projects = projects ?? new List<Project>(),
            Transportation = transportation,
            BillOfLading = billOfLading,
            Trip = trip,
            TransportationContractor = transportationContractor,
            warehousesCommand = warehousesCommand,
            updateWarehousesCommand = updateWarehousesCommand
        });
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public static (
        List<long> WarehouseIds,
        List<long> PackingIds,
        List<long> ShippingCostIds,
        List<long> ThirdPartyIds,
        List<long> ProductIds
    ) ExtractRequestAllIds(
        List<CreateTransportationRequestWarehouseModel>? Warehouses,
        List<UpdateTransportationRequestWarehouseModel>? UpdateWarehouses)
    {
        List<long> warehouseIds = new();
        List<long> packingIds = new();
        List<long> shippingCostIds = new();
        List<long> thirdPartyIds = new();
        List<long> productIds = new();

        if (Warehouses is not null && Warehouses.Any())
        {
            warehouseIds.AddRange(Warehouses.Select(x => x.WarehouseId));
            packingIds.AddRange(Warehouses.Select(x => x.PackingId));
            shippingCostIds.AddRange(Warehouses.Select(x => x.ShippingCostId));
            thirdPartyIds.AddRange(Warehouses
                .Where(x => x.ThirdPartyId.HasValue)
                .Select(x => x.ThirdPartyId!.Value));

            var createProductIds = Warehouses
                .Where(x => x.WarehouseProducts != null)
                .SelectMany(x => x.WarehouseProducts!)
                .Select(x => x.ProductId);
            productIds.AddRange(createProductIds);
        }

        if (UpdateWarehouses is not null && UpdateWarehouses.Any())
        {
            warehouseIds.AddRange(UpdateWarehouses.Select(x => x.WarehouseId));
            packingIds.AddRange(UpdateWarehouses.Select(x => x.PackingId));
            shippingCostIds.AddRange(UpdateWarehouses.Select(x => x.ShippingCostId));
            thirdPartyIds.AddRange(UpdateWarehouses
                .Where(x => x.ThirdPartyId.HasValue)
                .Select(x => x.ThirdPartyId!.Value));

            var updateProductIds = UpdateWarehouses
                .SelectMany(x =>
                    (x.WarehouseProducts ?? Enumerable.Empty<CreateTransportationRequestWarehouseProductModel>())
                    .Select(p => p.ProductId)
                    .Concat(
                        (x.UpdateWarehouseProducts ?? Enumerable.Empty<UpdateTransportationRequestWarehouseProductModel>())
                        .Select(p => p.ProductId)
                    )
                );

            productIds.AddRange(updateProductIds);
        }

        return (warehouseIds.Distinct().ToList(), packingIds.Distinct().ToList(), shippingCostIds.Distinct().ToList(),
            thirdPartyIds.Distinct().ToList(), productIds.Distinct().ToList());
    }
    private async Task<Result<AirplaneValidationResult>> ValidateAirplaneRelatedEntities(
        CreateAirplaneRequest? createRequest,
        UpdateAirplaneRequest? updateRequest,
        CT ct)
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        AirplaneRequest request = new AirplaneRequest();
        var result = new AirplaneValidationResult();

        if (createRequest is not null)
        {
            var isValidRequest = await createRequest.IsValidAsync<CreateAirplaneRequestValidator, CreateAirplaneRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<AirplaneValidationResult>(isValidRequest.Error!);
            request = createRequest.Adapt<AirplaneRequest>();
        }

        if (updateRequest is not null)
        {
            var isValidRequest = await updateRequest.IsValidAsync<UpdateAirplaneValidator, UpdateAirplaneRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<AirplaneValidationResult>(isValidRequest.Error!);
            request = updateRequest.Adapt<AirplaneRequest>();
        }

        result.CompanyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (result.CompanyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)result.CompanyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<AirplaneValidationResult>(companyResponse.Error!);
        }

        if (updateRequest is not null && request.Id is not null && request.Id > 0)
        {
            var airplaneData = await _mediator.Send(new GetAirplaneByIdQuery(request.Id.Value), ct);
            if (airplaneData.IsFailure)
                return Result.Failure<AirplaneValidationResult>(airplaneData.Error!);
            result.Airplane = airplaneData.Value;
        }

        var transportation = await _mediator.Send(new GetTransportationByTypeQuery(TransportationType.Airplane), ct);
        if (transportation.IsFailure)
            return Result.Failure<AirplaneValidationResult>(transportation.Error!);
        result.Transportation = transportation.Value;

        var trip = await _mediator.Send(new GetTripByIdQuery(request.TripId), ct);
        if (trip.IsFailure)
            return Result.Failure<AirplaneValidationResult>(trip.Error!);
        result.Trip = trip.Value;

        if (request.CostCenterIds is not null && request.CostCenterIds.Count > 0)
        {
            var costcenterQuery = await _mediator.Send(new GetsCostCenterByIdsQuery(request.CostCenterIds, null, null, 1, request.CostCenterIds.Count), ct);
            if (costcenterQuery.IsFailure || costcenterQuery.Value?.Data is null)
                return Result.Failure<AirplaneValidationResult>(costcenterQuery.Error!);
            result.CostCenters = costcenterQuery.Value.Data;
        }

        if (request.ProjectIds is not null && request.ProjectIds.Count > 0)
        {
            var projectQuery = await _mediator.Send(new GetsProjectByIdsQuery(request.ProjectIds, null, null, true, false, 1, request.ProjectIds.Count), ct);
            if (projectQuery.IsFailure || projectQuery.Value?.Data is null)
                return Result.Failure<AirplaneValidationResult>(projectQuery.Error!);
            result.Projects = projectQuery.Value.Data;
        }

        if (request.TransportationCostCategoryId > 0)
        {
            var costCategoryQuery = await _mediator.Send(new GetCostCategoryByIdQuery(request.TransportationCostCategoryId.Value), ct);
            if (costCategoryQuery.IsFailure)
                return Result.Failure<AirplaneValidationResult>(costCategoryQuery.Error!);
        }

        if (request.TransportationCostGroupId > 0)
        {
            var costGroupQuery = await _mediator.Send(new GetCostGroupByIdQuery(request.TransportationCostGroupId.Value), ct);
            if (costGroupQuery.IsFailure)
                return Result.Failure<AirplaneValidationResult>(costGroupQuery.Error!);
        }

        if (request.PassengerId is > 0)
        {
            var passengerQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.PassengerId.Value], null, false, null), ct);
            if (passengerQuery.IsFailure)
                return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.AirPlanePassengerNotValid);
        }

        if (request.TicketPayerId is > 0)
        {
            var ticketPayerQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.TicketPayerId.Value], null, false, null), ct);
            if (ticketPayerQuery.IsFailure)
                return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.AirPlaneTicketPayerNotValid);
        }
        if (request.ProjectOperationIds is { Count: > 0 })
        {
            var operationsQuery = await _mediator.Send(new GetsProjectOperationByIdsIncludelessQuery(request.ProjectOperationIds, null, result.CompanyId, 1, request.ProjectOperationIds.Count), ct);
            if (operationsQuery.IsFailure || operationsQuery.Value?.Data is null)
                return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.ProjectOperationIdsNotValid);
            result.ProjectOperations = operationsQuery.Value.Data;
        }

        if (request.ProjectOperationDetailIds is { Count: > 0 })
        {
            var detailsQuery = await _mediator.Send(new GetsProjectOperationDetailByIdsIncludelessQuery(request.ProjectOperationDetailIds), ct);
            if (detailsQuery.IsFailure || detailsQuery.Value?.Data is null)
                return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.ProjectOperationDetailIdsNotValid);
            result.ProjectOperationDetails = detailsQuery.Value.Data;
        }

        var startingCity = await _mediator.Send(new GetCityByIdQuery(request.StartingCityId), ct);
        if (startingCity.IsFailure)
            return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.StartingCityIdNotValid);

        var destinationCity = await _mediator.Send(new GetCityByIdQuery(request.DestinationCityId), ct);
        if (destinationCity.IsFailure)
            return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.DestinationCityIdNotValid);

        if (request.BankId is > 0)
        {
            var bankQuery = await _mediator.Send(new GetBankByIdQuery((long)request.BankId), ct);
            if (bankQuery.IsFailure)
                return Result.Failure<AirplaneValidationResult>(TransportationRequestErrors.BankIdNotValid);
        }

        // Validate currency
        if (request.CurrencyUnitId is > 0)
        {
            var currencyQuery = await _mediator.Send(new GetCurrencyByIdQuery((long)request.CurrencyUnitId), ct);
            if (currencyQuery.IsFailure)
                return Result.Failure<AirplaneValidationResult>(currencyQuery.Error!);
        }
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        return Result.Success(result);
    }

    private async Task<Result<SnapValidationResult>> ValidateSnapRequest(CreateSnapRequest? createRequest,
        UpdateSnapRequest? updateRequest,
        CT ct)
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        SnapRequest request = new SnapRequest();
        var result = new SnapValidationResult();

        if (createRequest is not null)
        {
            var isValidRequest = await createRequest.IsValidAsync<CreateSnapRequestValidator, CreateSnapRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<SnapValidationResult>(isValidRequest.Error!);
            request = createRequest.Adapt<SnapRequest>();
        }

        if (updateRequest is not null)
        {
            var isValidRequest = await updateRequest.IsValidAsync<UpdateSnapValidator, UpdateSnapRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<SnapValidationResult>(isValidRequest.Error!);
            request = updateRequest.Adapt<SnapRequest>();
        }

        result.CompanyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (result.CompanyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)result.CompanyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<SnapValidationResult>(companyResponse.Error!);
        }

        if (updateRequest is not null && request.Id is not null && request.Id > 0)
        {
            var snapData = await _mediator.Send(new GetSnapByIdQuery(request.Id.Value), ct);
            if (snapData.IsFailure)
                return Result.Failure<SnapValidationResult>(snapData.Error!);
            result.Snap = snapData.Value;
        }

        var transportation = await _mediator.Send(new GetTransportationByTypeQuery(TransportationType.SnappPassenger), ct);
        if (transportation.IsFailure)
            return Result.Failure<SnapValidationResult>(transportation.Error!);
        result.Transportation = transportation.Value;

        var trip = await _mediator.Send(new GetTripByIdQuery(request.TripId), ct);
        if (trip.IsFailure)
            return Result.Failure<SnapValidationResult>(trip.Error!);
        result.Trip = trip.Value;

        if (request.CostCenterIds is { Count: > 0 })
        {
            var costcenterQuery = await _mediator.Send(new GetsCostCenterByIdsQuery(request.CostCenterIds, null, null, 1, request.CostCenterIds.Count), ct);
            if (costcenterQuery.IsFailure || costcenterQuery.Value?.Data is null)
                return Result.Failure<SnapValidationResult>(costcenterQuery.Error!);
            result.CostCenters = costcenterQuery.Value.Data;
        }

        if (request.ProjectIds is { Count: > 0 })
        {
            var projectQuery = await _mediator.Send(new GetsProjectByIdsQuery(request.ProjectIds, null, null, true, false, 1, request.ProjectIds.Count), ct);
            if (projectQuery.IsFailure || projectQuery.Value?.Data is null)
                return Result.Failure<SnapValidationResult>(projectQuery.Error!);
            result.Projects = projectQuery.Value.Data;
        }

        if (request.TransportationCostCategoryId is > 0)
        {
            var costCategoryQuery = await _mediator.Send(new GetCostCategoryByIdQuery(request.TransportationCostCategoryId.Value), ct);
            if (costCategoryQuery.IsFailure)
                return Result.Failure<SnapValidationResult>(costCategoryQuery.Error!);
        }

        if (request.TransportationCostGroupId is > 0)
        {
            var costGroupQuery = await _mediator.Send(new GetCostGroupByIdQuery(request.TransportationCostGroupId.Value), ct);
            if (costGroupQuery.IsFailure)
                return Result.Failure<SnapValidationResult>(costGroupQuery.Error!);
        }

        if (request.StartingCityId is > 0)
        {
            var startingCity = await _mediator.Send(new GetCityByIdQuery(request.StartingCityId.Value), ct);
            if (startingCity.IsFailure)
                return Result.Failure<SnapValidationResult>(TransportationRequestErrors.StartingCityIdNotValid);
        }

        if (request.DestinationCityId is > 0)
        {
            var destinationCity = await _mediator.Send(new GetCityByIdQuery(request.DestinationCityId.Value), ct);
            if (destinationCity.IsFailure)
                return Result.Failure<SnapValidationResult>(TransportationRequestErrors.DestinationCityIdNotValid);
        }

        if (request.SecondDestinationCityId is > 0)
        {
            var secondDestinationCity = await _mediator.Send(new GetCityByIdQuery(request.SecondDestinationCityId.Value), ct);
            if (secondDestinationCity.IsFailure)
                return Result.Failure<SnapValidationResult>(TransportationRequestErrors.SecDestinationCityIdNotValid);
        }

        if (request.SnapRequester is > 0)
        {
            var requesterQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.SnapRequester.Value], null, false, null), ct);
            if (requesterQuery.IsFailure)
                return Result.Failure<SnapValidationResult>(TransportationRequestErrors.SnapRequesterNotValid);
        }

        if (request.DriverId is > 0)
        {
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.DriverId.Value], null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<SnapValidationResult>(TransportationRequestErrors.DriverIdNotValid);
        }

        if (request.CurrencyUnitId is > 0)
        {
            var currencyQuery = await _mediator.Send(new GetCurrencyByIdQuery((long)request.CurrencyUnitId), ct);
            if (currencyQuery.IsFailure)
                return Result.Failure<SnapValidationResult>(currencyQuery.Error!);
        }
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        return Result.Success(result);
    }

    private (DateTime? start, DateTime? end) PrepareDates(
        DateTime startDate,
        DateTime endDate,
        TimeSpan? startTime,
        TimeSpan? endTime)
    {
        DateTime? start = null;
        if (startTime != null)
            start = startDate.Date.Add(startTime.Value);

        DateTime? end = null;
        if (endTime != null)
            end = endDate.Date.Add(endTime.Value);

        return (start, end);
    }

    private async Task<Result> AddDocumentsToRequest(List<string> documentUrls,
        TransportationRequest request,
        CT ct)
    {
        foreach (var document in documentUrls)
        {
            var createDocument = await _mediator.Send(new CreateTransportationRequestDocumentCommand(document, request), ct);
            if (createDocument.IsFailure)
                return Result.Failure(createDocument.Error!);
        }
        return Result.Success();
    }

    private async Task<Result> SendMessageConfirmedTransportationRequest(TransportationRequest request,
        CT ct)
    {
        var costCenterIds = await _transportationCostRepo.GetByRequestId(request.Id, ct);
        var projectIds = await _transportationProjectRepo.GetByRequestId(request.Id, ct);
        var getTelegramChatsByTransportationRequestId = await _messengerChannelRepo.GetFltrChannel(projectIds, costCenterIds, MessengerMessageType.ConfirmTransportation, ct);
        if (getTelegramChatsByTransportationRequestId is not null &&
            getTelegramChatsByTransportationRequestId.Count > 0)
        {
            List<Guid> ids = new();
            if (request.TransportationRequestDocuments.Select(x => x.Url).ToList() is not null && request.TransportationRequestDocuments.Select(x => x.Url).ToList().Any())
                foreach (var item in request.TransportationRequestDocuments.Select(x => x.Url).ToList())
                    ids.Add(Guid.Parse(item));

            if (request.BillOfLadingImage != null)
                ids.Add(Guid.Parse(request.BillOfLadingImage));

            // دریافت نام شهرها
            List<long>? cityIds = [request.DestinationCityId ?? 0];
            cityIds.Add(request.StartingCityId ?? 0);
            cityIds = cityIds.Where(x => x > 0).Distinct().ToList();
            var destinationCityName = "";
            var startingCityName = "";
            if (cityIds is not null && cityIds.Count > 0)
            {
                var cities = await _mediator.Send(new GetsCityByIdQuery(0, 0, cityIds, null, false), ct);
                if (cities.IsSuccess && cities.Value is not null)
                {
                    destinationCityName = cities.Value.Data?.FirstOrDefault(x => x.Id == request.DestinationCityId)?.Name;
                    startingCityName = cities.Value.Data?.FirstOrDefault(x => x.Id == request.StartingCityId)?.Name;
                }
            }

            var startingCityAddress = request.StartingCityAddress;
            var destinationCityAddress = request.DestinationAddress;

            var creator = "";
            var getUser = await _mediator.Send(new GetUserByIdQuery(request.CreatorId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var currentUserId = _userProfileService.GetProfileInfo().UserId;
            var Confirmer = "";
            var getConfirmerUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getConfirmerUser.IsSuccess && getConfirmerUser.Value is not null)
                Confirmer = getConfirmerUser.Value.FullName;

            var driver = "";
            if (request.DriverId != null && request.DriverId > 0)
            {
                var getDriver = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.DriverId!.Value], null, true, null), ct);
                if (getDriver.IsSuccess && getDriver.Value is not null && getDriver.Value.Data is not null)
                    driver = getDriver.Value.Data.FirstOrDefault()?.FullName;
            }
            else
                driver = request.DriverName;

            var resultUrl = request.TransportationRequestDocuments.Select(x => x.Url).ToList() != null ? string.Join(",", request.TransportationRequestDocuments.Select(x => x.Url).ToList()) : string.Empty;

            var chats = getTelegramChatsByTransportationRequestId.DistinctBy(x => x.ChatId).ToList();

            foreach (var item in chats)
            {
                var message = TransportationRequestMessageModel(request, creator, Confirmer, startingCityName, startingCityAddress, destinationCityName, destinationCityAddress, ct);
                await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
            }
        }

        return Result.Success();
    }

    private async Task<Result> SendTelegramMessagePaidTransportationRequest(TransportationRequest request,
        CT ct)
    {
        var getTelegramChatsByTransportationRequestId = await _mediator.Send(
            new GetByTransportationRequestIdQuery(request.Id, true, Domain.Entities.TelegramChats.Enums.TelegramMessageType.TransportationPaid), ct);
        if (getTelegramChatsByTransportationRequestId.IsSuccess &&
            getTelegramChatsByTransportationRequestId.Value?.Data != null)
        {
            List<Guid> ids = new();
            if (request.TransportationRequestDocuments.Select(x => x.Url).ToList() is not null && request.TransportationRequestDocuments.Select(x => x.Url).ToList().Any())
                foreach (var item in request.TransportationRequestDocuments.Select(x => x.Url).ToList())
                    ids.Add(Guid.Parse(item));

            if (request.BillOfLadingImage != null)
                ids.Add(Guid.Parse(request.BillOfLadingImage));

            // دریافت نام شهرها
            var cities = await _mediator.Send(
                new GetsCityByIdQuery(0, 0, new List<long>() { request.DestinationCityId!.Value, request.StartingCityId!.Value },
                    null, false), ct);
            var destinationCityName = "";
            var startingCityName = "";
            if (cities.IsSuccess && cities.Value is not null)
            {
                destinationCityName = cities.Value.Data?.FirstOrDefault(x => x.Id == request.DestinationCityId)?.Name;
                startingCityName = cities.Value.Data?.FirstOrDefault(x => x.Id == request.StartingCityId)?.Name;
            }

            var startingCityAddress = request.StartingCityAddress;
            var destinationCityAddress = request.DestinationAddress;

            var creator = "";
            var getUser = await _mediator.Send(new GetUserByIdQuery(request.CreatorId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var currentUserId = _userProfileService.GetProfileInfo().UserId;
            var Confirmer = "";
            var getConfirmerUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getConfirmerUser.IsSuccess && getConfirmerUser.Value is not null)
                Confirmer = getConfirmerUser.Value.FullName;

            var driver = "";
            if (request.DriverId != null && request.DriverId > 0)
            {
                var getDriver = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.DriverId!.Value], null, true, null), ct);
                if (getDriver.IsSuccess && getDriver.Value is not null && getDriver.Value.Data is not null)
                    driver = getDriver.Value.Data.FirstOrDefault()?.FullName;
            }
            else
                driver = request.DriverName;

            var resultUrl = request.TransportationRequestDocuments.Select(x => x.Url).ToList() != null ? string.Join(",", request.TransportationRequestDocuments.Select(x => x.Url).ToList()) : string.Empty;

            var chats = getTelegramChatsByTransportationRequestId.Value.Data.DistinctBy(x => x.ChatId).ToList();

            foreach (var item in chats)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");

                var sendChats = item.TelegramChatTypes?.Where(x =>
                        x.TelegramMessageType ==
                        Domain.Entities.TelegramChats.Enums.TelegramMessageType.TransportationPaid ||
                        x.TelegramMessageType == Domain.Entities.TelegramChats.Enums.TelegramMessageType.OtherGroups)
                    .GroupBy(x => x.ChatId)
                    .Select(g => g.FirstOrDefault())
                    .ToList();

                if (sendChats is not null && sendChats.Any())
                {
                    foreach (var chat in sendChats)
                    {
                        await TelegramServicesLogic.PaidTransportationRequestMessage(
                            chat!.ChatId!,
                            request.Id,
                            request.RequestNumber,
                            request.FreightNumber,
                            destinationCityName,
                            startingCityName,
                            destinationCityAddress,
                            startingCityAddress,
                            createDateShamsi,
                            createTime,
                            creator,
                            Confirmer,
                            request.Description,
                            request.NumberPlates,
                            request.Price,
                            request.PhoneNumber,
                            request.CarSpecifications,
                            driver,
                            TimeCalculator.ConvertToShamsi(request.PaymentDate),
                            request.TransportationRequestStatus.GetEnumDescription(),
                            ids,
                            _mediator,
                            _telegramMessageHistoryLogic,
                            item.Id!.Value,
                            resultUrl,
                            chat.TelegramMessageType!.Value,
                            _authorization,
                            _messageSenderConfig,
                            ct);
                    }
                }
            }
        }

        return Result.Success();
    }

    private async Task<Result> SendTelegramMessagePaidSnap(
        List<TransportationRequest> request,
        string? thirdPartyName,
        DateTime? paymentDate,
        decimal? price,
        CT ct)
    {
        var ids = request.Select(x => x.Id).ToList();
        if (ids is not null && ids.Count > 0)
        {
            var getTelegramChatsByTransportationRequestId = await _mediator.Send(
                new GetBySnapRequestIdsQuery(ids, true, Domain.Entities.TelegramChats.Enums.TelegramMessageType.TransportationPaid), ct);
            if (getTelegramChatsByTransportationRequestId.IsSuccess &&
                getTelegramChatsByTransportationRequestId.Value?.Data != null)
            {
                List<Guid> docIds = new();
                if (request.SelectMany(z => z.TransportationRequestDocuments).Select(x => x.Url).ToList() is not null && request.SelectMany(z => z.TransportationRequestDocuments).Select(x => x.Url).ToList().Any())
                    foreach (var item in request.SelectMany(z => z.TransportationRequestDocuments).Select(x => x.Url).ToList())
                        docIds.Add(Guid.Parse(item));

                var currentUserId = _userProfileService.GetProfileInfo().UserId;
                var confirmer = "";
                var getConfirmerUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
                if (getConfirmerUser.IsSuccess && getConfirmerUser.Value is not null)
                    confirmer = getConfirmerUser.Value.FullName;

                var docs = request.Where(x => x.TransportationRequestDocuments != null && x.TransportationRequestDocuments.Count > 0).SelectMany(x => x.TransportationRequestDocuments).ToList();
                var resultUrl = docs != null ? string.Join(",", docs.Select(x => x.Url).ToList()) : string.Empty;

                foreach (var item in getTelegramChatsByTransportationRequestId.Value.Data)
                {
                    var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                    var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                    var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");

                    var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType ==
                            Domain.Entities.TelegramChats.Enums.TelegramMessageType.TransportationPaid ||
                            x.TelegramMessageType == Domain.Entities.TelegramChats.Enums.TelegramMessageType.OtherGroups)
                        .GroupBy(x => x.ChatId)
                        .Select(g => g.FirstOrDefault())
                        .ToList();

                    if (sendChats is not null && sendChats.Any())
                    {
                        foreach (var chat in sendChats)
                        {
                            await TelegramServicesLogic.PaidSnapRequestMessage(
                                chat!.ChatId,
                                request.Select(x => x.RequestNumber.ToString()).ToList(),
                                createDateShamsi,
                                createTime,
                                thirdPartyName,
                                confirmer,
                                price,
                                TimeCalculator.ConvertToShamsi(paymentDate),
                                docIds,
                                _mediator,
                                _telegramMessageHistoryLogic,
                                item.Id,
                                resultUrl,
                                chat.TelegramMessageType,
                                _authorization,
                                _messageSenderConfig,
                                ct);
                        }
                    }
                }
            }
        }

        return Result.Success();
    }

    private TransportationRequestNumberPlatesModel? NumberPlates(string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());
            string part1 = onlyNumbers.Substring(0, 2); // "12"
            string part2 = onlyNumbers.Substring(2, 3); // "345"
            string part3 = onlyNumbers.Substring(5, 2); // "67"

            return new TransportationRequestNumberPlatesModel()
            {
                Letter = onlyLetters,
                Part1 = part1,
                Part2 = part2,
                Part3 = part3,
            };
        }
        else
            return new TransportationRequestNumberPlatesModel();

    }

    private SnapNumberPlatesModel? SnapNumberPlates(string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());
            string part1 = onlyNumbers.Substring(0, 2); // "12"
            string part2 = onlyNumbers.Substring(2, 3); // "345"
            string part3 = onlyNumbers.Substring(5, 2); // "67"

            return new SnapNumberPlatesModel()
            {
                Letter = onlyLetters,
                Part1 = part1,
                Part2 = part2,
                Part3 = part3,
            };
        }
        else
            return new SnapNumberPlatesModel();

    }

    private SnapRequestDates DateCovertor(string fromDate, string toDate, string? fromTime, string? toTime)
    {
        DateTime StartDateOrg = new DateTime();
        DateTime EndDateOrg = new DateTime();

        int year1 = Convert.ToInt32(fromDate.Substring(0, 4));
        if (year1 < 2000)
            StartDateOrg = TimeCalculator.ToGeorgianDateTime(fromDate);
        else
            StartDateOrg = DateTime.Parse(fromDate);

        int year2 = Convert.ToInt32(toDate.Substring(0, 4));
        if (year2 < 2000)
            EndDateOrg = TimeCalculator.ToGeorgianDateTime(toDate);
        else
            EndDateOrg = DateTime.Parse(toDate);

        if (fromTime != null)
        {
            TimeSpan sTime = TimeSpan.Parse(fromTime);
            StartDateOrg = StartDateOrg.Add(sTime);
        }

        if (toTime != null)
        {
            TimeSpan eTime = TimeSpan.Parse(toTime);
            EndDateOrg = EndDateOrg.Add(eTime);
        }

        if (fromTime != null && toTime == null)
        {
            TimeSpan sTime = TimeSpan.Parse(fromTime);
            TimeSpan eTime = sTime + new TimeSpan(1, 0, 0);
            EndDateOrg = EndDateOrg.Add(eTime);
        }

        return new SnapRequestDates(StartDateOrg, EndDateOrg);
    }

    private List<string>? GetCodes(List<string>? values)
    {
        List<string>? costCodes = [];
        if (values is not null && values.Count > 0)
            foreach (var item in values)
            {
                string[] codes = item.Split(',');
                foreach (var item1 in codes)
                    costCodes.Add(item1);
            }

        return costCodes;
    }

    private bool ValidateNumberPlates(string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());

            var onlyLettersCount = onlyLetters.Count();
            if (onlyLetters == "الف")
                onlyLettersCount = 1;

            if (!(onlyNumbers.Count() == 7 && onlyLettersCount == 1))
                return true;
            else
                return false;
        }
        return false;
    }

    private bool ValidateStatusForUpdate(TransportationRequest? transportationRequest)
    {
        if (!(ValidateTransportationRequestStatus.AllowStatusForUpdate.Any(x => x == transportationRequest?.TransportationRequestStatus)))
            return true;
        else
            return false;
    }

    private async Task<(
        CostGroup? costGroup,
        CostCategory? costCategory,
        List<FilteredUserResponseModel>? users,
        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? cities,
        List<UserModel?>? thirdParties,
        Bank? bankInfo,
        Currency? currencyInfo,
        List<UserModel?>? ticketPayers)>
        GetTransportationRequestData(GetTransportationRequestByIdResponse value, CT ct)
    {
        CostGroup? costGroup = null;
        if (value.TransportationCostGroupId != null && value.TransportationCostGroupId > 0)
            costGroup = await WebServicesLogic.CostGroupDataReceiver(value.TransportationCostGroupId, _mediator, ct);

        CostCategory? costCategory = null;
        if (value.TransportationCostCategoryId != null && value.TransportationCostCategoryId > 0)
            costCategory = await WebServicesLogic.CostCategoryDataReceiver(value.TransportationCostCategoryId, _mediator, ct);

        List<long>? allUserIds = [value.RequestById is not null && value.RequestById > 0 ? value.RequestById.Value : 0,
            value.ConfrimUserId is not null && value.ConfrimUserId > 0 ? value.ConfrimUserId.Value : 0];
        var users = await WebServicesLogic.UserDataReceiver(allUserIds.Where(x => x > 0).Distinct().ToList(), null, _mediator, ct);

        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? cities = [];
        List<long>? allCityIds = [value.StartingCityId is not null && value.StartingCityId > 0 ? value.StartingCityId.Value : 0,
            value.DestinationCityId is not null && value.DestinationCityId > 0 ? value.DestinationCityId.Value : 0,
            value.SecondDestinationCityId is not null && value.SecondDestinationCityId > 0 ? value.SecondDestinationCityId.Value : 0];
        if (allCityIds is not null && allCityIds.Count > 0)
            cities = await WebServicesLogic.CityDataReceiver(allCityIds.Where(x => x > 0).Distinct().ToList(), _mediator, ct);

        List<UserModel?>? driversInfo = new();
        if (value.DriverId is not null && value.DriverId > 0)
        {
            var driverInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([(long)value.DriverId], null, null, _mediator, ct);
            if (driverInfos is not null && driverInfos.Count > 0)
                driversInfo.AddRange(driverInfos);
        }

        Bank? bankInfo = null;
        if (value.BankId is not null && value.BankId > 0)
            bankInfo = await WebServicesLogic.BankDataReceiver(value.BankId, _mediator, ct);

        Currency? currencyInfo = null;
        if (value.CurrencyUnitId is not null && value.CurrencyUnitId > 0)
            currencyInfo = await WebServicesLogic.CurrencyDataReceiver(value.CurrencyUnitId, _mediator, ct);

        List<UserModel?>? ticketPayersInfo = [];
        if (value.TicketPayerId is not null && value.TicketPayerId > 0)
        {
            var ticketPayerInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([(long)value.TicketPayerId], null, null, _mediator, ct);
            if (ticketPayerInfos is not null && ticketPayerInfos.Count > 0)
                ticketPayersInfo.AddRange(ticketPayerInfos);
        }

        return (costGroup, costCategory, users, cities, driversInfo, bankInfo, currencyInfo, ticketPayersInfo);
    }

    private async Task<(
        List<FilteredUserResponseModel>? users,
        List<UserModel?>? thirdParties,
        List<Bank>? bankInfos,
        List<Currency>? currencyInfos)>
        GetsTransportationRequesHistorytData(List<GetsTransportationRequestHistoryResponseModel> dataResponse, CT ct)
    {
        var bankIds = dataResponse.Where(x => x.BankId is not null && x.BankId > 0)
            .Select(x => (long)x.BankId!).Distinct().ToList();
        var currencyIds = dataResponse.Where(x => x.CurrencyId is not null && x.CurrencyId > 0)
            .Select(x => (long)x.CurrencyId!).Distinct().ToList();
        var driverIds = dataResponse.Where(x => x.DriverId is not null && x.DriverId > 0)
            .Select(x => (long)x.DriverId!).Distinct().ToList();
        List<long?>? userIds = dataResponse.SelectMany(x => new[] { x.ConfirmUserId, x.CreatorId })
            .Where(z => z is not null && z > 0).Distinct().ToList();

        var users = await WebServicesLogic.UserDataReceiver(userIds.Adapt<List<long>>().ToList(), null, _mediator, ct);
        var driverInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(driverIds, null, null, _mediator, ct);
        var bankInfo = await WebServicesLogic.BanksDataReceiver(bankIds, _mediator, ct);
        var currencyInfo = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        return (users, driverInfos, bankInfo, currencyInfo);
    }

    private async Task<(
         List<CostGroup>? costGroups,
         List<CostCategory>? costCategories,
         List<FilteredUserResponseModel>? users,
         List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? citiesInfo,
         List<UserModel?>? thirdParties,
         List<Currency>? currenciesInfo,
         List<FilteredPreferentialModel>? preferentials)>
         GetTransportationRequestsData(List<GetsFilteredTransportationRequestResponseModel> values, CT ct)
    {
        List<CostGroup>? costGroups = [];
        var costGroupIds = values!.Where(x => x.TransportationCostGroupId != null && x.TransportationCostGroupId > 0).Select(x => (long)x.TransportationCostGroupId!).Distinct().ToList();
        if (costGroupIds != null && costGroupIds.Count > 0)
            costGroups = await WebServicesLogic.CostGroupsDataReceiver(costGroupIds, _mediator, ct);

        List<CostCategory>? costCategories = [];
        var costCategoriesIds = values!.Where(x => x.TransportationCostCategoryId != null && x.TransportationCostCategoryId > 0).Select(x => (long)x.TransportationCostCategoryId!).Distinct().ToList();
        if (costCategoriesIds != null && costCategoriesIds.Count > 0)
            costCategories = await WebServicesLogic.CostCategoriesDataReceiver(costCategoriesIds, _mediator, ct);

        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? citiesInfo = [];
        List<long>? citiesIds = values.SelectMany(x => new[] { x.StartingCityId, x.DestinationCityId, x.SecondDestinationCityId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (citiesIds is not null && citiesIds.Count > 0)
            citiesInfo = await WebServicesLogic.CityDataReceiver(citiesIds, _mediator, ct);

        List<UserModel?>? thirdParties = [];
        List<long>? thirdPartiesId = values.SelectMany(x => new[] { x.DriverId, x.TicketPayerId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (thirdPartiesId is not null && thirdPartiesId.Count > 0)
        {
            var thirdPartyInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartiesId, null, null, _mediator, ct);
            if (thirdPartyInfos is not null && thirdPartyInfos.Count > 0)
                thirdParties.AddRange(thirdPartyInfos);
        }

        List<FilteredUserResponseModel>? users = [];
        var userIds = values.SelectMany(x => new[] { x.RequestById, x.ConfirmUserId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        var allIds = values?.Where(x => x.CurrencyUnitId != null && x.CurrencyUnitId != 0).Select(x => (long)x.CurrencyUnitId!).ToList();
        var currenciesInfo = await WebServicesLogic.CurrenciesDataReceiver(allIds, _mediator, ct);

        List<FilteredPreferentialModel>? prefrentials = [];
        List<Guid>? prefrentialGuids = thirdParties.Where(x => x.PreferentialReferenceCode != Guid.Empty || x.PreferentialReferenceCode != null)
            .Select(x => (Guid)x.PreferentialReferenceCode!).Distinct().ToList();
        if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
        {
            var response = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
            prefrentials = response.Value?.Data;
        }

        return (costGroups, costCategories, users, citiesInfo, thirdParties, currenciesInfo, prefrentials);
    }

    private async Task<(
         CostGroup? costGroup,
         CostCategory? costCategory,
         List<FilteredUserResponseModel>? users,
         List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? cities,
         List<UserModel?>? thirdParties,
         Bank? bankInfo,
         Currency? currencyInfo)>
         GetAirPlaneRequestData(GetAirplaneByIdResponse value, CT ct)
    {
        CostGroup? costGroup = null;
        if (value.TransportationCostGroupId != null && value.TransportationCostGroupId > 0)
            costGroup = await WebServicesLogic.CostGroupDataReceiver(value.TransportationCostGroupId, _mediator, ct);

        CostCategory? costCategory = null;
        if (value.TransportationCostCategoryId != null && value.TransportationCostCategoryId > 0)
            costCategory = await WebServicesLogic.CostCategoryDataReceiver(value.TransportationCostCategoryId, _mediator, ct);


        List<long>? allUserIds = [value.CreatorId is not null && value.CreatorId > 0 ? value.CreatorId.Value : 0,
            value.ConfrimUserId is not null && value.ConfrimUserId > 0 ? value.ConfrimUserId.Value : 0];
        var users = await WebServicesLogic.UserDataReceiver(allUserIds.Where(x => x > 0).Distinct().ToList(), null, _mediator, ct);

        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? cities = [];
        List<long>? allCityIds = [value.StartingCityId is not null && value.StartingCityId > 0 ? value.StartingCityId.Value : 0,
            value.DestinationCityId is not null && value.DestinationCityId > 0 ? value.DestinationCityId.Value : 0];
        if (allCityIds is not null && allCityIds.Count > 0)
            cities = await WebServicesLogic.CityDataReceiver(allCityIds.Where(x => x > 0).Distinct().ToList(), _mediator, ct);

        List<UserModel?>? thirdPartyInfos = [];
        List<long>? thirdPartyIds = [value.PassengerId is not null && value.PassengerId > 0 ? value.PassengerId.Value : 0,
            value.TicketPayerId is not null && value.TicketPayerId > 0 ? value.TicketPayerId.Value : 0];
        if (thirdPartyIds is not null && thirdPartyIds.Count > 0)
        {
            var thirdPartyInfo = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);
            if (thirdPartyInfo is not null)
                thirdPartyInfos.AddRange(thirdPartyInfo!);
        }

        Bank? bankInfo = null;
        if (value.BankId is not null)
            bankInfo = await WebServicesLogic.BankDataReceiver(value.BankId, _mediator, ct);

        Currency? currencyInfo = null;
        if (value.CurrencyUnitId is not null)
            currencyInfo = await WebServicesLogic.CurrencyDataReceiver(value.CurrencyUnitId, _mediator, ct);

        return (costGroup, costCategory, users, cities, thirdPartyInfos, bankInfo, currencyInfo);
    }

    private async Task<(
         List<CostGroup>? costGroups,
         List<CostCategory>? costCategories,
         List<FilteredUserResponseModel>? users,
         List<UserModel?>? thirdParties,
         List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? citiesInfo,
         List<Bank>? bankInfos,
         List<Currency>? currenciesInfo,
         List<FilteredPreferentialModel>? preferentials)>
         GetAirPlaneRequestsData(List<GetsFilteredAirplaneResponseModel> values, CT ct)
    {
        List<CostGroup>? costGroups = [];
        var costGroupIds = values!.Where(x => x.TransportationCostGroupId != null && x.TransportationCostGroupId > 0).Select(x => (long)x.TransportationCostGroupId!).Distinct().ToList();
        if (costGroupIds != null && costGroupIds.Count > 0)
            costGroups = await WebServicesLogic.CostGroupsDataReceiver(costGroupIds, _mediator, ct);

        List<CostCategory>? costCategories = [];
        var costCategoriesIds = values!.Where(x => x.TransportationCostGroupId != null && x.TransportationCostGroupId > 0).Select(x => (long)x.TransportationCostCategoryId!).Distinct().ToList();
        if (costCategoriesIds != null && costCategoriesIds.Count > 0)
            costCategories = await WebServicesLogic.CostCategoriesDataReceiver(costCategoriesIds, _mediator, ct);

        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? citiesInfo = null;
        List<long>? allCityIds = values.SelectMany(x => new[] { x.StartingCityId, x.DestinationCityId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (allCityIds is not null && allCityIds.Count != 0)
            citiesInfo = await WebServicesLogic.CityDataReceiver(allCityIds, _mediator, ct);

        List<long>? allUserIds = values.SelectMany(x => new[] { x.CreatorId, x.ConfrimUserId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        List<FilteredUserResponseModel>? users = null;
        if (allUserIds is not null && allUserIds.Count > 0)
            users = await WebServicesLogic.UserDataReceiver(allUserIds, null, _mediator, ct);

        var allIds = values?.Where(x => x.CurrencyUnitId != null && x.CurrencyUnitId != 0).Select(x => (long)x.CurrencyUnitId!).ToList();
        var currenciesInfo = await WebServicesLogic.CurrenciesDataReceiver(allIds, _mediator, ct);

        List<UserModel?>? thirdPartyInfos = [];
        List<long>? thirdPartyIds = values.SelectMany(x => new[] { x.PassengerId, x.TicketPayerId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (thirdPartyIds is not null && thirdPartyIds.Count > 0)
        {
            var thirdPartyInfo = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);
            if (thirdPartyInfo is not null)
                thirdPartyInfos.AddRange(thirdPartyInfo!);
        }

        List<Bank>? bankInfos = null;
        var bankIds = values!.Where(x => x.BankId is not null && x.BankId > 0).Select(x => (long)x.BankId!).Distinct().ToList();
        if (bankIds is not null && bankIds.Count > 0)
            bankInfos = await WebServicesLogic.BanksDataReceiver(bankIds, _mediator, ct);

        List<FilteredPreferentialModel>? prefrentials = [];
        List<Guid>? prefrentialGuids = thirdPartyInfos.Where(x => x.PreferentialReferenceCode != Guid.Empty || x.PreferentialReferenceCode != null)
            .Select(x => (Guid)x.PreferentialReferenceCode!).Distinct().ToList();
        if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
        {
            var response = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
            prefrentials = response.Value?.Data;
        }

        return (costGroups, costCategories, users, thirdPartyInfos, citiesInfo, bankInfos, currenciesInfo, prefrentials);
    }

    private async Task<(
         CostGroup? costGroup,
         CostCategory? costCategory,
         List<FilteredUserResponseModel>? users,
         List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? cities,
         List<UserModel?>? thirdParties,
         Currency? currencyInfo)>
         GetSnapRequestData(GetSnapByIdResponse value, CT ct)
    {
        CostGroup? costGroup = null;
        if (value.TransportationCostGroupId != null && value.TransportationCostGroupId > 0)
            costGroup = await WebServicesLogic.CostGroupDataReceiver(value.TransportationCostGroupId, _mediator, ct);

        CostCategory? costCategory = null;
        if (value.TransportationCostCategoryId != null && value.TransportationCostCategoryId > 0)
            costCategory = await WebServicesLogic.CostCategoryDataReceiver(value.TransportationCostCategoryId, _mediator, ct);

        List<long>? allUserIds = [value.RequestById is not null && value.RequestById > 0 ? value.RequestById.Value : 0,
            value.ConfrimUserId is not null && value.ConfrimUserId > 0 ? value.ConfrimUserId.Value : 0];
        List<FilteredUserResponseModel>? users = null;
        if (allUserIds is not null && allUserIds.Count > 0)
            users = await WebServicesLogic.UserDataReceiver(allUserIds.Where(x => x > 0).Distinct().ToList(), null, _mediator, ct);

        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? cities = null;
        List<long>? allCityIds = [value.StartingCityId is not null && value.StartingCityId > 0 ? value.StartingCityId.Value : 0,
            value.DestinationCityId is not null && value.DestinationCityId > 0 ? value.DestinationCityId.Value : 0,
            value.SecondDestinationCityId is not null && value.SecondDestinationCityId > 0 ? value.SecondDestinationCityId.Value : 0];
        if (allCityIds is not null && allCityIds.Count != 0)
            cities = await WebServicesLogic.CityDataReceiver(allCityIds, _mediator, ct);

        List<UserModel?>? thirdPartyInfos = [];
        List<long>? thirdPartyIds = [value.SnapRequester is not null && value.SnapRequester > 0 ? value.SnapRequester.Value : 0,
            value.DriverId is not null && value.DriverId > 0 ? value.DriverId.Value : 0];
        if (thirdPartyIds is not null && thirdPartyIds.Count > 0)
        {
            var thirdPartyInfo = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);
            if (thirdPartyInfo is not null)
                thirdPartyInfos.AddRange(thirdPartyInfo!);
        }

        Currency? currencyInfo = null;
        if (value.CurrencyUnitId is not null)
            currencyInfo = await WebServicesLogic.CurrencyDataReceiver(value.CurrencyUnitId, _mediator, ct);

        return (costGroup, costCategory, users, cities, thirdPartyInfos, currencyInfo);
    }

    private async Task<(
         List<CostGroup>? costGroups,
         List<CostCategory>? costCategories,
         List<FilteredUserResponseModel>? users,
         List<UserModel?>? thirdParties,
         List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? citiesInfo,
         List<Currency>? currenciesInfo,
         List<FilteredPreferentialModel>? preferentials)>
         GetSnapRequestsData(List<GetsFilteredSnapResponseModel> values, CT ct)
    {
        List<CostGroup>? costGroups = [];
        var costGroupIds = values!.Where(x => x.TransportationCostGroupId != null && x.TransportationCostGroupId > 0).Select(x => (long)x.TransportationCostGroupId!).Distinct().ToList();
        if (costGroupIds != null && costGroupIds.Count > 0)
            costGroups = await WebServicesLogic.CostGroupsDataReceiver(costGroupIds, _mediator, ct);

        List<CostCategory>? costCategories = [];
        var costCategoriesIds = values!.Where(x => x.TransportationCostCategoryId != null && x.TransportationCostCategoryId > 0).Select(x => (long)x.TransportationCostCategoryId!).Distinct().ToList();
        if (costCategoriesIds != null && costCategoriesIds.Count > 0)
            costCategories = await WebServicesLogic.CostCategoriesDataReceiver(costCategoriesIds, _mediator, ct);

        List<Engineering.Application.WebServices.MetaDataServices.Cities.Models.City>? citiesInfo = null;
        List<long>? allCityIds = values.SelectMany(x => new[] { x.StartingCityId, x.DestinationCityId, x.SecondDestinationCityId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (allCityIds is not null && allCityIds.Count != 0)
            citiesInfo = await WebServicesLogic.CityDataReceiver(allCityIds, _mediator, ct);

        List<FilteredUserResponseModel>? users = null;
        List<long>? allUserIds = values.SelectMany(x => new[] { x.CreatorId, x.ConfrimUserId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (allUserIds is not null && allUserIds.Count > 0)
            users = await WebServicesLogic.UserDataReceiver(allUserIds, null, _mediator, ct);

        var allIds = values?.Where(x => x.CurrencyUnitId != null && x.CurrencyUnitId != 0).Select(x => (long)x.CurrencyUnitId!).ToList();
        var currenciesInfo = await WebServicesLogic.CurrenciesDataReceiver(allIds, _mediator, ct);

        List<UserModel?>? thirdPartyInfos = [];
        List<long>? thirdPartyIds = values.SelectMany(x => new[] { x.SnapRequester, x.DriverId })
            .Where(x => x is not null && x > 0).Select(z => (long)z!).Distinct().ToList();
        if (thirdPartyIds is not null && thirdPartyIds.Count > 0)
        {
            var driverInfo = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds.Where(x => x > 0).Distinct().ToList(), null, null, _mediator, ct);
            if (driverInfo is not null)
                thirdPartyInfos.AddRange(driverInfo!);
        }

        List<FilteredPreferentialModel>? prefrentials = [];
        List<Guid>? prefrentialGuids = thirdPartyInfos.Where(x => x.PreferentialReferenceCode != Guid.Empty || x.PreferentialReferenceCode != null)
            .Select(x => (Guid)x.PreferentialReferenceCode!).Distinct().ToList();
        if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
        {
            var response = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
            prefrentials = response.Value?.Data;
        }

        return (costGroups, costCategories, users, thirdPartyInfos, citiesInfo, currenciesInfo, prefrentials);
    }

    private List<GetsFilteredRequesterResponseModel>? GetRequesterModelsWithFilter(List<long>? ids, List<FilteredUserResponseModel?>? users)
    {
        List<GetsFilteredRequesterResponseModel>? data = [];

        if (ids is not null && ids.Count > 0)
            foreach (var id in ids)
            {
                if (!users.Any(x => x?.UserId == id))
                    continue;

                var requester = users?.FirstOrDefault(x => x?.UserId == id);
                data.Add(new GetsFilteredRequesterResponseModel()
                {
                    Id = id,
                    FirstName = requester?.FirstName,
                    LastName = requester?.LastName,
                    DefaultPhoneNo = requester?.DefaultPhoneNo,
                    OrganizationCode = requester?.OrganizationCode,
                    IdentityNo = requester?.IdentityNo,
                    Nickname = requester?.Nickname
                });
            }

        return data;
    }

    private List<GetsFilteredRequesterResponseModel>? GetRequesterModels(List<long>? ids, List<FilteredUserResponseModel?>? users)
    {
        List<GetsFilteredRequesterResponseModel>? data = [];

        if (ids is not null && ids.Count > 0)
            foreach (var id in ids)
            {
                var requester = users?.FirstOrDefault(x => x?.UserId == id);
                data.Add(new GetsFilteredRequesterResponseModel()
                {
                    Id = id,
                    FirstName = requester?.FirstName,
                    LastName = requester?.LastName,
                    DefaultPhoneNo = requester?.DefaultPhoneNo,
                    OrganizationCode = requester?.OrganizationCode,
                    IdentityNo = requester?.IdentityNo,
                    Nickname = requester?.Nickname
                });
            }

        return data;
    }

    private string? GetFreightNumber(TransportationContractor contractor, string? lastNumber, int index)
    {
        string? result = lastNumber?.Split('/')[1];

        long number = 1;
        if (result != null)
        {
            if (contractor.SecondPrefix != null && long.Parse(result) > contractor.SecondPrefix)
            {
                number = (long.Parse(result) - (contractor.SecondPrefix ?? 0)) + 1;
            }

            if (contractor.SecondPrefix == null)
            {
                number = long.Parse(result) + 1;
            }
        }

        string? freightNumber = null;
        if (contractor?.FirstPrefix != null)
        {
            freightNumber = $"{contractor?.FirstPrefix}/{(contractor?.SecondPrefix ?? 0) + number + index}";
        }
        else
        {
            freightNumber = (number + index).ToString();
        }

        return freightNumber;
    }

    private Result<bool> ValidateShippings(List<ViewPackingShippingDetail> shippingDetails)
    {
        if (shippingDetails.Any())
        {
            bool allHaveContractor = shippingDetails.All(x => x.TransportationContractorId != null);
            var withContractor = shippingDetails.Count(x => x.TransportationContractorId != null);
            var withoutContractor = shippingDetails.Count(x => x.TransportationContractorId == null);
            var distinctContractors = shippingDetails.Select(x => x.TransportationContractorId).Distinct().Count();

            if (allHaveContractor)
            {
                if (distinctContractors > 1)
                    return Result.Failure<bool>(TransportationContractorErrors.ContractorCount);
            }
            else if (withContractor > 0 && withoutContractor > 0)
            {
                return Result.Failure<bool>(TransportationContractorErrors.ContractorCount);
            }

            var distinctDeliveryMethods = shippingDetails.Where(x => x.DeliveryMethod != null).Select(x => x.DeliveryMethod).Distinct().ToList();
            if (distinctDeliveryMethods.Count > 1)
                return Result.Failure<bool>(TransportationContractorErrors.DeliveryCount);
        }

        return Result.Success(true);
    }

    private async Task<(bool flowControl, ShippingCostsAndPriceWeightsDto? value)> GetTransportContractorPrices(
    TransportationContractor? contractor,
    CT ct)
    {
        if (contractor is null)
            return (false, null);

        ShippingCostsAndPriceWeightsDto? result = new ShippingCostsAndPriceWeightsDto();

        if (contractor.Type == TransportationContractorCalculateType.Distance)
        {
            var shippings = await _shippingCostRepository.GetShippingCostByContractor(contractor.Id, ct);
            if (shippings == null || !shippings.Any())
                return (false, null);

            result.ShippingCosts = shippings;
        }
        else if (contractor.Type == TransportationContractorCalculateType.Weight)
        {
            var prices = await _priceWeightRepository.GetPriceWeightByContractor(contractor.Id, ct);
            if (prices == null || !prices.Any())
                return (false, null);

            result.PriceWeights = prices;
        }
        else
        {
            return (true, null);
        }

        return (true, result);
    }

    private async Task<(bool flowControl, Result<List<TransportationCargoPallet>?> value)> ReformPallets(
        CargoReformsTransportRequest request,
        List<TransportationCargoPallet> palletsData,
        TransportationRequest transportationRequestData,
        List<ShippingCost>? shippingCosts,
        List<TransportationContractorPriceWeight>? priceWeights,
        CT ct)
    {
        if (request.PalletIds != null && request.PalletIds.Count > 0)
        {
            foreach (var item in palletsData)
            {
                if (transportationRequestData.TransportationContractor!.Type == TransportationContractorCalculateType.Distance)
                {
                    var packingProducts = item.TransportationRequestWarehouses.Distinct().ToList();
                    var source = packingProducts.Select(x => x.PackingSourceAddress).Distinct().FirstOrDefault();
                    var destinations = packingProducts.Select(x => x.PackingDestinationAddress).Distinct().ToList();

                    var postageDate = item.TransportationCargo.TransportationCargoPallets
                        .Where(x => x.PostageDate != null).MinBy(x => x.PostageDate)?.PostageDate;
                    shippingCosts = shippingCosts!.Where(x =>
                        x.FromDate != null &&
                        x.ToDate != null &&
                        (x.FromDate.Value.Date <= (postageDate ?? DateTime.UtcNow.Date) &&
                        x.ToDate!.Value.Date >= (postageDate ?? DateTime.UtcNow.Date)))
                        .ToList();

                    if (shippingCosts == null || shippingCosts.Count <= 0)
                        return (flowControl: false, Result.Failure<List<TransportationCargoPallet>>
                            (TransportationRequestErrors.ShippingcostNotfound));

                    var packingNumbers = item.TransportationCargo.PackingNumber;
                    var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers([packingNumbers]!, ct);
                    var invoiceOwners = invoices!.Listed(x => x.OwnerId);

                    var invoiceCompanies = invoices!.Listed(x => x.OrdererCompanyId);

                    ShippingCost? highestCost = null;
                    ViewPackingAddress? selectedAddress = null;
                    var machineShippingCosts = shippingCosts?
                   .Where(x => x.MachineTypeId == transportationRequestData.MachineTypeId && invoiceOwners.Contains(x.ThirdPartyId))
                   .ToList();

                    if (machineShippingCosts != null && machineShippingCosts.Any())
                    {
                        bool allHaveCompany = machineShippingCosts.All(x => x.ThirdPartyCompanyId != null);
                        if (allHaveCompany)
                        {
                            var companySpecificCosts = machineShippingCosts
                                .Where(x => invoiceCompanies.Contains(x.ThirdPartyCompanyId))
                                .ToList();

                            var highestPriceDestination = companySpecificCosts.MaxBy(x => x.Price);

                            if (highestPriceDestination is null)
                            {
                                return (flowControl: false, Result.Failure<List<TransportationCargoPallet>>
                            (TransportationRequestErrors.ShippingcostNotfound));
                            }

                            var address = destinations.FirstOrDefault(x => x!.CityId == highestPriceDestination!.DestinationCityId);
                            if (address is null)
                            {
                                return (flowControl: false, Result.Failure<List<TransportationCargoPallet>>
                            (TransportationRequestErrors.ShippingcostNotfound));
                            }

                            selectedAddress = address;
                            highestCost = highestPriceDestination;
                        }
                        else
                        {
                            var highestPriceDestination = destinations
                                .Join(machineShippingCosts.Where(z => z.SourceCityId == source!.CityId),
                                    dest => dest!.CityId,
                                    cost => cost.DestinationCityId,
                                    (dest, cost) => new { DestAddress = dest, ShippingCost = cost })
                                .MaxBy(x => x.ShippingCost.Price);

                            if (highestPriceDestination is null)
                            {
                                return (flowControl: false, Result.Failure<List<TransportationCargoPallet>>
                            (TransportationRequestErrors.ShippingcostNotfound));
                            }

                            selectedAddress = highestPriceDestination.DestAddress;
                            highestCost = highestPriceDestination.ShippingCost;
                        }
                    }
                    else
                    {
                        return (flowControl: false, Result.Failure<List<TransportationCargoPallet>>
                            (TransportationRequestErrors.ShippingcostNotfound));
                    }

                    var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
                    var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
                    var addresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
                    var dests = invoiceOwners.Count - 1;

                    var totalTransferPrice = (highestCost?.Price ?? 0) +
                        (((transportationRequestData.TransportationContractor.PercentageValue ?? 0m) * highestCost?.Price ?? 0m) / 100) *
                        (dests > 0 ? dests : 1);

                    item.SetTransferPrice(totalTransferPrice);
                    item.SetShippingCost(highestCost);
                }
                else if (transportationRequestData.TransportationContractor.Type == TransportationContractorCalculateType.Weight)
                {
                    var insurancePrice = 0m;
                    var insuranceData = transportationRequestData.TransportationContractor.TransportationContractorInsurances.LastOrDefault();
                    var productPrice = item.Price ?? 0;
                    if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
                    {
                        insurancePrice = insuranceData.FixedPrice;
                    }
                    else if (productPrice > insuranceData?.MaxProductPrice)
                    {
                        insurancePrice = (productPrice / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
                    }

                    var servicePrice = transportationRequestData.TransportationContractor.ServicePrice ?? 0;
                    var priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(),
                        transportationRequestData.TransportationCargoPallets!.Sum(x => x.Weight) ?? 0);

                    var totalTransferPrice = insurancePrice + (priceWeight ?? 0m) + ((priceWeight ?? 0m) *
                        (transportationRequestData.TransportationContractor.PercentageValue ?? 0) / 100) + servicePrice;

                    item.SetTransferPrice(totalTransferPrice);
                }

                item.SetTransportRequest(transportationRequestData);
                item.SetDeliveryMethod(transportationRequestData.DeliveryMethod);
                item.SetDeliveryType(transportationRequestData.DeliveryType);
                item.SetTransportationContractorId(transportationRequestData.TransportationContractor);
                item.SetPackingShippingType(PackingShippingType.Contracting);
                transportationRequestData.AddTransportationCargoPallet(item);
                await _transportationCargoPalletRepository.Update(item);
            }
        }

        return (flowControl: true, value: palletsData);
    }

    private async Task<Result<bool>> ReleasePackingData(
        TransportationRequest transportationRequestData,
        List<TransportationCargoPallet> palletsData,
        List<ShippingCost>? shippings,
        List<TransportationContractorPriceWeight>? priceWeights,
        CancellationToken ct
        )
    {
        foreach (var item in palletsData)
        {
            item.SetTransportRequest(null);
            item.SetDeliveryMethod(null);
            item.SetDeliveryType(null);
            item.SetTransportationContractorId(null);
            item.SetPackingShippingType(null);
            await _transportationCargoPalletRepository.Update(item);
        }

        if (transportationRequestData.IsDeleted == false)
        {
            var ReleasePalletIds = palletsData.Listed(x => x.Id);
            var requetsOtherPallets = transportationRequestData.TransportationCargoPallets.Where(x => !ReleasePalletIds.Contains(x.Id)).ToList();
            if (requetsOtherPallets.Count >= 1)
            {
                var totalTransferPrice = 0m;
                var taxPrice = 0m;
                decimal? priceWeight = 0m;
                var insurancePrice = 0m;
                var servicePrice = 0m;
                var productPrice = 0m;
                ShippingCost? highestCost = null;
                ViewPackingAddress? selectedAddress = null;
                var contractor = transportationRequestData.TransportationContractor;
                if (contractor!.Type == TransportationContractorCalculateType.Distance)
                {
                    var addresses = requetsOtherPallets.Where(x => x.TransportationCargo.Packing is not null)
                        .SelectMany(x => x.TransportationCargo.Packing!.PackingAddress).ToList();
                    var source = addresses.Where(x => x.Type == AddressType.Source).Distinct().FirstOrDefault();
                    var destinations = addresses.Where(x => x.Type == AddressType.Destination).Distinct().ToList();

                    var shippingCosts = shippings!.Where(x =>
                         x.FromDate != null &&
                         x.ToDate != null &&
                         (x.FromDate.Value.Date <= DateTime.Now.Date &&
                         x.ToDate!.Value.Date >= DateTime.Now.Date))
                         .ToList();

                    var packingNumbers = requetsOtherPallets.Listed(x => x.TransportationCargo.PackingNumber);
                    var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers(packingNumbers!, ct);
                    var invoiceOwners = invoices!.Listed(x => x.OwnerId);
                    var invoiceCompanies = invoices!.Listed(x => x.OrdererCompanyId);

                    var machineShippingCosts = shippingCosts?
                   .Where(x => x.MachineTypeId == transportationRequestData.MachineTypeId && invoiceOwners.Contains(x.ThirdPartyId))
                   .ToList();

                    if (machineShippingCosts != null && machineShippingCosts.Any())
                    {
                        bool allHaveCompany = machineShippingCosts.All(x => x.ThirdPartyCompanyId != null);
                        if (allHaveCompany)
                        {
                            var companySpecificCosts = machineShippingCosts
                                .Where(x => invoiceCompanies.Contains(x.ThirdPartyCompanyId))
                                .ToList();

                            var highestPriceDestination = companySpecificCosts.MaxBy(x => x.Price);
                            if (highestPriceDestination is null)
                                return Result.Failure<bool>(ShippingCostErrors.ShippingCostNotFound);

                            var address = destinations.FirstOrDefault(x => x!.CityId == highestPriceDestination!.DestinationCityId);
                            if (address is null)
                                return Result.Failure<bool>(ShippingCostErrors.ShippingCostNotFound);

                            selectedAddress = address;
                            highestCost = highestPriceDestination;
                        }
                        else
                        {
                            var highestPriceDestination = destinations
                                .Join(machineShippingCosts.Where(z => z.SourceCityId == source!.CityId),
                                    dest => dest!.CityId,
                                    cost => cost.DestinationCityId,
                                    (dest, cost) => new { DestAddress = dest, ShippingCost = cost })
                                .MaxBy(x => x.ShippingCost.Price);

                            if (highestPriceDestination is null)
                                return Result.Failure<bool>(ShippingCostErrors.ShippingCostNotFound);

                            selectedAddress = highestPriceDestination.DestAddress;
                            highestCost = highestPriceDestination.ShippingCost;
                        }
                    }
                    else
                    {
                        return Result.Failure<bool>(ShippingCostErrors.ShippingCostNotFound);
                    }

                    var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
                    var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
                    var finalAddresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
                    var dests = invoiceOwners.Count - 1;

                    totalTransferPrice = (highestCost?.Price ?? 0) +
                        (((contractor.PercentageValue ?? 0m) * highestCost?.Price ?? 0m) / 100) *
                        (dests > 0 ? dests : 1);

                    taxPrice = ((contractor.PercentageValue ?? 0m) * (highestCost?.Price ?? 0)) / 100;
                }
                else if (contractor.Type == TransportationContractorCalculateType.Weight)
                {
                    var insuranceData = contractor.TransportationContractorInsurances.LastOrDefault();
                    productPrice = requetsOtherPallets!.Sum(x => x.Price ?? 0);
                    if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
                    {
                        insurancePrice = insuranceData.FixedPrice;
                    }
                    else if (productPrice > insuranceData?.MaxProductPrice)
                    {
                        insurancePrice = (productPrice / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
                    }

                    servicePrice = contractor.ServicePrice ?? 0;
                    priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(), requetsOtherPallets!.Sum(x => x.Weight) ?? 0);
                    totalTransferPrice = insurancePrice + (priceWeight ?? 0m) + ((priceWeight ?? 0m) *
                        (contractor.PercentageValue ?? 0) / 100) + servicePrice;
                    taxPrice = ((priceWeight ?? 0m) * (contractor.TaxPercent ?? 1)) / 100;
                }

                var destination = contractor!.Type == TransportationContractorCalculateType.Distance ?
                   selectedAddress : requetsOtherPallets.OrderByDescending(x => x.Created).FirstOrDefault()?.PackingDestinationAddress;
                transportationRequestData.SetDestinationCityId(destination?.CityId ?? transportationRequestData.DestinationCityId);
                transportationRequestData.SetDestinationAddress(destination?.Address ?? transportationRequestData.DestinationAddress);
                var detail = transportationRequestData.TransportationRequestDetails.OrderByDescending(x => x.Id).FirstOrDefault();
                var extraData = new TransportationRequestDetail(
                    detail.GlobalFreightNumber, detail.ClassifiedFreightNumber, taxPrice, totalTransferPrice,
                    servicePrice, detail.InsuranceNumber, insurancePrice, highestCost?.Price ?? priceWeight ?? 0,
                    requetsOtherPallets.Sum(x => x.Price), detail.OutofRange, detail.OrderNumber, transportationRequestData);
                transportationRequestData.UpdateWarehouseTransportExtras(
                    extraData, totalTransferPrice, transportationRequestData.Volume, requetsOtherPallets.Sum(x => x.Weight) ?? 0);
                await _repository.Update(transportationRequestData);
            }
        }

        return true;
    }

    private async Task GetByIdSeedData(GetsAggregateWarehouseTransportationByIdResponse value, CT ct)
    {
        var creators = await _thirdPartyRepository.GetByUserIds([value.CreatorId!.Value], ct);
        if (creators != null && creators.Count > 0)
        {
            var creator = creators.FirstOrDefault(x => x.UserId == value.CreatorId);
            value.Creator = $"{creator?.FirstName} {creator?.LastName}";
        }
    }

    private async Task GetWarehouseTransportationPalletSeedData(GetTransportationCargoPalletResponse value, CT ct)
    {
        var contractorIds = value.TransportationContractorId != null ? new List<long> { value.TransportationContractorId.Value } : [];
        var contractors = await _transportationContractorRepository.GetByIdsIncludeLess(contractorIds, ct);

        var creatorIds = value.CreatorId != null ? new List<long> { value.CreatorId.Value } : [];
        var creators = await _thirdPartyRepository.GetByUserIds(creatorIds, ct);

        var allCityIds = new List<long>();
        allCityIds.Add(value.PackingDestinationAddressCityId ?? 0);
        allCityIds.Add(value.PackingSourceAddressCityId ?? 0);
        if (value.PalletProducts != null)
        {
            allCityIds.AddRange(value.PalletProducts.Select(p => p.PackingSourceAddressCityId ?? 0));
            allCityIds.AddRange(value.PalletProducts.Select(p => p.PackingDestinationAddressCityId ?? 0));
        }
        var distinctCityIds = allCityIds.Where(x => x > 0).Listed(x => x);

        var cities = await WebServicesLogic.CityDataReceiver(allCityIds, _mediator, ct);

        var measureIds = value.PackagingSpecMeasureUnitId != null ? new List<long> { value.PackagingSpecMeasureUnitId.Value } : [];
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var packingNumbers = value.PackingNumber != null ? new List<long> { value.PackingNumber.Value } : [];
        var invoices = await _invoiceRepository.GetInvoiceByRequestNumbers(packingNumbers, ct);
        List<ViewInvoice>? childInvoices = [];
        if (invoices != null && invoices.Count > 0)
            childInvoices = await _invoiceRepository.GetInvoiceByParentIds(invoices.Listed(x => x.Id), ct);

        GetCompanyByIdsResponseDto? companies = await GetOrdererCompanies(invoices, childInvoices, ct);

        var thirdParty = creators.FirstOrDefault(x => x.UserId == value.CreatorId);
        value.Creator = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
        if (value.PackingNumber != null)
        {

            value.PackingDestinationAddressCity = cities?.FirstOrDefault(x => x.Id == value.PackingDestinationAddressCityId)?.Name;
            value.PackingSourceAddressCity = cities?.FirstOrDefault(x => x.Id == value.PackingSourceAddressCityId)?.Name;
            value.PackagingSpecMeasureUnit = measures?.FirstOrDefault(x => x.Id == value.PackagingSpecMeasureUnitId)?.Name;
            var contractor = contractors?.FirstOrDefault(x => x.Id == value.TransportationContractorId);
            value.TransportationContractorTitle = contractor?.Title;
            value.TransportationContractorLegal = contractor?.ThirdParty?.Legal?.CompanyName;
            value.TransportationContractorName = contractor?.ThirdParty?.FirstName + " " + contractor?.ThirdParty?.LastName;

            List<string>? invoiceCodes = [];
            List<string>? invoiceNumbers = [];
            List<string>? invoiceTypes = [];
            List<long>? invoiceOrderers = [];
            var invoice = invoices?.FirstOrDefault(z => z.PackingNumber == value.PackingNumber);
            invoiceOrderers.Add(invoice?.OrdererCompanyId ?? 0);
            invoiceCodes.Add(invoice?.Code ?? string.Empty);
            invoiceNumbers.Add(invoice?.ReferringTo ?? string.Empty);
            invoiceTypes.Add(invoice?.SalesChannelType?.GetEnumDescription() ?? string.Empty);
            var childs = childInvoices?.Where(x => x.ParentId == invoice?.Id);
            if (childs != null && childs.Any())
            {
                invoiceNumbers.AddRange(childs.Listed(x => x.ReferringTo ?? string.Empty));
                invoiceTypes.AddRange(childs.Listed(x => x?.SalesChannelType?.GetEnumDescription() ?? string.Empty));
                invoiceCodes.AddRange(childs.Listed(x => x.Code ?? string.Empty));
                invoiceOrderers.AddRange(childs.Listed(x => x.OrdererCompanyId ?? 0));
            }

            if (invoiceNumbers != null && invoiceNumbers.Count > 0)
                value.InvoiceNumber = string.Join(", ", invoiceNumbers.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));

            if (invoiceCodes != null && invoiceCodes.Count > 0)
                value.ExitInvoice = string.Join(", ", invoiceCodes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));

            if (invoiceTypes != null && invoiceTypes.Count > 0)
                value.channels = string.Join(", ", invoiceTypes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));

            var companiesData = companies?.Data?.Where(x => invoiceOrderers.Distinct().Contains(x.Id)).ToList();
            if (companiesData != null && companiesData.Count > 0)
            {
                value.CompanyCode = string.Join(", ", companiesData?.Listed(x => x.Code) ?? []);
                value.CompanyName = string.Join(", ", companiesData?.Listed(x => x.NameFa) ?? []);
            }
        }

        if (value.PalletProducts != null && value.PalletProducts.Count > 0)
        {
            value.PalletProducts.ForEach(p =>
            {
                p.PackingDestinationAddressCity = cities?.FirstOrDefault(x => x.Id == p.PackingDestinationAddressCityId)?.Name;
                p.PackingSourceAddressCity = cities?.FirstOrDefault(x => x.Id == p.PackingSourceAddressCityId)?.Name;
            });
        }
    }

    private async Task GetCargoWarehouseTransportationPalletSeedData(GetTransportationCargoByIdResponse value, CT ct)
    {
        var contractorIds = value.TransportationContractorId != null ? new List<long> { value.TransportationContractorId.Value } : [];
        var contractors = await _transportationContractorRepository.GetByIdsIncludeLess(contractorIds, ct);

        var creatorIds = value.CreatorId != null ? new List<long> { value.CreatorId.Value } : [];
        var creators = await _thirdPartyRepository.GetByUserIds(creatorIds, ct);

        var packingNumbers = value.PackingNumber != null ? new List<long> { value.PackingNumber.Value } : [];
        var invoices = await _invoiceRepository.GetInvoiceByRequestNumbers(packingNumbers, ct);
        List<ViewInvoice>? childInvoices = [];
        if (invoices != null && invoices.Count > 0)
            childInvoices = await _invoiceRepository.GetInvoiceByParentIds(invoices.Listed(x => x.Id), ct);

        GetCompanyByIdsResponseDto? companies = await GetOrdererCompanies(invoices, childInvoices, ct);

        var thirdParty = creators.FirstOrDefault(x => x.UserId == value.CreatorId);
        value.Creator = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
        if (value.PackingNumber != null)
        {
            var contractor = contractors?.FirstOrDefault(x => x.Id == value.TransportationContractorId);
            value.TransportationContractorTitle = contractor?.Title;
            value.TransportationContractorLegal = contractor?.ThirdParty?.Legal?.CompanyName;
            value.TransportationContractorName = contractor?.ThirdParty?.FirstName + " " + contractor?.ThirdParty?.LastName;

            List<string>? invoiceCodes = [];
            List<string>? invoiceNumbers = [];
            List<string>? invoiceTypes = [];
            List<long>? invoiceOrderers = [];
            var invoice = invoices?.FirstOrDefault(z => z.PackingNumber == value.PackingNumber);
            invoiceOrderers.Add(invoice?.OrdererCompanyId ?? 0);
            invoiceCodes.Add(invoice?.Code ?? string.Empty);
            invoiceNumbers.Add(invoice?.ReferringTo ?? string.Empty);
            invoiceTypes.Add(invoice?.SalesChannelType?.GetEnumDescription() ?? string.Empty);
            var childs = childInvoices?.Where(x => x.ParentId == invoice?.Id);
            if (childs != null && childs.Any())
            {
                invoiceNumbers.AddRange(childs.Listed(x => x.ReferringTo ?? string.Empty));
                invoiceTypes.AddRange(childs.Listed(x => x?.SalesChannelType?.GetEnumDescription() ?? string.Empty));
                invoiceCodes.AddRange(childs.Listed(x => x.Code ?? string.Empty));
                invoiceOrderers.AddRange(childs.Listed(x => x.OrdererCompanyId ?? 0));
            }

            if (invoiceNumbers != null && invoiceNumbers.Count > 0)
                value.InvoiceNumber = string.Join(", ", invoiceNumbers.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));

            if (invoiceCodes != null && invoiceCodes.Count > 0)
                value.ExitInvoice = string.Join(", ", invoiceCodes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));

            if (invoiceTypes != null && invoiceTypes.Count > 0)
                value.channels = string.Join(", ", invoiceTypes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));

            var companiesData = companies?.Data?.Where(x => invoiceOrderers.Distinct().Contains(x.Id)).ToList();
            if (companiesData != null && companiesData.Count > 0)
            {
                value.CompanyCode = string.Join(", ", companiesData?.Listed(x => x.Code) ?? []);
                value.CompanyName = string.Join(", ", companiesData?.Listed(x => x.NameFa) ?? []);
            }
        }
    }

    private async Task GetWarehouseTransportationSeedData(List<GetsTransportationCargoPalletResponseModel> values, CT ct)
    {
        var contractorIds = values.NullListed(x => x.TransportationContractorId);
        var contractors = await _transportationContractorRepository.GetByIdsIncludeLess(contractorIds, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await _thirdPartyRepository.GetByUserIds(creatorIds, ct);

        var allCityIds = values
            .Select(r => r.PackingDestinationAddressCityId)
            .Union(values.Select(r => r.PackingDestinationAddressCityId))
            .Union(values.SelectMany(r => r.PalletProducts!.Select(p => p.PackingSourceAddressCityId)))
            .Union(values.SelectMany(r => r.PalletProducts!.Select(p => p.PackingDestinationAddressCityId)))
            .NullListed(x => x);
        var cities = await WebServicesLogic.CityDataReceiver(allCityIds, _mediator, ct);

        var measureIds = values.NullListed(x => x.PackagingSpecMeasureUnitId);
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var packingNumbers = values.NullListed(x => x.PackingNumber);
        var invoices = await _invoiceRepository.GetInvoiceByRequestNumbers(packingNumbers, ct);
        List<ViewInvoice>? childInvoices = [];
        if (invoices != null && invoices.Count > 0)
            childInvoices = await _invoiceRepository.GetInvoiceByParentIds(invoices.Listed(x => x.Id), ct);

        GetCompanyByIdsResponseDto? companies = await GetOrdererCompanies(invoices, childInvoices, ct);

        values.ForEach(item =>
        {
            var thirdParty = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
            if (item.PackingNumber != null)
            {
                List<string>? invoiceCodes = [];
                List<string>? invoiceNumbers = [];
                List<string>? invoiceTypes = [];
                List<long>? invoiceOrderers = [];
                var invoice = invoices?.FirstOrDefault(z => z.PackingNumber == item.PackingNumber);
                invoiceOrderers.Add(invoice?.OrdererCompanyId ?? 0);
                invoiceCodes.Add(invoice?.Code ?? string.Empty);
                invoiceNumbers.Add(invoice?.ReferringTo ?? string.Empty);
                invoiceTypes.Add(invoice?.SalesChannelType?.GetEnumDescription() ?? string.Empty);

                item.PackingDestinationAddressCity = cities?.FirstOrDefault(x => x.Id == item.PackingDestinationAddressCityId)?.Name;
                item.PackingSourceAddressCity = cities?.FirstOrDefault(x => x.Id == item.PackingSourceAddressCityId)?.Name;
                item.PackagingSpecMeasureUnit = measures?.FirstOrDefault(x => x.Id == item.PackagingSpecMeasureUnitId)?.Name;
                var contractor = contractors?.FirstOrDefault(x => x.Id == item.TransportationContractorId);
                item.TransportationContractorTitle = contractor?.Title;
                item.TransportationContractorLegal = contractor?.ThirdParty?.Legal?.CompanyName;
                item.TransportationContractorName = contractor?.ThirdParty?.FirstName + " " + contractor?.ThirdParty?.LastName;

                var childs = childInvoices?.Where(x => x.ParentId == invoice?.Id);
                if (childs != null && childs.Any())
                {
                    invoiceNumbers.AddRange(childs.Listed(x => x.ReferringTo ?? string.Empty));
                    invoiceTypes.AddRange(childs.Listed(x => x?.SalesChannelType?.GetEnumDescription() ?? string.Empty));
                    invoiceCodes.AddRange(childs.Listed(x => x.Code ?? string.Empty));
                    invoiceOrderers.AddRange(childs.Listed(x => x.OrdererCompanyId ?? 0));
                }

                if (invoiceNumbers != null && invoiceNumbers.Count > 0)
                {
                    item.InvoiceNumber = string.Join(", ", invoiceNumbers.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                }

                if (invoiceCodes != null && invoiceCodes.Count > 0)
                {
                    item.ExitInvoice = string.Join(", ", invoiceCodes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                }

                if (invoiceTypes != null && invoiceTypes.Count > 0)
                {
                    item.channels = string.Join(", ", invoiceTypes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                }

                var companiesData = companies?.Data?.Where(x => invoiceOrderers.Distinct().Contains(x.Id)).ToList();
                if (companiesData != null && companiesData.Count > 0)
                {
                    item.CompanyCode = string.Join(", ", companiesData?.Listed(x => x.Code) ?? []);
                    item.CompanyName = string.Join(", ", companiesData?.Listed(x => x.NameFa) ?? []);
                }
            }

            if (item.PalletProducts != null && item.PalletProducts.Count > 0)
            {
                item.PalletProducts.ForEach(p =>
                {
                    p.PackingDestinationAddressCity = cities?.FirstOrDefault(x => x.Id == p.PackingDestinationAddressCityId)?.Name;
                    p.PackingSourceAddressCity = cities?.FirstOrDefault(x => x.Id == p.PackingSourceAddressCityId)?.Name;
                });
            }
        });
    }

    private async Task GetCargoWarehouseTransportationSeedData(List<GetsFilteredTransportationCargoResponseModel> values, CT ct)
    {
        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await _thirdPartyRepository.GetByUserIds(creatorIds, ct);

        var packingNumbers = values.NullListed(x => x.PackingNumber);
        var invoices = await _invoiceRepository.GetInvoiceByRequestNumbers(packingNumbers, ct);
        List<ViewInvoice>? childInvoices = [];
        if (invoices != null && invoices.Count > 0)
            childInvoices = await _invoiceRepository.GetInvoiceByParentIds(invoices.Listed(x => x.Id), ct);

        GetCompanyByIdsResponseDto? companies = await GetOrdererCompanies(invoices, childInvoices, ct);

        values.ForEach(item =>
        {
            var thirdParty = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
            if (item.PackingNumber != null)
            {
                List<string>? invoiceCodes = [];
                List<string>? invoiceNumbers = [];
                List<string>? invoiceTypes = [];
                List<long>? invoiceOrderers = [];
                var invoice = invoices?.FirstOrDefault(z => z.PackingNumber == item.PackingNumber);
                invoiceOrderers.Add(invoice?.OrdererCompanyId ?? 0);
                invoiceCodes.Add(invoice?.Code ?? string.Empty);
                invoiceNumbers.Add(invoice?.ReferringTo ?? string.Empty);
                invoiceTypes.Add(invoice?.SalesChannelType?.GetEnumDescription() ?? string.Empty);

                var childs = childInvoices?.Where(x => x.ParentId == invoice?.Id);
                if (childs != null && childs.Any())
                {
                    invoiceNumbers.AddRange(childs.Listed(x => x.ReferringTo ?? string.Empty));
                    invoiceTypes.AddRange(childs.Listed(x => x?.SalesChannelType?.GetEnumDescription() ?? string.Empty));
                    invoiceCodes.AddRange(childs.Listed(x => x.Code ?? string.Empty));
                    invoiceOrderers.AddRange(childs.Listed(x => x.OrdererCompanyId ?? 0));
                }

                if (invoiceNumbers != null && invoiceNumbers.Count > 0)
                {
                    item.InvoiceNumber = string.Join(", ", invoiceNumbers.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                }

                if (invoiceCodes != null && invoiceCodes.Count > 0)
                {
                    item.ExitInvoice = string.Join(", ", invoiceCodes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                }

                if (invoiceTypes != null && invoiceTypes.Count > 0)
                {
                    item.Channels = string.Join(", ", invoiceTypes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                }

                var companiesData = companies?.Data?.Where(x => invoiceOrderers.Distinct().Contains(x.Id)).ToList();
                if (companiesData != null && companiesData.Count > 0)
                {
                    item.CompanyCode = string.Join(", ", companiesData?.Listed(x => x.Code) ?? []);
                    item.CompanyName = string.Join(", ", companiesData?.Listed(x => x.NameFa) ?? []);
                }
            }
        });
    }

    private async Task GetWarehouseTransportationSeedData(List<GetsWarehouseTransportationResponseModel> values, CT ct)
    {
        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await _thirdPartyRepository.GetByUserIds(creatorIds, ct);

        var inoiceCodes = values.Where(x => x.PackingData != null && x.PackingData.Count > 0)
            .SelectMany(x => x.PackingData!).NullListed(x => x.PackingNumber);

        var invoices = await _invoiceRepository.GetInvoiceByRequestNumbers(inoiceCodes, ct);
        List<ViewInvoice>? childInvoices = [];
        if (invoices != null && invoices.Count > 0)
            childInvoices = await _invoiceRepository.GetInvoiceByParentIds(invoices.Listed(x => x.Id), ct);

        GetCompanyByIdsResponseDto? companies = await GetOrdererCompanies(invoices, childInvoices, ct);

        values.ForEach(item =>
        {
            var thirdParty = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
            item.PackingData?.ForEach(x =>
            {
                if (x.PackingNumber != null)
                {
                    List<string>? invoiceNumbers = [];
                    List<string>? invoiceTypes = [];
                    var invoice = invoices?.FirstOrDefault(z => z.PackingNumber == x.PackingNumber);
                    x.ExitInvoice = invoice?.Code;
                    invoiceNumbers.Add(invoice?.ReferringTo ?? string.Empty);
                    invoiceTypes.Add(invoice?.SalesChannelType?.GetEnumDescription() ?? string.Empty);

                    var childs = childInvoices?.Where(x => x.ParentId == invoice?.Id);
                    if (childs != null && childs.Any())
                    {
                        x.Invoices = string.Join(", ", childs!.Listed(x => x.Code));
                        invoiceNumbers.AddRange(childs.Listed(x => x.ReferringTo ?? string.Empty));
                        invoiceTypes.AddRange(childs.Listed(x => x?.SalesChannelType?.GetEnumDescription() ?? string.Empty));
                    }

                    if (invoiceNumbers != null && invoiceNumbers.Count > 0)
                    {
                        x.InvoiceNumber = string.Join(", ", invoiceNumbers.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                    }

                    if (invoiceTypes != null && invoiceTypes.Count > 0)
                    {
                        x.Channel = string.Join(", ", invoiceTypes.Where(x => !string.IsNullOrEmpty(x))!.Listed(x => x));
                    }

                    if (companies?.Data != null && companies.Data.Count > 0)
                    {
                        x.CompanyCode = string.Join(", ", companies.Data.Listed(x => x.Code));
                        x.CompanyName = string.Join(", ", companies.Data.Listed(x => x.NameFa));
                    }
                }
            });
        });
    }

    private async Task AggregateSeedData(List<GetsAggregateWarehouseTransportationResponseModel> values, CT ct)
    {
        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await _thirdPartyRepository.GetByUserIds(creatorIds, ct);
        values.ForEach(item =>
        {
            var thirdParty = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
        });
    }
#pragma warning disable CS0168 // Variable is declared but never used

    private async Task<GetCompanyByIdsResponseDto?> GetOrdererCompanies(List<ViewInvoice>? invoices, List<ViewInvoice>? childInvoices, CT ct)
    {
        List<long>? ordererCompany = [];
        if (childInvoices != null && childInvoices.Count > 0)
        {
            ordererCompany.AddRange(childInvoices?.NullListed(x => x.OrdererCompanyId) ?? []);
        }
        else
        {
            ordererCompany.AddRange(invoices?.NullListed(x => x.OrdererCompanyId) ?? []);
        }

        GetCompanyByIdsResponseDto? companies = null;
        if (ordererCompany is not null && ordererCompany.Count > 0)
        {
            try
            {
                companies = await _companyClient.GetCompanyByIds(new GetCompanyByIdsRequestDto()
                {
                    Ids = ordererCompany.Distinct().ToList(),
                    PageIndex = 1,
                    PageSize = ordererCompany.Distinct().Count()
                }, ct);
            }
            catch (Exception ex)
            {
                return companies;
            }
        }

        return companies;
    }
#pragma warning restore CS0168 // Variable is declared but never used
    private async Task<(bool flowControl, Result<UpdateMachineDriverResponse?> value)> ChangeDistanceTypePrice(
        UpdateMachineDriverRequest request, TransportationRequest entity, CT ct)
    {
        List<ShippingCost>? shippingCosts = [];
        List<TransportationContractorPriceWeight>? priceWeights = [];
        var (flowControl, pricesData) = await GetTransportContractorPrices(entity.TransportationContractor, ct);
        if (!flowControl)
            return (flowControl: false, value: Result.Failure<UpdateMachineDriverResponse>(ShippingCostErrors.ShippingCostNotFound!));

        if (entity!.TransportationContractor?.Type == TransportationContractorCalculateType.Distance)
        {
            var shippingData = await GetShippingCostData(request, entity, pricesData, ct);
            if (shippingData.IsFailure)
                return (flowControl: false, value: Result.Failure<UpdateMachineDriverResponse>(shippingData.Error!));

            var volume = entity.Volume ?? 0;
            var updateTransport = await _mediator.Send(new UpdateTransportExtrasCommand(entity,
                shippingData.Value.extraInfo, shippingData.Value.totalTransferPrice, volume), ct);
            if (updateTransport.IsFailure)
                return (flowControl: false, value: Result.Failure<UpdateMachineDriverResponse>(updateTransport.Error!));

            foreach (var item in entity.TransportationCargoPallets.ToList())
            {
                var shippingPallet = await GetPalletShippingCostData(request, entity, pricesData, item, ct);
                if (shippingPallet.IsFailure)
                    return (flowControl: false, value: Result.Failure<UpdateMachineDriverResponse>(shippingPallet.Error!));

                item.UpdateShippingPrice(shippingPallet.Value.totalTransferPrice);
                item.SetShippingCost(shippingPallet.Value.shippingCost);

                await _transportationCargoPalletRepository.Update(item);
            }
        }

        return (flowControl: true, value: null);
    }

    private async Task<Result<(ShippingCost shippingCost, decimal totalTransferPrice, TransportationRequestDetail extraInfo)>> GetShippingCostData(
        UpdateMachineDriverRequest request, TransportationRequest entity, ShippingCostsAndPriceWeightsDto pricesData, CT ct)
    {
        List<ShippingCost>? shippingCosts;
        var addresses = entity.TransportationCargoPallets.Where(x => x.TransportationCargo.Packing is not null)
                        .SelectMany(x => x.TransportationCargo.Packing!.PackingAddress).ToList();
        var source = addresses.Where(x => x.Type == AddressType.Source).Distinct().FirstOrDefault();
        var destinations = addresses.Where(x => x.Type == AddressType.Destination).Distinct().ToList();

        var postDate = entity.TransportationCargoPallets.Listed(x => x.PostageDate).MinBy(x => x);
        shippingCosts = pricesData.ShippingCosts!.Where(x =>
            x.FromDate != null &&
            x.ToDate != null &&
            (x.FromDate.Value.Date <= (postDate ?? DateTime.Now.Date) &&
            x.ToDate!.Value.Date >= (postDate ?? DateTime.Now.Date)))
            .ToList();

        if (shippingCosts is null || shippingCosts.Count <= 0)
            return Result.Failure<(ShippingCost, decimal, TransportationRequestDetail)>(ShippingCostErrors.ShippingCostNotFound);

        ShippingCost? highestCost = null;
        ViewPackingAddress? selectedAddress = null;
        var machineShippingCosts = shippingCosts?
            .Where(x => x.MachineTypeId == request.MachineTypeId && x.SourceCityId == source!.CityId)
            .ToList();

        var packingNumbers = entity.TransportationCargoPallets.Listed(x => x.TransportationCargo.PackingNumber);
        var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers(packingNumbers!, ct);
        var invoiceOwners = invoices!.Listed(x => x.OwnerId);

        if (machineShippingCosts != null && machineShippingCosts.Any())
        {
            var highestPriceDestination = destinations
            .Join(machineShippingCosts,
                a => a.CityId,
                s => s.DestinationCityId,
                (a, s) =>
                    new { DestAddress = a, ShippingCost = s })
            .Where(x => x.ShippingCost.ThirdPartyId == null ||
            invoiceOwners.Contains(x.ShippingCost.ThirdPartyId)).ToList()
            .MaxBy(x => x.ShippingCost.Price);
            selectedAddress = highestPriceDestination?.DestAddress;
            highestCost = highestPriceDestination?.ShippingCost;
        }

        var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
        var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
        var shippingAddresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
        var dests = invoiceOwners.Count - 1;

        var totalTransferPrice = (highestCost?.Price ?? 0) +
            (((entity.TransportationContractor.PercentageValue ?? 0m) * highestCost?.Price ?? 0m) / 100) *
            (dests > 0 ? dests : 1);

        var taxPrice = ((entity.TransportationContractor.PercentageValue ?? 0m) * (highestCost?.Price ?? 0)) / 100;

        var extraInfo = new TransportationRequestDetail(null, null, taxPrice, totalTransferPrice, entity.TransportationContractor.ServicePrice,
            null, 0, highestCost?.Price ?? 0, entity.TransportationCargoPallets.Sum(x => x.Price ?? 0), 0, null, entity);

        return (highestCost, totalTransferPrice, extraInfo);
    }

    private async Task<Result<(ShippingCost shippingCost, decimal totalTransferPrice)>> GetPalletShippingCostData(
        UpdateMachineDriverRequest request, TransportationRequest entity, ShippingCostsAndPriceWeightsDto pricesData, TransportationCargoPallet? pallet, CT ct)
    {
        List<ShippingCost>? shippingCosts;
        var addresses = pallet!.TransportationRequestWarehouses.ToList();
        var source = addresses.Where(x => x.PackingSourceAddress is not null).Listed(x => x.PackingSourceAddress).Distinct().FirstOrDefault();
        var destinations = addresses.Where(x => x.PackingDestinationAddress is not null).Listed(x => x.PackingDestinationAddress).Distinct().ToList();

        var postDate = entity.TransportationCargoPallets.Listed(x => x.PostageDate).MinBy(x => x);
        shippingCosts = pricesData.ShippingCosts!.Where(x =>
            x.FromDate != null &&
            x.ToDate != null &&
            (x.FromDate.Value.Date <= (postDate ?? DateTime.Now.Date) &&
            x.ToDate!.Value.Date >= (postDate ?? DateTime.Now.Date)))
            .ToList();

        if (shippingCosts is null || shippingCosts.Count <= 0)
            return Result.Failure<(ShippingCost, decimal)>(ShippingCostErrors.ShippingCostNotFound);

        var packingNumbers = new List<long> { pallet.TransportationCargo.PackingNumber };
        var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers(packingNumbers!, ct);
        var invoiceOwners = invoices!.Listed(x => x.OwnerId);
        var invoiceCompanies = invoices!.Listed(x => x.OrdererCompanyId);

        ShippingCost? highestCost = null;
        ViewPackingAddress? selectedAddress = null;
        var machineShippingCosts = shippingCosts?
       .Where(x => x.MachineTypeId == entity.MachineTypeId && invoiceOwners.Contains(x.ThirdPartyId))
       .ToList();

        if (machineShippingCosts != null && machineShippingCosts.Any())
        {
            bool allHaveCompany = machineShippingCosts.All(x => x.ThirdPartyCompanyId != null);
            if (allHaveCompany)
            {
                var companySpecificCosts = machineShippingCosts
                    .Where(x => invoiceCompanies.Contains(x.ThirdPartyCompanyId))
                    .ToList();

                var highestPriceDestination = companySpecificCosts.MaxBy(x => x.Price);

                if (highestPriceDestination is null)
                {
                    return Result.Failure<(ShippingCost, decimal)>(ShippingCostErrors.ShippingCostNotFound);
                }

                var address = destinations.FirstOrDefault(x => x!.CityId == highestPriceDestination!.DestinationCityId);
                if (address is null)
                {
                    return Result.Failure<(ShippingCost, decimal)>(ShippingCostErrors.ShippingCostNotFound);
                }

                selectedAddress = address;
                highestCost = highestPriceDestination;
            }
            else
            {
                var highestPriceDestination = destinations
                    .Join(machineShippingCosts.Where(z => z.SourceCityId == source!.CityId),
                        dest => dest!.CityId,
                        cost => cost.DestinationCityId,
                        (dest, cost) => new { DestAddress = dest, ShippingCost = cost })
                    .MaxBy(x => x.ShippingCost.Price);

                if (highestPriceDestination is null)
                {
                    return Result.Failure<(ShippingCost, decimal)>(ShippingCostErrors.ShippingCostNotFound);
                }

                selectedAddress = highestPriceDestination.DestAddress;
                highestCost = highestPriceDestination.ShippingCost;
            }
        }
        else
        {
            return Result.Failure<(ShippingCost, decimal)>(ShippingCostErrors.ShippingCostNotFound);
        }

        var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
        var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
        var shippingAddresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
        var dests = invoiceOwners.Count - 1;

        var totalTransferPrice = (highestCost?.Price ?? 0) +
            (((entity.TransportationContractor!.PercentageValue ?? 0m) * highestCost?.Price ?? 0m) / 100) *
            (dests > 0 ? dests : 1);

        var taxPrice = ((entity.TransportationContractor.PercentageValue ?? 0m) * (highestCost?.Price ?? 0)) / 100;

        return (highestCost, totalTransferPrice);
    }

    public static string TransportationRequestMessageModel(TransportationRequest request,
        string? creator,
        string? confirmer,
        string? srcCityName,
        string? srcAddress,
        string? desCityName,
        string? desAddress, CT ct)
    {
        string message = string.Empty;

        var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");

        var priceWithComma = request.Price is not null
            ? request.Price.Value.ToString("N0") + " ریال"
            : null;

        message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
        message += $"{TelegramValues.TruckIcon}<b>تایید درخواست حمل و نقل باربری :</b>{Environment.NewLine}{Environment.NewLine}" +
                   $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                   $"<b>تاریخ و ساعت :</b> {createTime} {createDateShamsi} {Environment.NewLine}" +
                   $"<b>شماره بارنامه :</b> {request.FreightNumber} {Environment.NewLine}" +
                   $"<b>شماره ترابری :</b> {request.RequestNumber} {Environment.NewLine}" +
                   $"<b>تایید کننده درخواست :</b> {confirmer} {Environment.NewLine}" +
                   $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                   $"<b>مبداء :</b> {srcCityName}: {srcAddress} {Environment.NewLine}" +
                   $"<b>مقصد :</b> {desCityName}: {desAddress} {Environment.NewLine}" +
                   $"<b>مشخصات راننده:</b> {request.CarSpecifications} - {request.NumberPlates} - {request.DriverName} - {request.PhoneNumber} {Environment.NewLine}" +
                   $"<b>توضیحات :</b> {request.Description} {Environment.NewLine}";

        return message;
    }

    public static string PaidTransportationRequestMessageModel(TransportationRequest request,
        string? creator,
        string? confirmer,
        string? srcCityName,
        string? srcAddress,
        string? desCityName,
        string? desAddress, CT ct)
    {
        string message = string.Empty;

        var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");

        var priceWithComma = request.Price is not null
                ? request.Price.Value.ToString("N0") + " ریال"
                : null;

        message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
        message += $"{TelegramValues.TruckIcon}<b>درخواست پرداخت حمل و نقل باربری :</b>{Environment.NewLine}{Environment.NewLine}" +
                   $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                   $"<b>تاریخ و ساعت :</b> {createTime} {createDateShamsi} {Environment.NewLine}" +
                   $"<b>شماره بارنامه :</b> {request.FreightNumber} {Environment.NewLine}" +
                   $"<b>شماره ترابری :</b> {request.RequestNumber} {Environment.NewLine}" +
                   $"<b>پرداخت تایید:</b> {confirmer} {Environment.NewLine}" +
                   $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                   $"<b>مبداء :</b> {srcCityName}: {srcAddress} {Environment.NewLine}" +
                   $"<b>مقصد :</b> {desCityName}: {desAddress} {Environment.NewLine}" +
                   $"<b>مشخصات راننده:</b> {request.CarSpecifications} - {request.NumberPlates} - {request.DriverName} - {request.PhoneNumber} {Environment.NewLine}" +
                   $"<b>تاریخ دستور پرداخت :</b> {TimeCalculator.ConvertToShamsi(request.PaymentDate)} {Environment.NewLine}" +
                   $"<b>وضعیت :</b> {request.TransportationRequestStatus.GetEnumDescription()} {Environment.NewLine}" +
                   $"<b>توضیحات :</b> {request.Description} {Environment.NewLine}";

        return message;
    }

    public static string PaidSnapRequestMessageModel(List<string>? snapRequestNumber,
        string? confirmer,
        string? pettyCash,
        decimal? price,
        DateTime? paymentDate, CT ct)
    {
        string message = string.Empty;

        var priceWithComma = price is not null ? price.Value.ToString("N0") + " ریال" : null;

        var paymentDay = TimeCalculator.ConvertToShamsi(paymentDate);
        var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");

        message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
        message += $"{TelegramValues.TruckIcon}<b>درخواست پرداخت حمل و نقل اسنپ :</b>  {Environment.NewLine}{Environment.NewLine}" +
                   $"<b>تنخواه گردان :</b> {pettyCash} {Environment.NewLine}" +
                   $"<b>تاریخ و ساعت :</b> {createTime} {createDateShamsi} {Environment.NewLine}" +
                   $"<b> شماره درخواست های ترابری :</b> {string.Join("-", snapRequestNumber ?? [])} {Environment.NewLine}" +
                   $"<b>صادر کننده دستور پرداخت:</b> {confirmer} {Environment.NewLine}" +
                   $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                   $"<b>تاریخ دستور پرداخت :</b> {paymentDay} {Environment.NewLine}";

        return message;
    }


    private async Task<(bool flowControl, (List<ViewPacking>? packings, List<ViewPackingShippingDetail>? shippingDetails) value, Error? error)>
    ValidatePackings(CreateWarehouseTransportationRequest request, CT ct)
    {
        var packings = await _packingRepository.GetByIdsWithInclude(request.PackingIds, ct);

        if (packings is null || packings.Distinct().Count() != request.PackingIds.Distinct().Count())
            return (flowControl: false, value: (null, null), error: TransportationContractorErrors.PackingNotFound);

        if (packings.Any(x => x.Type != PackingType.Exit))
            return (flowControl: false, value: (null, null), error: TransportationContractorErrors.PackingTypeNotValid);

        if (packings.Any(x => x.Status != PackingStatus.NotSent))
            return (flowControl: false, value: (null, null), error: TransportationContractorErrors.PackingStatusNotValid);

        var shippingDetails = packings.SelectMany(x => x.PackingShippingDetails).ToList();
        if (shippingDetails.Any())
        {
            var checkShippings = ValidateShippings(shippingDetails);
            if (checkShippings.IsFailure)
                return (flowControl: false, value: (null, null), error: checkShippings.Error);
        }

        return (flowControl: true, value: (packings, shippingDetails), error: null);
    }

    private async Task<Result<ValidateRequestOfAggragateRequestModel?>> ValidateAggregateWarehouseTransportation(
        AggregateWarehouseTransportationRequest request, CT ct)
    {
        var cargoPalletIds = request.Cargos.ListedMany(x => x.PalletIds);
        var cargosQuery = await _transportationCargoRepository.GetByPalletIds(cargoPalletIds, ct);
        if (cargosQuery == null)
            return Result.Failure<ValidateRequestOfAggragateRequestModel>
                (TransportationRequestErrors.CargosNotfound);
        var cargos = cargosQuery.Distinct().ToList();
        var cargoIds = cargos.Listed(x => x.Id);

        TransportationContractor? contractor = null;
        List<MachineType>? machineTypes = [];
        List<ViewThirdParty>? drivers = [];
        var driverIds = request.Cargos.NullListed(x => x.DriverId);
        var machineTypeIds = request.Cargos.NullListed(x => x.MachineTypeId);
        if (request.Cargos.All(x => x.PackingShippingType != PackingShippingType.InPersonDelivery))
        {
            if (request.Cargos.FirstOrDefault()!.TransportationContractorId != null)
            {
                contractor = await _transportationContractorRepository.GetTransportationContractor(
                    request.Cargos.FirstOrDefault()!.TransportationContractorId!.Value, ct);
                if (contractor is null)
                    return Result.Failure<ValidateRequestOfAggragateRequestModel>
                        (TransportationContractorErrors.TransportationContractorNotFound);
            }
            else
            {
                return Result.Failure<ValidateRequestOfAggragateRequestModel>
                    (TransportationContractorErrors.ContractorIdIsNull);
            }

            if (machineTypeIds is not null && machineTypeIds.Count > 0)
            {
                machineTypes = await _machineTypeRepository.GetsMachineTypeByIds(machineTypeIds!, ct);
                if (machineTypes is null || machineTypes.Count != machineTypeIds.Count)
                    return Result.Failure<ValidateRequestOfAggragateRequestModel>
                        (MachineErrors.MachineWithIdNotFound);
            }
            else
            {
                return Result.Failure<ValidateRequestOfAggragateRequestModel>
                    (MachineErrors.MachineWithIdNotFound);
            }

            if (driverIds is not null && driverIds.Count > 0)
            {
                drivers = await _thirdPartyRepository.GetByIds(driverIds, ct);
                if (drivers is null || drivers.Count != driverIds.Count)
                    return Result.Failure<ValidateRequestOfAggragateRequestModel>
                        (TransportationRequestErrors.UnValidDriver);
            }
            else if ((request.Cargos.All(x => x.PackingShippingType == PackingShippingType.Contracting)) &&
                (driverIds is null || driverIds.Count <= 0))
            {
                return Result.Failure<ValidateRequestOfAggragateRequestModel>
                    (TransportationRequestErrors.UnValidDriver);
            }
        }

        var palletIds = request.Cargos.SelectMany(z => z.PalletIds).Distinct().ToList();
        var palletsData = await _transportationCargoPalletRepository.GetByIds(palletIds, ct);
        if (palletsData is null || palletsData.Count != palletIds.Count)
            return Result.Failure<ValidateRequestOfAggragateRequestModel>
                (TransportationRequestErrors.CargosPalletNotfound);

        if (palletsData.Any(x => x.TransportationRequestId is not null &&
                x.TransportationRequest is not null &&
                x.TransportationRequest.IsDeleted == false))
            return Result.Failure<ValidateRequestOfAggragateRequestModel>
                (TransportationRequestErrors.CargosPalletHaveTransport);

        var packingIds = cargos.Listed(x => x.PackingId);
        var packings = await _packingRepository.GetByIdsWithInclude(packingIds, ct);
        if (packings is null || packings.Count != packingIds.Count)
            return Result.Failure<ValidateRequestOfAggragateRequestModel>
                (TransportationRequestErrors.CargosNotfound);

        return new ValidateRequestOfAggragateRequestModel(
            cargos,
            contractor,
            machineTypes,
            palletsData,
            packings,
            drivers,
            cargoIds,
            machineTypeIds,
            palletIds,
            contractor?.Id);
    }

    private async Task<(bool flowControl, Result<AggregateWarehouseTransportationResponse?> value)> AddAgancyTransportRequests(
        AggregateWarehouseTransportationRequest request,
        ValidateRequestOfAggragateRequestModel validateData,
        List<TransportationRequest> transportationRequests,
        long companyId, CT ct)
    {
        if (validateData.Cargos!.All(z => z.TransportationCargoPallets.All(x => x.PackingShippingType == PackingShippingType.Agency)) ||
            request.Cargos!.All(z => z.PackingShippingType == PackingShippingType.Agency))
        {
            var groups = request.Cargos
                .GroupBy(x => new { x.MachineTypeId, x.NumberPlate })
                .ToList();

            var requestNumber = await _repository.RequestNumberCreator(false, validateData.Cargos.FirstOrDefault()!.Packing!.CompanyId, ct);
            var contractorFreight = await _repository.GetLastFreightNumber(validateData.Contractor.Id, ct);
            var number = requestNumber;

            var index = 0;
            foreach (var group in groups)
            {
                var machineTypeId = group.Key.MachineTypeId;
                var numberPlate = group.Key.NumberPlate;

                var allPalletIds = group.SelectMany(x => x.PalletIds).Distinct().ToList();
                var allDocumentUrls = group.SelectMany(x => x.DocumentUrls ?? []).Distinct().ToList();

                var cargosQuery = await _transportationCargoRepository.GetByPalletIds(allPalletIds, ct);
                if (cargosQuery == null)
                    return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                        (TransportationRequestErrors.CargosNotfound));
                var cargos = cargosQuery.Distinct().ToList();
                var cargoIdsInGroup = cargos.Listed(x => x.Id);

                var palletData = validateData.PalletsData!.Where(x => allPalletIds.Contains(x.Id)).Distinct().ToList();

                var cargosData = validateData.Cargos.Where(x => cargoIdsInGroup.Contains(x.Id)).Distinct().ToList();
                if (!group.All(x => x.DeliveryMethod == DeliveryMethod.GroundTransport))
                    return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                        (TransportationRequestErrors.DeliveryTypeIsUnvalid));

                var cargoPackings = cargosData.Listed(x => x.PackingId);
                var packings = validateData.PackingsData!.Where(x => cargoPackings.Contains(x.Id)).Distinct().ToList();
                var source = packings.SelectMany(x => x.PackingAddress).Where(x => x.Type == AddressType.Source).Distinct().FirstOrDefault();
                var destination = packings.SelectMany(x => x.PackingAddress).Distinct().OrderByDescending(x => x.Id)
                    .Where(x => x.Type == AddressType.Destination).FirstOrDefault();

                var machineType = validateData.MachineTypes!.FirstOrDefault(x => x.Id == machineTypeId);

                var freightNumber = GetFreightNumber(validateData.Contractor, contractorFreight, index);
                index++;
                var transportationRequest = new TransportationRequest(DeliveryMethod.GroundTransport,
                    cargosData.Any(x => x.TransportationCargoPallets.Any(z => z.DeliveryType == DeliveryType.Quick)) ?
                    DeliveryType.Quick : DeliveryType.Normal,
                    source!.CityId, destination!.CityId, source.Address, destination!.Address,
                    cargosData.FirstOrDefault()!.TransportationCargoPallets.MinBy(x => x.PostageDate)?.PostageDate ?? DateTime.Now,
                    group.First().Price, group.First().Description,
                    companyId, validateData.Contractor, number, freightNumber, false, numberPlate,
                    null, group.First().Driver, machineType, group.First().CarSpecifications, group.First().PhoneNumber,
                    palletData.Sum(x => x.Weight) ?? 0);
                var result = await _repository.Create(transportationRequest, ct);

                result.AddWarehouseTransportDetail(new TransportationRequestDetail(group.First().GlobalFreightNumber, null, 0, group.First().Price,
                    0, null, 0, group.First().Price, palletData.Sum(x => x.Price), 0, null, result));

                transportationRequests.Add(result);

                foreach (var item in palletData)
                {
                    item.SetTransportRequest(result);
                    if (item.PackingShippingType == null)
                    {
                        item.SetPackingShippingType(PackingShippingType.Agency);
                        item.SetDeliveryMethod(result.DeliveryMethod);
                        item.SetDeliveryType(result.DeliveryType);
                        item.SetTransportationContractorId(validateData.Contractor);
                    }
                    await _transportationCargoPalletRepository.Update(item);
                }

                number++;
            }
        }

        return (flowControl: true, value: null);
    }

    private async Task<(bool flowControl, Result<AggregateWarehouseTransportationResponse?> value)> AddInPersonDeliveryTransportRequests(
        AggregateWarehouseTransportationRequest request,
        ValidateRequestOfAggragateRequestModel validateData,
        List<TransportationRequest> transportationRequests,
        long companyId, CT ct)
    {
        if (validateData.Cargos!.All(z => z.TransportationCargoPallets.All(x => x.PackingShippingType == PackingShippingType.InPersonDelivery)) ||
            request.Cargos!.All(z => z.PackingShippingType == PackingShippingType.InPersonDelivery))
        {
            var groups = request.Cargos
                .GroupBy(x => new { x.Driver, x.VehicleName, x.NumberPlate })
                .ToList();

            foreach (var group in groups)
            {
                var palletIds = group.SelectMany(x => x.PalletIds).ToList();
                var pallets = validateData.PalletsData!.Where(x => palletIds.Contains(x.Id)).ToList();
                foreach (var item in pallets)
                {
                    item.SetDeliveryMethod(group.First().DeliveryMethod);
                    item.SetDeliveryType(group.First().DeliveryType);
                    item.SetTransportationContractorId(null);
                    item.SetPostageDate(group.First().PostageDate);
                    item.SetVehicleName(group.Key.VehicleName);
                    item.SetNumberPlate(group.Key.NumberPlate);
                    item.SetDriver(group.Key.Driver);
                    item.SetDriverPhoneNumber(group.First().PhoneNumber);
                    item.SetPackingShippingType(PackingShippingType.InPersonDelivery);
                    item.SetTransferPrice(0);
                    await _transportationCargoPalletRepository.Update(item);
                }
            }
        }

        return (flowControl: true, value: null);
    }

    private async Task<(bool flowControl, Result<AggregateWarehouseTransportationResponse?> value)> AddContractorTransportRequests(
        AggregateWarehouseTransportationRequest request,
        ValidateRequestOfAggragateRequestModel validateData,
        List<TransportationRequest> transportationRequests,
        long companyId,
        CT ct)
    {
        if (validateData.Cargos!.All(z => z.TransportationCargoPallets.All(x => x!.PackingShippingType == PackingShippingType.Contracting)) ||
            request.Cargos!.All(z => z.PackingShippingType == PackingShippingType.Contracting))
        {
            var groups = request.Cargos
                .GroupBy(x => new { x.MachineTypeId, x.NumberPlate })
                .ToList();

            var requestNumber = await _repository.RequestNumberCreator(false, validateData.Cargos!.FirstOrDefault()!.Packing!.CompanyId, ct);
            var contractorFreight = await _repository.GetLastFreightNumber(validateData.Contractor!.Id, ct);
            var number = requestNumber;

            List<ShippingCost>? shippingCosts = [];
            List<TransportationContractorPriceWeight>? priceWeights = [];
            var (flowControl, pricesData) = await GetTransportContractorPrices(validateData.Contractor, ct);
            if (!flowControl)
                return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                            (ShippingCostErrors.ShippingCostNotFound));

            var index = 0;
            foreach (var group in groups)
            {
                var machineTypeId = group.Key.MachineTypeId;
                var machineType = validateData.MachineTypes!.FirstOrDefault(x => x.Id == machineTypeId);
                var numberPlate = group.Key.NumberPlate;

                var allPalletIds = group.SelectMany(x => x.PalletIds).Distinct().ToList();
                var allDocumentUrls = group.SelectMany(x => x.DocumentUrls ?? []).Distinct().ToList();

                var cargosQuery = await _transportationCargoRepository.GetByPalletIds(allPalletIds, ct);
                if (cargosQuery == null)
                    return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                        (TransportationRequestErrors.CargosNotfound));
                var cargos = cargosQuery.Distinct().ToList();
                var cargoIdsInGroup = cargos.Listed(x => x.Id);

                var palletData = validateData.PalletsData!.Where(x => allPalletIds.Contains(x.Id)).Distinct().ToList();
                var cargosData = validateData.Cargos!.Where(x => cargoIdsInGroup.Contains(x.Id)).Distinct().ToList();

                var cargoPackings = cargosData.Listed(x => x.PackingId);
                var packings = validateData.PackingsData!.Where(x => cargoPackings.Contains(x.Id)).Distinct().ToList();
                var source = packings.SelectMany(x => x.PackingAddress).Where(x => x.Type == AddressType.Source).Distinct().FirstOrDefault();
                var destinations = packings.SelectMany(x => x.PackingAddress).Where(x => x.Type == AddressType.Destination).Distinct().ToList();

                var freightNumber = GetFreightNumber(validateData.Contractor, contractorFreight, index);
                index++;

                var totalTransferPrice = 0m;
                var taxPrice = 0m;
                decimal? priceWeight = 0m;
                var insurancePrice = 0m;
                var servicePrice = 0m;
                var productPrice = 0m;
                ViewPackingAddress finalAddress = null;

                if (validateData.Contractor.Type == TransportationContractorCalculateType.Distance)
                {
                    var postDate = group.MinBy(x => x.PostageDate)?.PostageDate;
                    shippingCosts = pricesData!.ShippingCosts!.Where(x =>
                        x.FromDate != null &&
                        x.ToDate != null &&
                        (x.FromDate.Value.Date <= (postDate ?? DateTime.Now.Date) &&
                        x.ToDate!.Value.Date >= (postDate ?? DateTime.Now.Date)))
                        .ToList();

                    if (shippingCosts is null || shippingCosts.Count <= 0)
                        return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                                    (ShippingCostErrors.ShippingCostNotFound));

                    var packingNumbers = packings.NullListed(x => x.RequestNumber);
                    var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers(packingNumbers!, ct);
                    var invoiceOwners = invoices!.Listed(x => x.OwnerId);
                    var invoiceCompanies = invoices!.Listed(x => x.OrdererCompanyId);

                    ShippingCost? highestCost = null;
                    ViewPackingAddress? selectedAddress = null;
                    var machineShippingCosts = shippingCosts?
                   .Where(x => x.MachineTypeId == machineTypeId && invoiceOwners.Contains(x.ThirdPartyId))
                   .ToList();

                    if (machineShippingCosts != null && machineShippingCosts.Any())
                    {
                        bool allHaveCompany = machineShippingCosts.All(x => x.ThirdPartyCompanyId != null);
                        if (allHaveCompany)
                        {
                            var companySpecificCosts = machineShippingCosts
                                .Where(x => invoiceCompanies.Contains(x.ThirdPartyCompanyId))
                                .ToList();

                            var highestPriceDestination = companySpecificCosts.MaxBy(x => x.Price);

                            if (highestPriceDestination is null)
                            {
                                return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                                    (ShippingCostErrors.ShippingCostNotFound));
                            }

                            var address = destinations.FirstOrDefault(x => x!.CityId == highestPriceDestination!.DestinationCityId);
                            if (address is null)
                            {
                                return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                                    (ShippingCostErrors.ShippingCostNotFound));
                            }

                            selectedAddress = address;
                            highestCost = highestPriceDestination;
                            finalAddress = selectedAddress!;
                        }
                        else
                        {
                            var highestPriceDestination = destinations
                                .Join(machineShippingCosts.Where(z => z.SourceCityId == source!.CityId),
                                    dest => dest!.CityId,
                                    cost => cost.DestinationCityId,
                                    (dest, cost) => new { DestAddress = dest, ShippingCost = cost })
                                .MaxBy(x => x.ShippingCost.Price);

                            if (highestPriceDestination is null)
                            {
                                return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                                    (ShippingCostErrors.ShippingCostNotFound));
                            }

                            selectedAddress = highestPriceDestination.DestAddress;
                            highestCost = highestPriceDestination.ShippingCost;
                            finalAddress = selectedAddress!;
                        }
                    }
                    else
                    {
                        return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>
                                    (ShippingCostErrors.ShippingCostNotFound));
                    }

                    var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
                    var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
                    var addresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
                    var dests = invoiceOwners.Count - 1;

                    totalTransferPrice = (highestCost?.Price ?? 0) +
                        (((validateData.Contractor.PercentageValue ?? 0m) * highestCost?.Price ?? 0m) / 100) *
                        (dests > 0 ? dests : 1);

                    taxPrice = ((validateData.Contractor.PercentageValue ?? 0m) * (highestCost?.Price ?? 0)) / 100;
                }
                else if (validateData.Contractor.Type == TransportationContractorCalculateType.Weight)
                {
                    priceWeights = pricesData!.PriceWeights;
                    var insuranceData = validateData.Contractor.TransportationContractorInsurances.LastOrDefault();
                    productPrice = palletData!.Sum(x => x.Price ?? 0);
                    if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
                    {
                        insurancePrice = insuranceData.FixedPrice;
                    }
                    else if (productPrice > insuranceData?.MaxProductPrice)
                    {
                        insurancePrice = (productPrice / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
                    }

                    servicePrice = validateData.Contractor.ServicePrice ?? 0;
                    priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(), palletData!.Sum(x => x.Weight) ?? 0);
                    totalTransferPrice = insurancePrice + (priceWeight ?? 0m) + ((priceWeight ?? 0m) *
                        (validateData.Contractor.PercentageValue ?? 0) / 100) + servicePrice;
                    taxPrice = ((priceWeight ?? 0m) * (validateData.Contractor.TaxPercent ?? 1)) / 100;

                    finalAddress = destinations.OrderByDescending(x => x.Id).FirstOrDefault()!;
                }
                else if (validateData.Contractor.Type == TransportationContractorCalculateType.Price)
                {
                    totalTransferPrice = group.First().Price ?? 0m;
                    finalAddress = destinations.OrderByDescending(x => x.Id).FirstOrDefault()!;
                }

                var driver = validateData.Drivers!.FirstOrDefault(x => x.Id == group.First().DriverId);
                var transportationRequest = new TransportationRequest(
                    group.First().DeliveryMethod, group.Any(x => x.DeliveryType == DeliveryType.Quick) ? DeliveryType.Quick : DeliveryType.Normal,
                    source!.CityId, finalAddress!.CityId, source.Address, finalAddress!.Address,
                    group.MinBy(x => x.PostageDate)!.PostageDate ?? DateTime.Now, totalTransferPrice, group.First().Description,
                    companyId, validateData.Contractor, number, freightNumber, false, numberPlate,
                    driver?.Id, group.First().Driver, machineType, group.First().CarSpecifications,
                    driver is not null ? driver.DefaultPhoneNo : group.First().PhoneNumber, palletData.Sum(x => x.Weight) ?? 0);
                var result = await _repository.Create(transportationRequest, ct);

                result.AddWarehouseTransportDetail(new TransportationRequestDetail(group.First().GlobalFreightNumber, null, taxPrice, totalTransferPrice,
                    servicePrice, null, insurancePrice, totalTransferPrice, palletData.Sum(x => x.Price), 0, null, result));

                transportationRequests.Add(result);
                var updatePallet = await UpdatePallet(validateData, shippingCosts, priceWeights, pricesData!,
                    group.Key.MachineTypeId!.Value, group.First().Price, palletData, totalTransferPrice,
                    priceWeight, insurancePrice, servicePrice, productPrice, finalAddress, result, ct);
                if (updatePallet.IsFailure)
                    return (flowControl: false, value: Result.Failure<AggregateWarehouseTransportationResponse>(updatePallet.Error!));

                number++;
            }
        }

        return (flowControl: true, value: null);
    }

    private async Task<Result<(List<ShippingCost> shippingCosts,
        List<TransportationContractorPriceWeight>? priceWeights,
        decimal totalTransferPrice, decimal? priceWeight,
        decimal insurancePrice, decimal servicePrice,
        decimal productPrice,
        ViewPackingAddress finalAddress)>>
        UpdatePallet(
        ValidateRequestOfAggragateRequestModel? validateData,
        List<ShippingCost>? shippingCosts,
        List<TransportationContractorPriceWeight>? priceWeights,
        ShippingCostsAndPriceWeightsDto pricesData,
        long machineTypeId,
        decimal? price,
        List<TransportationCargoPallet> palletData,
        decimal totalTransferPrice,
        decimal? priceWeight,
        decimal insurancePrice,
        decimal servicePrice,
        decimal productPrice,
        ViewPackingAddress finalAddress,
        TransportationRequest result,
        CT ct)
    {
        foreach (var item in palletData)
        {
            var addresses = item!.TransportationRequestWarehouses.ToList();
            var source = addresses.Where(x => x.PackingSourceAddress is not null).Listed(x => x.PackingSourceAddress).Distinct().FirstOrDefault();
            var destinations = addresses.Where(x => x.PackingDestinationAddress is not null).Listed(x => x.PackingDestinationAddress).Distinct().ToList();

            ShippingCost? highestCost = null;
            ViewPackingAddress? selectedAddress = null;
            if (validateData!.Contractor!.Type == TransportationContractorCalculateType.Distance)
            {
                var postDate = item.PostageDate;
                shippingCosts = pricesData.ShippingCosts!.Where(x =>
                    x.FromDate != null &&
                    x.ToDate != null &&
                    (x.FromDate.Value.Date <= (postDate ?? DateTime.Now.Date) &&
                    x.ToDate!.Value.Date >= (postDate ?? DateTime.Now.Date)))
                    .ToList();

                if (shippingCosts is null || shippingCosts.Count <= 0)
                    return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?,
                        decimal, decimal?, decimal, decimal, decimal, ViewPackingAddress)>
                                (ShippingCostErrors.ShippingCostNotFound);

                var packingNumber = item.TransportationCargo.Packing!.RequestNumber;
                var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers([packingNumber!.Value]!, ct);
                var invoiceOwners = invoices!.Listed(x => x.OwnerId);
                var invoiceCompanies = invoices!.Listed(x => x.OrdererCompanyId);

                var machineShippingCosts = shippingCosts?
                    .Where(x => x.MachineTypeId == machineTypeId && invoiceOwners.Contains(x.ThirdPartyId))
                    .ToList();

                if (machineShippingCosts != null && machineShippingCosts.Any())
                {
                    bool allHaveCompany = machineShippingCosts.All(x => x.ThirdPartyCompanyId != null);
                    if (allHaveCompany)
                    {
                        var companySpecificCosts = machineShippingCosts
                            .Where(x => invoiceCompanies.Contains(x.ThirdPartyCompanyId))
                            .ToList();

                        var highestPriceDestination = companySpecificCosts.MaxBy(x => x.Price);

                        if (highestPriceDestination is null)
                        {
                            return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?,
                            decimal, decimal?, decimal, decimal, decimal, ViewPackingAddress)>
                                    (ShippingCostErrors.ShippingCostNotFound);
                        }

                        var address = destinations.FirstOrDefault(x => x!.CityId == highestPriceDestination!.DestinationCityId);
                        if (address is null)
                        {
                            return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?,
                            decimal, decimal?, decimal, decimal, decimal, ViewPackingAddress)>
                                    (ShippingCostErrors.DestinationNotFound);
                        }

                        selectedAddress = address;
                        highestCost = highestPriceDestination;
                        finalAddress = selectedAddress!;
                    }
                    else
                    {
                        var highestPriceDestination = destinations
                            .Join(machineShippingCosts.Where(z => z.SourceCityId == source!.CityId),
                                dest => dest!.CityId,
                                cost => cost.DestinationCityId,
                                (dest, cost) => new { DestAddress = dest, ShippingCost = cost })
                            .MaxBy(x => x.ShippingCost.Price);

                        if (highestPriceDestination is null)
                        {
                            return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?,
                            decimal, decimal?, decimal, decimal, decimal, ViewPackingAddress)>
                                    (ShippingCostErrors.ShippingCostNotFound);
                        }

                        selectedAddress = highestPriceDestination.DestAddress;
                        highestCost = highestPriceDestination.ShippingCost;
                        finalAddress = selectedAddress!;
                    }
                }
                else
                {
                    return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?,
                    decimal, decimal?, decimal, decimal, decimal, ViewPackingAddress)>
                            (ShippingCostErrors.ShippingCostNotFound);
                }

                var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
                var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
                var shippingAddresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
                var dests = invoiceOwners.Count - 1;

                totalTransferPrice = (highestCost?.Price ?? 0) +
                    (((validateData.Contractor.PercentageValue ?? 1m) * highestCost?.Price ?? 0m) / 100) *
                    (dests > 0 ? dests : 1);
            }
            else if (validateData.Contractor.Type == TransportationContractorCalculateType.Weight)
            {
                var insuranceData = validateData.Contractor.TransportationContractorInsurances.LastOrDefault();
                productPrice = item.Price ?? 0;
                if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
                {
                    insurancePrice = insuranceData.FixedPrice;
                }
                else if (productPrice > insuranceData?.MaxProductPrice)
                {
                    insurancePrice = (productPrice / insuranceData.Division ?? 1) + insuranceData.Addition ?? 0;
                }

                servicePrice = validateData.Contractor.ServicePrice ?? 0;
                priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(), item.Weight ?? 0);
                totalTransferPrice = insurancePrice + (priceWeight ?? 0m) + ((priceWeight ?? 0m) *
                    (validateData.Contractor.PercentageValue ?? 0) / 100) + servicePrice;
            }
            else if (validateData.Contractor.Type == TransportationContractorCalculateType.Price)
            {
                totalTransferPrice = price ?? 0m;
                finalAddress = destinations.OrderByDescending(x => x.Id).FirstOrDefault()!;
            }

            item.SetTransportRequest(result);
            item.SetShippingCost(highestCost ?? null);
            item.SetTransferPrice(totalTransferPrice);
            item.SetTransportRequest(result);
            if (item.PackingShippingType == null)
            {
                item.SetPackingShippingType(PackingShippingType.Contracting);
                item.SetDeliveryMethod(result.DeliveryMethod);
                item.SetDeliveryType(result.DeliveryType);
                item.SetTransportationContractorId(validateData.Contractor);
            }
            await _transportationCargoPalletRepository.Update(item);
        }

        return (shippingCosts, priceWeights, totalTransferPrice, priceWeight, insurancePrice, servicePrice, productPrice, finalAddress)!;
    }

    private async Task<Result<(List<ShippingCost> shippingCosts,
        List<TransportationContractorPriceWeight>? priceWeights,
        decimal totalTransferPrice,
        TransportationRequestDetail? extraInfo)>>
        CalcTransportAfterReforms(
        TransportationRequest transportationRequestData,
        List<ShippingCost>? shippingCosts,
        List<TransportationContractorPriceWeight>? priceWeights,
        ShippingCostsAndPriceWeightsDto pricesData,
        List<TransportationCargoPallet> palletsData,
        CT ct)
    {
        var totalTransferPrice = 0m;
        var taxPrice = 0m;
        decimal? priceWeight = 0m;
        var insurancePrice = 0m;
        var servicePrice = 0m;
        var productPrice = 0m;
        ViewPackingAddress finalAddress = null;
        ShippingCost? highestCost = null;

        var packings = transportationRequestData.TransportationCargoPallets
            .SelectMany(x => x.TransportationCargo!.Packing!.PackingAddress).Distinct().ToList();
        var source = packings.Where(x => x.Type == AddressType.Source).FirstOrDefault();
        var destinations = packings.Where(x => x.Type == AddressType.Destination).ToList();

        if (transportationRequestData.TransportationContractor!.Type == TransportationContractorCalculateType.Distance)
        {
            var postDate = palletsData.Select(x => x.PostageDate).FirstOrDefault();
            shippingCosts = pricesData.ShippingCosts!.Where(x =>
                x.FromDate != null &&
                x.ToDate != null &&
                (x.FromDate.Value.Date <= (postDate ?? DateTime.Now.Date) &&
                x.ToDate!.Value.Date >= (postDate ?? DateTime.Now.Date)))
                .ToList();

            if (shippingCosts is null || shippingCosts.Count <= 0)
                return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?, decimal, TransportationRequestDetail?)>
                            (ShippingCostErrors.ShippingCostNotFound);

            var packingNumbers = palletsData.Listed(x => x.TransportationCargo.PackingNumber);
            var invoices = await _invoiceRepository.GetInvoiceByPackingRequestNumbers(packingNumbers!, ct);
            var invoiceOwners = invoices!.Listed(x => x.OwnerId);
            var invoiceCompanies = invoices!.Listed(x => x.OrdererCompanyId);

            ViewPackingAddress? selectedAddress = null;
            var machineShippingCosts = shippingCosts?
               .Where(x => x.MachineTypeId == transportationRequestData.MachineTypeId && invoiceOwners.Contains(x.ThirdPartyId))
               .ToList();

            if (machineShippingCosts != null && machineShippingCosts.Any())
            {
                bool allHaveCompany = machineShippingCosts.All(x => x.ThirdPartyCompanyId != null);
                if (allHaveCompany)
                {
                    var companySpecificCosts = machineShippingCosts
                        .Where(x => invoiceCompanies.Contains(x.ThirdPartyCompanyId))
                        .ToList();

                    var highestPriceDestination = companySpecificCosts.MaxBy(x => x.Price);

                    if (highestPriceDestination is null)
                    {
                        return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?, decimal, TransportationRequestDetail?)>
                            (ShippingCostErrors.ShippingCostNotFound);
                    }

                    var address = destinations.FirstOrDefault(x => x!.CityId == highestPriceDestination!.DestinationCityId);
                    if (address is null)
                    {
                        return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?, decimal, TransportationRequestDetail?)>
                            (ShippingCostErrors.ShippingCostNotFound);
                    }

                    selectedAddress = address;
                    highestCost = highestPriceDestination;
                    finalAddress = selectedAddress!;
                }
                else
                {
                    var highestPriceDestination = destinations
                        .Join(machineShippingCosts.Where(z => z.SourceCityId == source!.CityId),
                            dest => dest!.CityId,
                            cost => cost.DestinationCityId,
                            (dest, cost) => new { DestAddress = dest, ShippingCost = cost })
                        .MaxBy(x => x.ShippingCost.Price);

                    if (highestPriceDestination is null)
                    {
                        return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?, decimal, TransportationRequestDetail?)>
                            (ShippingCostErrors.ShippingCostNotFound);
                    }

                    selectedAddress = highestPriceDestination.DestAddress;
                    highestCost = highestPriceDestination.ShippingCost;
                    finalAddress = selectedAddress!;
                }
            }
            else
            {
                return Result.Failure<(List<ShippingCost>, List<TransportationContractorPriceWeight>?, decimal, TransportationRequestDetail?)>
                            (ShippingCostErrors.ShippingCostNotFound);
            }

            var withWarehouse = destinations.Where(x => x.WarehouseId != null).ToList();
            var withoutWarehouse = destinations.Where(x => x.WarehouseId == null).ToList();
            var addresses = withWarehouse.Concat(withoutWarehouse).Distinct().ToList();
            var dests = invoiceOwners.Count - 1;

            totalTransferPrice = (highestCost?.Price ?? 0) +
                (((transportationRequestData.TransportationContractor.PercentageValue ?? 0m) * highestCost?.Price ?? 0m) / 100) *
                (dests > 0 ? dests : 1);

            taxPrice = ((transportationRequestData.TransportationContractor.PercentageValue ?? 0m) * (highestCost?.Price ?? 0)) / 100;
        }
        else if (transportationRequestData.TransportationContractor.Type == TransportationContractorCalculateType.Weight)
        {
            priceWeights = pricesData.PriceWeights;
            var insuranceData = transportationRequestData.TransportationContractor.TransportationContractorInsurances.LastOrDefault();
            productPrice = transportationRequestData.TransportationCargoPallets!.Sum(x => x.Price ?? 0);
            if (productPrice >= insuranceData?.MinProductPrice && productPrice <= insuranceData?.MaxProductPrice)
            {
                insurancePrice = insuranceData.FixedPrice;
            }
            else if (productPrice > insuranceData?.MaxProductPrice)
            {
                insurancePrice = (productPrice / (insuranceData.Division ?? 1)) + (insuranceData.Addition ?? 0);
            }

            servicePrice = transportationRequestData.TransportationContractor.ServicePrice ?? 0;
            priceWeight = Calculator.CalculateTransportPriceWeight(priceWeights!.ToArray(),
                transportationRequestData.TransportationCargoPallets!.Sum(x => x.Weight) ?? 0);

            totalTransferPrice = insurancePrice + (priceWeight ?? 0m) + ((priceWeight ?? 0m) *
                (transportationRequestData.TransportationContractor.PercentageValue ?? 0) / 100) + servicePrice;

            taxPrice = ((priceWeight ?? 0m) * (transportationRequestData.TransportationContractor.TaxPercent ?? 1)) / 100;

            finalAddress = destinations.OrderByDescending(x => x.Id).FirstOrDefault()!;
        }

        var detail = transportationRequestData.TransportationRequestDetails?.FirstOrDefault() ?? null;
        TransportationRequestDetail? extraInfo = null;
        if (detail is null)
        {
            extraInfo = new TransportationRequestDetail(null, null, taxPrice, totalTransferPrice, servicePrice,
                null, insurancePrice, highestCost?.Price ?? totalTransferPrice, productPrice, 0, null, transportationRequestData);
        }
        else
        {
            detail.Update(detail.GlobalFreightNumber, null, taxPrice, totalTransferPrice, servicePrice,
                null, insurancePrice, highestCost?.Price ?? totalTransferPrice, productPrice, detail.OutofRange, null, transportationRequestData);
        }

        return (shippingCosts, priceWeights, totalTransferPrice, extraInfo);
    }

    private async Task<(bool flowControl, Result<UpdateAfterCargoDeclarationResponse?> value)> UpdatePalletsAfterCargoDeclare(
        UpdateAfterCargoDeclarationRequest request, List<TransportationCargoPallet>? cargoPallets, CT ct)
    {
        TransportationContractor? transportationContractor = null;
        if (request.PackingShippingType != PackingShippingType.InPersonDelivery)
        {
            if (request.TransportationContractorId != null && request.TransportationContractorId > 0)
            {
                var tContractor = await _transportationContractorRepository.GetTransportationContractor(
                    request.TransportationContractorId.Value, ct);
                if (tContractor is null)
                    return (flowControl: false, value: Result.Failure<UpdateAfterCargoDeclarationResponse>
                        (TransportationContractorErrors.TransportationContractorNotFound));
                transportationContractor = tContractor;
            }
            else
            {
                return (flowControl: false, value: Result.Failure<UpdateAfterCargoDeclarationResponse>
                    (TransportationContractorErrors.TransportationContractorNotFound));
            }
        }

        if (cargoPallets != null && cargoPallets.Count > 0)
            foreach (var item in cargoPallets)
            {
                item.SetDeliveryMethod(request.DeliveryMethod);
                item.SetDeliveryType(request.DeliveryType);
                item.SetTransportationContractorId(
                    request.PackingShippingType == PackingShippingType.InPersonDelivery ?
                    null : transportationContractor);
                item.SetPackingShippingType(request.PackingShippingType);
                item.SetVehicleName(request.VehicleName);
                item.SetNumberPlate(request.NumberPlate);
                item.SetDriver(request.Driver);
                item.SetDriverPhoneNumber(request.DriverPhoneNumber);
                item.SetPostageDate(request.PostageDate);

                await _transportationCargoPalletRepository.Update(item);
            }

        return (flowControl: true, value: null);
    }

    private async Task<(bool flowControl, Result<UpdateFreeCargosTransportInfoResponse?> value)> UpdatePalletsAfterCargoDeclare(
        UpdateFreeCargosTransportInfoRequest request, List<TransportationCargoPallet>? cargoPallets, CT ct)
    {
        TransportationContractor? transportationContractor = null;
        if (request.PackingShippingType != PackingShippingType.InPersonDelivery)
        {
            if (request.TransportationContractorId != null && request.TransportationContractorId > 0)
            {
                var tContractor = await _transportationContractorRepository.GetTransportationContractor(
                    request.TransportationContractorId.Value, ct);
                if (tContractor is null)
                    return (flowControl: false, value: Result.Failure<UpdateFreeCargosTransportInfoResponse>
                        (TransportationContractorErrors.TransportationContractorNotFound));
                transportationContractor = tContractor;
            }
            else
            {
                return (flowControl: false, value: Result.Failure<UpdateFreeCargosTransportInfoResponse>
                    (TransportationContractorErrors.TransportationContractorNotFound));
            }
        }

        if (cargoPallets != null && cargoPallets.Count > 0)
            foreach (var item in cargoPallets)
            {
                item.SetDeliveryMethod(request.DeliveryMethod);
                item.SetDeliveryType(request.DeliveryType);
                item.SetTransportationContractorId(
                    request.PackingShippingType == PackingShippingType.InPersonDelivery ?
                    null : transportationContractor);
                item.SetPackingShippingType(request.PackingShippingType);
                item.SetVehicleName(request.VehicleName);
                item.SetNumberPlate(request.NumberPlate);
                item.SetDriver(request.Driver);
                item.SetDriverPhoneNumber(request.DriverPhoneNumber);
                item.SetPostageDate(request.PostageDate);

                await _transportationCargoPalletRepository.Update(item);
            }

        return (flowControl: true, value: null);
    }
    private async Task<(bool flowControl, Result<UpdateFreeCargosTransportInfoResponse?> value)> UpdatePackingShippingDetails(
    UpdateFreeCargosTransportInfoRequest request,
    List<TransportationCargoPallet> palletNotHaveTransport,
    List<TransportationCargoPallet> pallets,
    CT ct)
    {
        var requestedPalletIds = request.PalletIds?.Distinct().ToList() ?? pallets.Select(x => x.Id).ToList();

        var requestContainsAllPallets =
            requestedPalletIds.Count == pallets.Count &&
            pallets.All(x => requestedPalletIds.Contains(x.Id));

        var allPalletsAreWithoutTransport =
            palletNotHaveTransport.Count == pallets.Count &&
            pallets.All(x => palletNotHaveTransport.Any(y => y.Id == x.Id));

        if (allPalletsAreWithoutTransport && requestContainsAllPallets)
        {
            var packingIds = pallets.Listed(x => x.TransportationCargo.PackingId);
            var packings = await _packingRepository.GetByIdsWithInclude(packingIds, ct);
            var shippingIds = packings!
                .SelectMany(x => x.PackingShippingDetails)
                .Distinct()
                .Listed(x => x.Id);

            if (shippingIds.Any())
            {
                var updatePac = await _packingService.UpdateDeliveryPacking(
                    new(
                        shippingIds,
                        (Warehouse.ClientSdks.Enums.DeliveryMethod?)(int?)request.DeliveryMethod,
                        (Warehouse.ClientSdks.Enums.DeliveryType?)(int?)request.DeliveryType,
                        request.TransportationContractorId,
                        (Warehouse.ClientSdks.Enums.PackingShippingType?)(int?)request.PackingShippingType,
                        request.VehicleName,
                        request.NumberPlate,
                        request.Driver,
                        request.DriverPhoneNumber,
                        request.PostageDate), ct);

                if (updatePac == null || updatePac.IsDone == false)
                {
                    return (
                        flowControl: false,
                        value: Result.Failure<UpdateFreeCargosTransportInfoResponse>(TransportationContractorErrors.UpdateFeild));
                }
            }
        }

        return (flowControl: true, value: null);
    }

}
