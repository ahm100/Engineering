using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Configs;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.BillOfLadings;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.Services.TransportationRequests.Commands.Create;
using Engineering.Application.Services.TransportationRequests.Commands.DeleteTransportationRequestDocument;
using Engineering.Application.Services.TransportationRequests.Commands.Disable;
using Engineering.Application.Services.TransportationRequests.Commands.Update;
using Engineering.Application.Services.TransportationRequests.Models.Create;
using Engineering.Application.Services.TransportationRequests.Models.Disable;
using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationPaymentType;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestType;
using Engineering.Application.Services.TransportationRequests.Models.Model;
using Engineering.Application.Services.TransportationRequests.Models.TransportationRequestGroupDelete;
using Engineering.Application.Services.TransportationRequests.Models.Update;
using Engineering.Application.Services.TransportationRequests.Queries.GetById;
using Engineering.Application.Services.TransportationRequests.Queries.GetsFilteredRequester;
using Engineering.Application.Services.TransportationRequests.Queries.GetsTotalTransportationRequestPrice;
using Engineering.Application.Services.TransportationRequests.Queries.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Queries.GetTransportationRequestByIdWithoutInclude;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Domain.Entities.Transportations.Enums;
using Gita.Backend.Shared.Domain.Shared.Contracts;
using IdentityServer.ClientSdk.Services;
using IdentityServer.ClientSdk.Services.ServiceClients;
using Microsoft.Extensions.Options;
using Warehouse.ClientSdks.Services;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransportationRequestLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly ITelegramMessageHistoryLogic _telegramMessageHistoryLogic;
    private readonly IMessengerChannelRepository _messengerChannelRepo;
    private readonly IBillOfLadingLogic _billLogic;
    private readonly ITransportationContractorRepository _transportationContractorRepository;
    private readonly ITransportationRequestCostCenterRepository _transportationCostRepo;
    private readonly ITransportationRequestProjectRepository _transportationProjectRepo;
    private readonly ITransportationRequestRepository _repository;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;
    private readonly IViewPackingRepository _packingRepository;
    private readonly IViewThirdPartyRepository _thirdPartyRepository;
    private readonly ITransportationRepository _transportationRepository;
    private readonly ICostCenterRepository _costCenterRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IViewInvoiceRepository _invoiceRepository;
    private readonly IMachineTypeRepository _machineTypeRepository;
    private readonly IPackingService _packingService;
    private readonly ITransportationRequestWarehouseRepository _transportWarehouseRipository;
    private readonly ITransportationRequestDetailRepository _detailRepository;
    private readonly ICompanyClient _companyClient;
    private readonly IShippingCostRepository _shippingCostRepository;
    private readonly ITransportationContractorPriceWeightRepository _priceWeightRepository;
    private readonly ITransportationCargoRepository _transportationCargoRepository;
    private readonly ITransportationCargoPalletRepository _transportationCargoPalletRepository;

    public TransportationRequestLogic(IMediator mediator,
        ILogger<TransportationRequestLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IMessengerChannelRepository messengerChannelRepo,
        IOptionsSnapshot<MessageSenderConfig> options,
        IHttpContextAccessor httpContextAccessor,
        IBillOfLadingLogic billLogic,
        ITransportationContractorRepository transportationContractorRepository,
        ITransportationRequestCostCenterRepository transportationCostRepo,
        ITransportationRequestProjectRepository transportationProjectRepo,
        IViewProductRepository productRepository,
        IViewPackingRepository packingRepository,
        IViewWarehouseRepository warehouseRepository,
        IShippingCostRepository shippingCostRepository,
        IViewThirdPartyRepository thirdPartyRepository,
        ITransportationRepository transportationRepository,
        ICostCenterRepository costCenterRepository,
        IViewInvoiceRepository invoiceRepository,
        IViewInvoiceProductRepository invoiceProductRepository,
        IProjectRepository projectRepository,
        ITransportationRequestRepository repository,
        IMachineTypeRepository machineTypeRepository,
        IPackingService packingService,
        IUserTokenProvider tokenProvider,
        ITransportationRequestWarehouseRepository transportWarehouseRipository,
        ITransportationRequestDetailRepository detailRepository,
        ICompanyClient companyClient,
        ITransportationContractorPriceWeightRepository priceWeightRepository,
        ITransportationCargoRepository transportationCargoRepository,
        ITransportationCargoPalletRepository transportationCargoPalletRepository,
        IUserInfoProvider userInfoProvider)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _messageSenderConfig = options.Value;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
        _billLogic = billLogic;
        _repository = repository;
        _transportationContractorRepository = transportationContractorRepository;
        _messengerChannelRepo = messengerChannelRepo;
        _transportationCostRepo = transportationCostRepo;
        _transportationProjectRepo = transportationProjectRepo;
        _packingRepository = packingRepository;
        _thirdPartyRepository = thirdPartyRepository;
        _transportationRepository = transportationRepository;
        _costCenterRepository = costCenterRepository;
        _invoiceRepository = invoiceRepository;
        _projectRepository = projectRepository;
        _machineTypeRepository = machineTypeRepository;
        _packingService = packingService;
        _transportWarehouseRipository = transportWarehouseRipository;
        _detailRepository = detailRepository;
        _companyClient = companyClient;
        _shippingCostRepository = shippingCostRepository;
        _priceWeightRepository = priceWeightRepository;
        _transportationCargoRepository = transportationCargoRepository;
        _transportationCargoPalletRepository = transportationCargoPalletRepository;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateTransportationRequestResponse?>> CreateTransportationRequest(
        CreateTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for CreateTransportationRequest, TransportationId:{TransportationId}, TripId:{TripId},",
            request.TransportationId, request.TripId);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null
            ? null
            : _userInfoService.UserCompanyId;

        var entitiesValidations = await ValidateTransportationRelatedEntities(
            _mediator,
            _userInfoService,
            new TransportationRequestBaseRequest()
            {
                CompanyId = companyId,
                BankId = request.BankId,
                DriverId = request.DriverId,
                DriverName = request.DriverName,
                BillOfLadingId = request.BillOfLadingId,
                CurrencyUnitId = request.CurrencyUnitId,
                CostCenterIds = request.CostCenterIds,
                DestinationCityId = request.DestinationCityId,
                MachineTypeId = request.MachineTypeId,
                NumberPlates = request.NumberPlates,
                ProjectOperationDetailIds = request.ProjectOperationDetailIds,
                TripId = request.TripId,
                ProjectIds = request.ProjectIds,
                TransportationId = request.TransportationId,
                ProjectOperationIds = request.ProjectOperationIds,
                PostageDate = request.PostageDate,
                ReceivedDate = request.ReceivedDate,
                StartingCityId = request.StartingCityId,
                TransportationCostCategoryId = request.TransportationCostCategoryId,
                TransportationCostGroupId = request.TransportationCostGroupId,
                TransportationContractorId = request.TransportationContractorId,
            },
            false,
            ct);
        if (entitiesValidations.IsFailure)
            return Result.Failure<CreateTransportationRequestResponse>(entitiesValidations.Error!);
        var dataValidate = entitiesValidations.Value;

        var response = await _mediator.Send(new CreateTransportationRequestCommand(
            dataValidate?.Trip!, dataValidate?.Transportation!, dataValidate?.MachineType!,
            request.StartingCityId, request.DestinationCityId, request.StartDate, request.StartDate,
            request.ImageLink, request.Description, dataValidate?.CostCenters!, dataValidate?.Projects,
            dataValidate?.ProjectOperations, dataValidate?.ProjectOperationDetails, request.DriverId,
            request.DriverName, dataValidate?.BillOfLading, request.PostageDate, request.ReceivedDate,
            request.DelivererName, request.RecipientName, request.FreightNumber, request.LoadWeight,
            request.PhoneNumber, request.CarSpecifications, request.NumberPlates, request.BillOfLadingImage,
            request.AccountNumber, request.BankId, request.CardNumber, request.AccountName, request.IBAN,
            request.Price, request.CurrencyUnitId, request.AccountDescription, request.CarID,
            dataValidate!.Transportation!.IsPassenger, request.TransportationCostGroupId,
            request.TransportationCostCategoryId, request.StartingCityAddress,
            request.DestinationAddress, companyId, dataValidate?.TransportationContractor), ct);
        if (response.IsFailure)
            return Result.Failure<CreateTransportationRequestResponse>(response.Error!);

        if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
        {
            var documentResult = await AddDocumentsToRequest(request.DocumentUrls, response.Value!, ct);
            if (documentResult.IsFailure)
                return Result.Failure<CreateTransportationRequestResponse>(documentResult.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateTransportationRequestResponse(response.Value!.Id, true);
    }

    public async Task<Result<UpdateTransportationRequestResponse?>> UpdateTransportationRequest(
        UpdateTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for UpdateTransportationRequest, TransportationId:{TransportationId}, TripId:{TripId},",
            request.TransportationId, request.TripId);

        var transportationRequestData = await _mediator.Send(new GetTransportationRequestByIdQuery(request.Id), ct);
        if (transportationRequestData.IsFailure)
            return Result.Failure<UpdateTransportationRequestResponse>(transportationRequestData.Error!);
        var transporationRequest = transportationRequestData!.Value!;

        var manager = await _userProfileService.HasUserAccessToAction(1096, ct);
        if (!(transporationRequest.TransportationRequestStatus == TransportationRequestStatus.Accepted &&
              manager.Status == ResponseStatusType.Ok))
        {
            var currentUser = _userProfileService.GetProfileInfo();
            if (currentUser is null)
                return Result.Failure<UpdateTransportationRequestResponse>(TransportationRequestErrors
                    .UserInfoNotFound);

            if (currentUser.UserId != transporationRequest.CreatorId)
                return Result.Failure<UpdateTransportationRequestResponse>(TransportationRequestErrors.UserIsUnValid);

            if (ValidateStatusForUpdate(transporationRequest))
                return Result.Failure<UpdateTransportationRequestResponse>(TransportationRequestErrors.UnValidStatus);
        }

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null
            ? null
            : _userInfoService.UserCompanyId;
        var entitiesValidations = await ValidateTransportationRelatedEntities(
            _mediator,
            _userInfoService,
            new TransportationRequestBaseRequest()
            {
                CompanyId = companyId,
                BankId = request.BankId,
                DriverId = request.DriverId,
                DriverName = request.DriverName,
                BillOfLadingId = request.BillOfLadingId,
                CurrencyUnitId = request.CurrencyUnitId,
                CostCenterIds = request.CostCenterIds,
                DestinationCityId = request.DestinationCityId,
                MachineTypeId = request.MachineTypeId,
                NumberPlates = request.NumberPlates,
                ProjectOperationDetailIds = request.ProjectOperationDetailIds,
                TripId = request.TripId,
                ProjectIds = request.ProjectIds,
                TransportationId = request.TransportationId,
                ProjectOperationIds = request.ProjectOperationIds,
                PostageDate = request.PostageDate,
                ReceivedDate = request.ReceivedDate,
                StartingCityId = request.StartingCityId,
                TransportationCostCategoryId = request.TransportationCostCategoryId,
                TransportationCostGroupId = request.TransportationCostGroupId,
                TransportationContractorId = request.TransportationContractorId,
            },
            true,
            ct);
        if (entitiesValidations.IsFailure)
            return Result.Failure<UpdateTransportationRequestResponse>(entitiesValidations.Error!);
        var dataValidate = entitiesValidations.Value;

        var response = await _mediator.Send(new UpdateTransportationRequestCommand(
            transporationRequest.Id, dataValidate?.Trip!, dataValidate?.Transportation!, dataValidate?.MachineType!,
            request.StartingCityId, request.DestinationCityId, request.StartDate, request.StartDate, request.ImageLink,
            request.Description, dataValidate?.CostCenters!, dataValidate?.Projects, dataValidate?.ProjectOperations,
            dataValidate?.ProjectOperationDetails, request.DriverId, request.DriverName, dataValidate?.BillOfLading,
            request.PostageDate, request.ReceivedDate, request.DelivererName, request.RecipientName,
            request.FreightNumber,
            request.LoadWeight, request.PhoneNumber, request.CarSpecifications, request.NumberPlates,
            request.BillOfLadingImage,
            request.AccountNumber, request.BankId, request.CardNumber, request.AccountName, request.IBAN, request.Price,
            request.CurrencyUnitId, request.AccountDescription, request.CarID,
            dataValidate!.Transportation!.IsPassenger,
            request.TransportationCostGroupId, request.TransportationCostCategoryId, request.StartingCityAddress,
            request.DestinationAddress, companyId, dataValidate?.TransportationContractor), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTransportationRequestResponse>(response.Error!);

        foreach (var document in transporationRequest.TransportationRequestDocuments)
        {
            var deleteDailyDocument =
                await _mediator.Send(new DeleteTransportationRequestDocumentCommand(document.Id), ct);
            if (deleteDailyDocument.IsFailure)
                return Result.Failure<UpdateTransportationRequestResponse>(deleteDailyDocument.Error!);
        }

        if (request.DocumentUrls != null && request.DocumentUrls.Count > 0)
        {
            var documentResult = await AddDocumentsToRequest(request.DocumentUrls, response.Value!, ct);
            if (documentResult.IsFailure)
                return Result.Failure<UpdateTransportationRequestResponse>(documentResult.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateTransportationRequestResponse(response.Value!.Id, true);
    }

    public async Task<Result<GetTransportationRequestByIdResponse?>> GetTransportationRequestById(
        GetTransportationRequestByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestById, id:{Id}", request.Id);

        var response = await _mediator.Send(new GetTransportationRequestByIdWithoutIncludeQuery(request.Id), ct);
        if (response.IsFailure || response is null || response.Value is null)
            return Result.Failure<GetTransportationRequestByIdResponse>(response!.Error!);
        var value = response.Value;

        Company? company = null;
        if (value.CompanyId is not null && value.CompanyId >= 1)
            company = await WebServicesLogic.CompanyDataReceiver(value.CompanyId, _mediator, ct);

        var dataRecieved = await GetTransportationRequestData(value, ct);

        value.RequestByName = dataRecieved.users?.FirstOrDefault(x => x.UserId == value.RequestById)?.FullName;
        value.ConfrimName = dataRecieved.users?.FirstOrDefault(x => x.UserId == value.ConfrimUserId)?.FullName;
        value.StartingCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.StartingCityId)?.Name;
        value.DestinationCityName = dataRecieved.cities?.FirstOrDefault(x => x.Id == value.DestinationCityId)?.Name;
        value.SecondDestinationCityName =
            dataRecieved.cities?.FirstOrDefault(x => x.Id == value.SecondDestinationCityId)?.Name;
        value.BankName = dataRecieved.bankInfo?.Name;
        value.CompanyNameFa = company?.NameFa;
        value.CurrencyUnitName = dataRecieved.currencyInfo?.Name;
        value.DriverUserName = dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == value.DriverId)
            ?.FullName;
        value.NumberPlatesModel = NumberPlates(value.NumberPlates);
        value.PhoneNumber = value.PhoneNumber is not null
            ? value.PhoneNumber.ToString()
            : dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == value.DriverId)?.DefaultPhoneNo;
        value.TicketPayer = dataRecieved.ticketPayers?.FirstOrDefault(x => x is not null && x.Id == value.TicketPayerId)
            ?.FullName;
        value.TransportationCostCategoryTitle = dataRecieved.costCategory?.CostCategoryTitle;
        value.TransportationCostCategoryCode = dataRecieved.costCategory?.CostCategoryCode;
        value.TransportationCostGroupTitle = dataRecieved.costGroup?.CostGroupTitle;
        value.TransportationCostGroupCode = dataRecieved.costGroup?.CostGroupCode;

        return value;
    }

    public async Task<Result<GetsFilteredTransportationRequestResponse?>> GetsFilteredTransportationRequest(
        GetsFilteredTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequest pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.PageIndex, request.PageSize);

        var response = await GetFilteredTransportationRequests(
            null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TransportationRequestStatus,
            request.PaymentType,
            request.TripId,
            request.BillOfLadingId,
            request.TransportationId,
            request.RequestById,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
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
            return Result.Failure<GetsFilteredTransportationRequestResponse>(TransportationRequestErrors
                .FilteredTransportationRequestNotFound);
        var values = response.Value.Data;

        return new GetsFilteredTransportationRequestResponse(
            values ?? new List<GetsFilteredTransportationRequestResponseModel>(0), response.Value.RowCount);
    }

    public async Task<Result<GetsFilteredRequesterResponse?>> GetsFilteredRequester(
        GetsFilteredRequesterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestRequesters");

        var response = await _mediator.Send(new GetsFilteredTransportationRequesterQuery(), ct);
        if (response.IsFailure || response is null)
            return Result.Failure<GetsFilteredRequesterResponse>(TransportationRequestErrors.RequesterIdsAreEmpty);
        var value = response!.Value!;

        List<FilteredUserResponseModel?> requesters = new();
        var data = new List<GetsFilteredRequesterResponseModel>();
        if (value.Data is not null && value.Data?.Count > 0)
        {
            var responseValue = await WebServicesLogic.UserDataReceiver(value.Data!, request.FilterData, _mediator, ct);
            if (responseValue is not null)
                requesters.AddRange(responseValue);

            if (!string.IsNullOrEmpty(request.FilterData))
                data = GetRequesterModelsWithFilter(value.Data, requesters);
            else
                data = GetRequesterModels(value.Data, requesters);
        }

        var responseData = data?.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsFilteredRequesterResponse(responseData ?? new List<GetsFilteredRequesterResponseModel>(0),
            requesters?.Count ?? 0);
    }

    public async Task<Result<GetsTransportationRequestHistoryResponse?>> GetsTransportationRequestHistory(
        GetsTransportationRequestHistoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequestHistory");

        var response = await _mediator.Send(new GetsTransportationRequestHistoryQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetsTransportationRequestHistoryResponse>(response.Error!);
        var value = response!.Value!;
        var dataResponse = value.Data;

        var dataRecieved = await GetsTransportationRequesHistorytData(dataResponse, ct);

        dataResponse.ForEach(item =>
        {
            item.Creator = dataRecieved.users?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.ConfirmUser = dataRecieved.users?.FirstOrDefault(x => x.UserId == item.ConfirmUserId)?.FullName;
            item.CurrencyName = dataRecieved.currencyInfos?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
            item.BankName = dataRecieved.bankInfos?.FirstOrDefault(x => x.Id == item.BankId)?.Name;
            item.DriverFullName = dataRecieved.thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.DriverId)
                ?.FullName;
        });

        dataResponse = dataResponse.SetPaging(request.PageIndex - 1, request.PageSize);
        value.RowCount = dataResponse?.Count ?? 0;

        return value;
    }

    public async Task<Result<GetsTransportationRequestExcelExporterResponse?>> GetsTransportationRequestExcelExporter(
        GetsTransportationRequestExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTransportationRequestExcelExporter");

        var response = await GetFilteredTransportationRequests(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TransportationRequestStatus,
            request.PaymentType,
            request.TripId,
            request.BillOfLadingId,
            request.TransportationId,
            request.RequestById,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
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
            return Result.Failure<GetsTransportationRequestExcelExporterResponse>(TransportationRequestErrors
                .FilteredTransportationRequestNotFound);
        var values = response.Value.Data;

        var data = values.Adapt<List<GetsTransportationRequestExcelExporterResponseModel>>();

        var file = new FileContentResult(
            TransportationRequestExcels.TransportationRequestToExcel(data!, request.ExcelFilters),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"TransportationRequests-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsTransportationRequestExcelExporterResponse(file);
    }

    public async Task<Result<GetsTransportationRequestExcelEnumResponse?>> GetsTransportationRequestExcelEnum(
        GetsTransportationRequestExcelEnumRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationRequestExcelEnum>());
        return new GetsTransportationRequestExcelEnumResponse(result);
    }

    public async Task<Result<GetsTransportationRequestTypeResponse?>> GetsTransportationRequestType(
        GetsTransportationRequestTypeRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationRequestStatus>());
        return new GetsTransportationRequestTypeResponse(result);
    }

    public async Task<Result<GetsTotalTransportationRequestPriceResponse?>> GetsTotalTransportationRequestPrice(
        GetsTotalTransportationRequestPriceRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsTotalTransportationRequestPrice");

        var response = await _mediator.Send(new GetsTotalTransportationRequestPriceQuery(
            null,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.CostGroupIds,
            request.CostCategoryIds,
            request.TransportationRequestStatus,
            request.PaymentType,
            request.TripId,
            request.BillOfLadingId,
            request.TransportationId,
            request.RequestById,
            request.StartDate,
            request.EndDate,
            request.RequestNumber,
            request.FromPrice,
            request.ToPrice,
            request.DriverName,
            null,
            request.FilterData,
            null,
            0,
            0), ct);

        return response.Value;
    }

    public async Task<Result<DisableTransportationRequestResponse?>> DisableTransportationRequest(
        DisableTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableTransportationRequest, Id:{Id}", request.Id);

        var response = await _mediator.Send(new DisableTransportationRequestCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableTransportationRequestResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableTransportationRequestResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<TransportationRequestGroupDeleteResponse?>> TransportationRequestGroupDelete(
        TransportationRequestGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for TransportationRequestGroupDelete, Ids:{Ids}", request.Ids);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableTransportationRequestCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<TransportationRequestGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new TransportationRequestGroupDeleteResponse(true);
    }

    public async Task<Result<GetsTransportationPaymentTypeResponse?>> GetsTransportationPaymentType(
        GetsTransportationPaymentTypeRequest request, CT ct)
    {
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<TransportationPaymentType>());
        return new GetsTransportationPaymentTypeResponse(result);
    }
}
