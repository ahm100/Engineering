using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Configs;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryById;
using Engineering.Application.Services.MachineryReservations.Commands.CreateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Commands.DisableMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservationStatus;
using Engineering.Application.Services.MachineryReservations.Models.CreateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.DisableMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationById;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservations;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationStatus;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationUnit;
using Engineering.Application.Services.MachineryReservations.Models.MachineryReservationGroupDelete;
using Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservationStatus;
using Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservationById;
using Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservations;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDate;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using IdentityServer.ClientSdk.Services;
using Microsoft.Extensions.Options;

namespace Engineering.Application.Services.MachineryReservations;

public partial class MachineryReservationLogic : IMachineryReservationLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<MachineryReservationLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpContext _context;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly ITelegramMessageHistoryLogic _telegramMessageHistoryLogic;
    private readonly IMessengerChannelRepository _messengerChannelRepo;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;

    public MachineryReservationLogic(IMediator mediator,
        ILogger<MachineryReservationLogic> logger,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor context,
        IMessengerChannelRepository messengerChannelRepo,
        IUserInfoService userInfoService,
        IUserProfileService userProfileService,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IOptionsSnapshot<MessageSenderConfig> options,
        IUserInfoProvider userInfoProvider)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _context = context.HttpContext!;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _messengerChannelRepo = messengerChannelRepo;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _messageSenderConfig = options.Value;
        _authorization = context.HttpContext?.Request.Headers.Authorization.ToString();
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<CreateMachineryReservationResponse?>> CreateMachineryReservation(
        CreateMachineryReservationRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateMachineryReservation");

        var isValidRequest = await request.IsValidAsync<CreateMachineryReservationValidator, CreateMachineryReservationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateMachineryReservationResponse>(isValidRequest.Error!);

        var fixMachineryQuery = await GetFixMachineryAsync(request.FixAssetMachineryId, ct);
        if (fixMachineryQuery.IsFailure || fixMachineryQuery.Value is null)
            return Result.Failure<CreateMachineryReservationResponse>(fixMachineryQuery.Error!);
        var fixMachinery = fixMachineryQuery.Value;

        var requestMachineryQuery = await GetRequestMachineryAsync(request.RequestMachineryId, ct);
        if (requestMachineryQuery.IsFailure || requestMachineryQuery.Value is null)
            return Result.Failure<CreateMachineryReservationResponse>(requestMachineryQuery.Error!);
        var requestMachinery = requestMachineryQuery.Value;

        if (request.EndDate.Date < request.StartDate.Date)
            return Result.Failure<CreateMachineryReservationResponse>(MachineryReservationErrors.InValidDates);

        var (startDate, endDate) = CalculateDates(request);

        var response = await _mediator.Send(new CreateMachineryReservationCommand(
            requestMachinery,
            fixMachinery,
            request.MachineryReservationUnit,
            startDate ?? request.StartDate.Date,
            endDate ?? request.EndDate.Date,
            request.Description), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<CreateMachineryReservationResponse>(response.Error!);
        var reserve = response.Value;

        var unit = GetUnit(reserve);

        var (confirmTime, totalHours) = CalculateConfirmTime(request, requestMachinery);
        if (confirmTime == null)
            return Result.Failure<CreateMachineryReservationResponse>(RequestMachineryErrors.TimeSpantCountError);

        var contractorId = fixMachinery!.FixAssetMachineryType == FixAssetMachineryType.rented ? fixMachinery.ContractorId : _userInfoProvider.CompanyThirdPartyId;

        var priceRate = GetPriceRate(request.MachineryReservationUnit, fixMachinery, request.StartDate.Date, request.EndDate.Date);
        if (priceRate == null || priceRate <= 0)
            return Result.Failure<CreateMachineryReservationResponse>(RequestMachineryErrors.InValidRate);

        var totalPrice = requestMachinery.RequestCount * priceRate * confirmTime;

        var currentUser = await _userProfileService.GetExtraInfo();

        if (requestMachinery.Status != RequestMachineryStatus.Confirmed)
        {
            var confirmedResponse = await ConfirmRequestMachineryStatus(requestMachinery, ct);
            if (confirmedResponse.IsFailure)
                return Result.Failure<CreateMachineryReservationResponse>(confirmedResponse.Error!);
        }

        if (fixMachinery.FixAssetMachineryType == FixAssetMachineryType.rented)
        {
            var inquiryResult = await HandleInquiryAsync(
                currentUser,
                requestMachinery,
                contractorId,
                priceRate.Value,
                totalPrice.Value,
                unit,
                confirmTime.Value,
                request.Description,
                ct);
            if (inquiryResult.IsFailure)
                return Result.Failure<CreateMachineryReservationResponse>(inquiryResult.Error!);
        }

        await HandleAssignmentAsync(fixMachinery, requestMachinery, ct);

        var updateResponse = await _mediator.Send(new UpdateRequestMachineryConfirmDateCommand(
            requestMachinery,
            confirmTime,
            request.Description,
            request.StartDate,
            request.StartTime,
            request.EndDate,
            request.EndTime,
            contractorId,
            null), ct);
        if (updateResponse.IsFailure)
            return Result.Failure<CreateMachineryReservationResponse>(updateResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        var userExtra = await _userProfileService.GetExtraInfo(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendMessageRequestMachinery(requestMachinery, fixMachinery, reserve, $"{userExtra.FirstName} {userExtra.LastName}", ct);

        return response.Value!.Adapt<CreateMachineryReservationResponse>();
    }

    public async Task<Result<DisableMachineryReservationResponse?>> DisableMachineryReservation(
        DisableMachineryReservationRequest request, CT ct)
    {
        _logger.LogInformation("Request for DisableMachineryReservation, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisableMachineryReservationValidator, DisableMachineryReservationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisableMachineryReservationResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableMachineryReservationCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisableMachineryReservationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisableMachineryReservationResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<MachineryReservationGroupDeleteResponse?>> MachineryReservationGroupDelete(
        MachineryReservationGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryReservationGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<MachineryReservationGroupDeleteValidator, MachineryReservationGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<MachineryReservationGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableMachineryReservationCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<MachineryReservationGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new MachineryReservationGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateMachineryReservationResponse?>> UpdateMachineryReservation(
        UpdateMachineryReservationRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateMachineryReservation, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateMachineryReservationValidator, UpdateMachineryReservationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateMachineryReservationResponse>(isValidRequest.Error!);

        var machineryReserveQuery = await _mediator.Send(new GetMachineryReservationByIdQuery(request.Id), ct);
        if (machineryReserveQuery.IsFailure || machineryReserveQuery.Value is null)
            return Result.Failure<UpdateMachineryReservationResponse>(MachineryReservationErrors.MachineryReservationNotFoundWithId);
        var machineryReserve = machineryReserveQuery.Value;

        if (machineryReserve.Status != MachineryReservationStatus.NotsStarted)
            return Result.Failure<UpdateMachineryReservationResponse>(MachineryReservationErrors.InValidStatus);

        FixAssetMachinery? fixMachinery = null;
        if (request.FixAssetMachineryId != machineryReserve.FixAssetMachinery.Id)
        {
            var fixMachineryQuery = await _mediator.Send(new GetFixAssetMachineryByIdQuery(request.FixAssetMachineryId), ct);
            if (fixMachineryQuery.IsFailure)
                return Result.Failure<UpdateMachineryReservationResponse>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);
            fixMachinery = fixMachineryQuery.Value;
        }
        else
            fixMachinery = machineryReserve.FixAssetMachinery;

        RequestMachinery? requestMachinery = null;
        if (request.FixAssetMachineryId != machineryReserve.FixAssetMachinery.Id)
        {
            var requestMachineryQuery = await _mediator.Send(new GetRequestMachineryByIdQuery(request.RequestMachineryId), ct);
            if (requestMachineryQuery.IsFailure)
                return Result.Failure<UpdateMachineryReservationResponse>(RequestMachineryErrors.RequestMachineryNotFound);
            requestMachinery = requestMachineryQuery.Value;
        }
        else
            requestMachinery = machineryReserve.RequestMachinery;

        if (requestMachinery!.Machinery.Id != fixMachinery!.Machinery.Id)
            return Result.Failure<UpdateMachineryReservationResponse>(MachineryReservationErrors.InvalidMachinery);

        DateTime? startDate = null;
        if (request.StartTime != null)
            startDate = request.StartDate.Date.Add(request.StartTime.Value);

        DateTime? endDate = null;
        if (request.EndTime != null)
            endDate = request.EndDate.Date.Add(request.EndTime.Value);

        var response = await _mediator.Send(new UpdateMachineryReservationCommand(machineryReserve.Id, requestMachinery!, fixMachinery!, request.MachineryReservationUnit,
            startDate != null ? startDate.Value : request.StartDate.Date, endDate != null ? endDate.Value : request.EndDate.Date, request.Description), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateMachineryReservationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateMachineryReservationResponse>();
    }

    public async Task<Result<UpdateMachineryReservationStatusResponse?>> MachineryReservationStatusChanger(
        UpdateMachineryReservationStatusRequest request, CT ct)
    {
        _logger.LogInformation("Request for MachineryReservationStatusChanger, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateMachineryReservationStatusValidator, UpdateMachineryReservationStatusRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateMachineryReservationStatusResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new UpdateMachineryReservationStatusCommand(request.Id, request.MachineryReservationStatus), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateMachineryReservationStatusResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateMachineryReservationStatusResponse(response.Value!.Id);
    }

    public async Task<Result<GetMachineryReservationByIdResponse?>> GetMachineryReservationById(
        GetMachineryReservationByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetMachineryReservationById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetMachineryReservationByIdValidator, GetMachineryReservationByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineryReservationByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineryReservationByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineryReservationByIdResponse>(response.Error!);
        var value = response.Value!;

        var data = value.Adapt<GetMachineryReservationByIdResponse>();
        return data;
    }

    public async Task<Result<GetMachineryReservationsResponse?>> GetsMachineryReservation(
        GetMachineryReservationsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsMachineryReservation");

        var isValidRequest = await request.IsValidAsync<GetMachineryReservationsValidator, GetMachineryReservationsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMachineryReservationsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetMachineryReservationsQuery(null, request.MachineryIds, request.FixAssetMachineryIds,
            request.RequestMachineryIds, request.Unit, request.Status, request.StartDate, request.EndDate, request.FilterData,
            request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetMachineryReservationsResponse>(response.Error!);
        var values = response.Value!.Data;

        var data = values.Adapt<List<GetMachineryReservationsModel>>();

        return new GetMachineryReservationsResponse(data ?? new List<GetMachineryReservationsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetMachineryReservationUnitResponse?>> GetMachineryReservationUnit(
        GetMachineryReservationUnitRequest request, CT ct)
    {
        var response = await Task.Run(() =>
        {
            return EnumExt.GetEnumObjectList<MachineryReservationUnit>();
        });

        return new GetMachineryReservationUnitResponse(response);
    }

    public async Task<Result<GetMachineryReservationStatusResponse?>> GetMachineryReservationStatus(
        GetMachineryReservationStatusRequest request, CT ct)
    {
        var response = await Task.Run(() =>
        {
            return EnumExt.GetEnumObjectList<MachineryReservationStatus>();
        });

        return new GetMachineryReservationStatusResponse(response);
    }
}

