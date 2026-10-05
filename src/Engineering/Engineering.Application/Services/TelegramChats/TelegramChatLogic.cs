using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Configs;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterById;
using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByIds;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetCostCenterWarehousesByWarehouseId;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetSupplyProductByCommercialRequestId;
using Engineering.Application.Services.TelegramChats.Commands.ActiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Commands.CreateTelegramChat;
using Engineering.Application.Services.TelegramChats.Commands.DeleteTelegramChat;
using Engineering.Application.Services.TelegramChats.Commands.InactiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Commands.StateChangerTelegramChats;
using Engineering.Application.Services.TelegramChats.Commands.UpdateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.AcceptPaymentOrderTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ActiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.CommerceRequestWarehouseStatusChangeTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.CommercialPaymentTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ConsumerExitTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ContractorStatementPaymentMessage;
using Engineering.Application.Services.TelegramChats.Models.CreateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.DeleteTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.EnteringToWarehouseTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.EntryThroughRelocationForTemporaryDeliveryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ExitForRelocationTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.ExitRelocationForTemporaryDeliveryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.GetActiveTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.GetByCostCenterId;
using Engineering.Application.Services.TelegramChats.Models.GetsTelegramChatByCostCenterIds;
using Engineering.Application.Services.TelegramChats.Models.GetsTelegramMessageType;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChatById;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChatByName;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.InactiveTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.PaymentTreasuryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.SendMessage;
using Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;
using Engineering.Application.Services.TelegramChats.Models.TelegramChatGroupDelete;
using Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;
using Engineering.Application.Services.TelegramChats.Models.TemporaryDeliveryTelegramMessage;
using Engineering.Application.Services.TelegramChats.Models.UpdateTelegramChat;
using Engineering.Application.Services.TelegramChats.Models.UserChangedTelegramMessage;
using Engineering.Application.Services.TelegramChats.Queries.GetActiveTelegramChats;
using Engineering.Application.Services.TelegramChats.Queries.GetByCostCenterId;
using Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByCostCenterIds;
using Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByIds;
using Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatById;
using Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatByName;
using Engineering.Application.Services.TelegramChats.Queries.GetTelegramChats;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.TelegramChats.Enums;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;
using Microsoft.Extensions.Options;
using WarehouseEntity = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Models.Warehouse;

namespace Engineering.Application.Services.TelegramChats;

public partial class TelegramChatLogic : ITelegramChatLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TelegramChatLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly ITelegramMessageHistoryLogic _telegramMessageHistoryLogic;
    private readonly IMessengerChannelRepository _messengerChannelRepository;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;

    public TelegramChatLogic(
        IMediator mediator,
        ILogger<TelegramChatLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IUserProfileService userProfileService,
        IOptionsSnapshot<MessageSenderConfig> options,
        IHttpContextAccessor httpContextAccessor,
        IMessengerChannelRepository messengerChannelRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _userInfoService = userInfoService;
        _userProfileService = userProfileService;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _messengerChannelRepository = messengerChannelRepository;
        _messageSenderConfig = options.Value;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
    }

    public async Task<Result<CreateTelegramChatResponse?>> CreateTelegramChat(CreateTelegramChatRequest request,
        CT ct)
    {
        _logger.LogInformation(
            "Request for CreateTelegramChat, TelegramChatName:{TelegramChatName},", request.ChatName);

        var isValidRequest =
            await request.IsValidAsync<CreateTelegramChatValidator, CreateTelegramChatRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateTelegramChatResponse>(isValidRequest.Error!);

        if (request.ChatName is not null)
        {
            var nameIsDuplicate = await _mediator.Send(new GetTelegramChatByNameQuery(request.ChatName),
                ct);
            if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
                return Result.Failure<CreateTelegramChatResponse>(TelegramChatErrors.NameIsDuplicate);
        }

        Project? project = null;
        if (request.ProjectId is not null)
        {
            var getProject =
                await _mediator.Send(new GetProjectByIdQuery(request.ProjectId.Value), ct);
            if (getProject.IsFailure)
                return Result.Failure<CreateTelegramChatResponse>(TelegramChatErrors.TelegramChatProjectIdNotFound);
            project = getProject.Value;
        }

        var getCostCenter =
            await _mediator.Send(new GetCostCenterByIdQuery(request.CostCenterId), ct);
        if (getCostCenter.IsFailure)
            return Result.Failure<CreateTelegramChatResponse>(TelegramChatErrors.TelegramChatCostCenterNotFound);

        var response =
            await _mediator.Send(new CreateTelegramChatCommand(getCostCenter!.Value!, project, request.ChatName,
                    request.ChatUrl, request.ChatId, request.Description, request.IsActive, request.Types),
                ct);
        if (response.IsFailure)
            return Result.Failure<CreateTelegramChatResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<CreateTelegramChatResponse>();
    }

    public async Task<Result<DeleteTelegramChatResponse?>> DeleteTelegramChat(DeleteTelegramChatRequest request,
        CT ct)
    {
        _logger.LogInformation("Request for DeleteTelegramChat, Id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<DeleteTelegramChatValidator, DeleteTelegramChatRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteTelegramChatResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteTelegramChatCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteTelegramChatResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteTelegramChatResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<TelegramChatGroupDeleteResponse?>> TelegramChatGroupDelete(
        TelegramChatGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for TelegramChatGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest =
            await request.IsValidAsync<TelegramChatGroupDeleteValidator, TelegramChatGroupDeleteRequest>(
                ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<TelegramChatGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteTelegramChatCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<TelegramChatGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new TelegramChatGroupDeleteResponse(true);
    }

    public async Task<Result<UpdateTelegramChatResponse?>> UpdateTelegramChat(UpdateTelegramChatRequest request,
        CT ct)
    {
        _logger.LogInformation(
            "Request for UpdateTelegramChat, id:{Id}, TelegramChat:{ChatName}", request.Id, request.ChatName);

        var isValidRequest =
            await request.IsValidAsync<UpdateTelegramChatValidator, UpdateTelegramChatRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateTelegramChatResponse>(isValidRequest.Error!);


        if (request.ChatName is not null)
        {
            var nameIsDuplicate =
                await _mediator.Send(new GetTelegramChatByNameQuery(request.ChatName), ct);
            if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
                return Result.Failure<UpdateTelegramChatResponse>(TelegramChatErrors.NameIsDuplicate);
        }

        Project? project = null;
        if (request.ProjectId is not null)
        {
            var getProject =
                await _mediator.Send(new GetProjectByIdQuery(request.ProjectId.Value), ct);
            if (getProject.IsFailure)
                return Result.Failure<UpdateTelegramChatResponse>(TelegramChatErrors.TelegramChatProjectIdNotFound);
            project = getProject.Value;
        }

        var getCostCenter =
            await _mediator.Send(new GetCostCenterByIdQuery(request.CostCenterId), ct);
        if (getCostCenter.IsFailure)
            return Result.Failure<UpdateTelegramChatResponse>(TelegramChatErrors.TelegramChatCostCenterNotFound);

        var response =
            await _mediator.Send(new UpdateTelegramChatCommand(request.Id, getCostCenter.Value!, project,
                    request.ChatName,
                    request.ChatUrl, request.ChatId, request.Description, request.IsActive, request.Types),
                ct);
        if (response.IsFailure)
            return Result.Failure<UpdateTelegramChatResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return response.Value!.Adapt<UpdateTelegramChatResponse>();
    }

    public async Task<Result<InactiveTelegramChatResponse?>> InactiveTelegramChat(InactiveTelegramChatRequest request,
        CT ct)
    {
        _logger.LogInformation("Request for InactiveTelegramChat, Id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<InactiveTelegramChatValidator, InactiveTelegramChatRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveTelegramChatResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveTelegramChatCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveTelegramChatResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new InactiveTelegramChatResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<ActiveTelegramChatResponse?>> ActiveTelegramChat(ActiveTelegramChatRequest request,
        CT ct)
    {
        _logger.LogInformation("Request for  ActiveTelegramChat, Id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<ActiveTelegramChatValidator, ActiveTelegramChatRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveTelegramChatResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveTelegramChatCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveTelegramChatResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ActiveTelegramChatResponse(response.Value!.Id, response.Value!.IsActive);
    }

    public async Task<Result<StateChangerTelegramChatsResponse?>> StateChangerTelegramChats(
        StateChangerTelegramChatsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerTelegramChats, Id:{Id}", request.Ids);

        var isValidRequest =
            await request.IsValidAsync<StateChangerTelegramChatsValidator, StateChangerTelegramChatsRequest>(
                ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerTelegramChatsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerTelegramChatsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsTelegramChatByIdsQuery(request.Ids), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Count <= 0)
            return Result.Failure<StateChangerTelegramChatsResponse>(responses.Error!);
        var values = responses.Value;

        var response = await _mediator.Send(new StateChangerTelegramChatsCommand(values, request.State),
            ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerTelegramChatsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerTelegramChatsResponse(true);
    }

    public async Task<Result<GetTelegramChatByIdResponse?>> GetTelegramChatById(GetTelegramChatByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("Request for GetTelegramChatById, id:{Id}", request.Id);

        var isValidRequest =
            await request.IsValidAsync<GetTelegramChatByIdValidator, GetTelegramChatByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTelegramChatByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTelegramChatByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetTelegramChatByIdResponse>(response.Error!);
        var value = response.Value!;

        var data = value.Adapt<GetTelegramChatByIdResponse>();
        return data;
    }

    public async Task<Result<GetsTelegramMessageTypeResponse?>> GetsTelegramMessageType(GetsTelegramMessageTypeRequest request,
        CT ct)
    {
        _logger.LogInformation("Request for GetsTelegramMessageType");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<TelegramMessageType>());
        return new GetsTelegramMessageTypeResponse(result);
    }

    public async Task<Result<GetTelegramChatByNameResponse?>> GetTelegramChatByName(
        GetTelegramChatByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTelegramChatByName ChatName:{ChatName}",
            request.ChatName);

        var isValidRequest =
            await request.IsValidAsync<GetTelegramChatByNameValidator, GetTelegramChatByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTelegramChatByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTelegramChatByNameQuery(request.ChatName),
            ct);
        if (response.IsFailure)
            return Result.Failure<GetTelegramChatByNameResponse>(response.Error!);
        var value = response.Value!;

        var data = value.Adapt<GetTelegramChatByNameResponse>();
        return data;
    }

    public async Task<Result<GetActiveTelegramChatsResponse?>> GetsActiveTelegramChat(
        GetActiveTelegramChatsRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for GetActiveTelegramChat,, TelegramChatName:{Name}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.Name, request.PageIndex, request.PageSize);

        var isValidRequest =
            await request.IsValidAsync<GetActiveTelegramChatsValidator, GetActiveTelegramChatsRequest>(
                ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveTelegramChatsResponse>(isValidRequest.Error!);

        var response =
            await _mediator.Send(
                new GetActiveTelegramChatsQuery(request.FilterData, request.CostCenterId, request.ProjectId,
                    request.Name, true, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveTelegramChatsResponse>(response.Error!);
        var values = response.Value!.Data;
        var data = values.Adapt<List<GetsActiveTelegramChatModel>>();

        return new GetActiveTelegramChatsResponse(data ?? new List<GetsActiveTelegramChatModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetTelegramChatsResponse?>> GetsTelegramChat(GetTelegramChatsRequest request,
        CT ct)
    {
        _logger.LogInformation(
            "Request for GetsTelegramChat,:{ChatName} , IsActive:{IsActive}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.ChatName, request.IsActive, request.PageIndex, request.PageSize);

        var isValidRequest =
            await request.IsValidAsync<GetTelegramChatsValidator, GetTelegramChatsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetTelegramChatsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetTelegramChatsQuery(request.Ids, request.TelegramMessageType,
            request.FilterData, request.CostCenterId, request.ProjectId, request.ChatName,
            request.IsActive, request.OrderBy, true, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetTelegramChatsResponse>(response.Error!);
        var values = response.Value!.Data;

        var data = values.Adapt<List<GetTelegramChatsWithChildModel>>();

        return new GetTelegramChatsResponse(data ?? new List<GetTelegramChatsWithChildModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetByCostCenterIdResponse?>> GetByCostCenterId(
        GetByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for GetByCostCenterId, CostCenterId:{CostCenterId}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.CostCenterId, request.PageIndex, request.PageSize);

        var isValidRequest =
            await request.IsValidAsync<GetByCostCenterIdValidator, GetByCostCenterIdRequest>(
                ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetByCostCenterIdResponse>(isValidRequest.Error!);

        var response =
            await _mediator.Send(
                new GetByCostCenterIdQuery(request.CostCenterId, request.ProjectId, true, request.PageIndex,
                    request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetByCostCenterIdResponse>(response.Error!);
        var values = response.Value!.Data;
        var data = values.Adapt<List<GetTelegramChatsWithChildModel>>();

        return new GetByCostCenterIdResponse(data ?? new List<GetTelegramChatsWithChildModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsTelegramChatByCostCenterIdsResponse?>> GetsTelegramChatByCostCenterIds(
        GetsTelegramChatByCostCenterIdsRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for GetsTelegramChatByCostCenterIdsRequest, CostCenterId:{CostCenterIds}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.CostCenterIds, request.PageIndex, request.PageSize);

        var isValidRequest =
            await request
                .IsValidAsync<GetsTelegramChatByCostCenterIdsRequestValidator, GetsTelegramChatByCostCenterIdsRequest>(
                    ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTelegramChatByCostCenterIdsResponse>(isValidRequest.Error!);

        var response =
            await _mediator.Send(
                new GetsTelegramChatByCostCenterIdsQuery(
                    request.CostCenterIds,
                    request.TelegramMessageType,
                    request.FilterData,
                    request.IsActive,
                    true,
                    request.PageIndex,
                    request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsTelegramChatByCostCenterIdsResponse>(response.Error!);
        var values = response.Value!.Data;
        var data = values.Adapt<List<GetsActiveTelegramChatModel>>();

        return new GetsTelegramChatByCostCenterIdsResponse(data ?? new List<GetsActiveTelegramChatModel>(0),
            response.Value?.RowCount ?? 0);
    }


    //Send Messages

    public async Task<Result<CommercialPaymentTelegramMessageResponse?>> SendCommercialPaymentMessage(
        CommercialPaymentTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.ProjectId is not null)
            {
                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel([request.ProjectId.Value], null, MessengerMessageType.Payment, ct);
                if (getTelegramChats is not null)
                {
                    List<Guid> ids = new();
                    if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                        foreach (var item in request.DocumentUrls)
                            ids.Add(Guid.Parse(item));

                    var resultUrl = request.DocumentUrls != null
                        ? string.Join(",", request.DocumentUrls)
                        : string.Empty;

                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate.Value.ToString("HH:mm:ss");

                        var message = CommercialPaymentMessageModel(request.ShabaNo, request.Amount, createDateShamsi, createTime, request.Reason, request.Description, request.ThirdPartyName, ct);
                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new CommercialPaymentTelegramMessageResponse(true);
    }

    public async Task<Result<CommercialPaymentTelegramMessageResponse?>> SendCommercialPaymentTelegramMessage(
        CommercialPaymentTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.ProjectId is not null)
            {
                var getTelegramChats = await _mediator.Send(new GetTelegramChatsQuery(
                    null,
                    TelegramMessageType.Payment,
                    null,
                    request.ProjectId,
                    null,
                    null,
                    true,
                    request.OrderBy,
                    true,
                    0,
                    0), ct);

                if (getTelegramChats.Value?.Data is not null)
                {
                    List<Guid> ids = new();
                    if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                        foreach (var item in request.DocumentUrls)
                            ids.Add(Guid.Parse(item));

                    var resultUrl = request.DocumentUrls != null
                        ? string.Join(",", request.DocumentUrls)
                        : string.Empty;

                    foreach (var item in getTelegramChats.Value.Data)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
#pragma warning disable CS8629 // Nullable value type may be null.
                        var createTime = request.CreateDate.Value.ToString("HH:mm:ss");
#pragma warning restore CS8629 // Nullable value type may be null.
                        var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType == TelegramMessageType.Payment ||
                            x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.CommercialPaymentMessage(
                                    chat!.ChatId,
                                    request.DocumentNumber,
                                    request.ShabaNo,
                                    request.Amount,
                                    createDateShamsi,
                                    createTime,
                                    request.Reason,
                                    request.Description,
                                    request.ThirdPartyName,
                                    ids,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    resultUrl,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }


        return new CommercialPaymentTelegramMessageResponse(true);
    }

    public async Task<Result<TemporaryDeliveryTelegramMessageResponse?>> SendTemporaryDeliveryMessage(
        TemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getCostCenterWarehousesByWarehouseId =
            await _mediator.Send(
                new GetCostCenterWarehousesByWarehouseIdQuery(new List<long>() { request.WarehouseId }),
                ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();

                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.WarehouseEntry, ct);

                if (getTelegramChats is not null && getTelegramChats.Count > 0)
                {
                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var enterDateShamsi = TimeCalculator.ConvertToShamsi(request.EnterDate);
                        var enterTime = request.EnterDate!.Value.ToString("HH:mm:ss");

                        var message = TemporaryDeliveryMessageModel(request.WarehouseName,
                                    request.DestinationWarehouseName,
                                    request.Products,
                                    request.ReceiverDelivery,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    enterDateShamsi,
                                    enterTime,
                                    request.Creator,
                                    request.ConfirmCreator, ct);
                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new TemporaryDeliveryTelegramMessageResponse(true);
    }

    public async Task<Result<TemporaryDeliveryTelegramMessageResponse?>> SendTemporaryDeliveryTelegramMessage(
        TemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getCostCenterWarehousesByWarehouseId =
            await _mediator.Send(
                new GetCostCenterWarehousesByWarehouseIdQuery(new List<long>() { request.WarehouseId }),
                ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats =
                    await _mediator.Send(
                        new GetsTelegramChatByCostCenterIdsQuery(
                            costCenterIds,
                            TelegramMessageType.TemporaryDelivery,
                            null,
                            null,
                            true,
                            0,
                            0), ct);

                if (getTelegramChats.Value?.Data is not null)
                {
                    foreach (var item in getTelegramChats.Value.Data)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var enterDateShamsi = TimeCalculator.ConvertToShamsi(request.EnterDate);
                        var enterTime = request.EnterDate!.Value.ToString("HH:mm:ss");

                        var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType == TelegramMessageType.TemporaryDelivery ||
                            x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.TemporaryDeliveryMessage(
                                    chat!.ChatId,
                                    request.WarehouseName,
                                    request.DestinationWarehouseName,
                                    request.Products,
                                    request.ReceiverDelivery,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    enterDateShamsi,
                                    enterTime,
                                    request.Creator,
                                    request.ConfirmCreator,
                                    ids,
                                    _mediator,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }

        return new TemporaryDeliveryTelegramMessageResponse(true);
    }

    public async Task<Result<CommercialPackingTelegramMessageResponse?>> SendCommercialPackingMessage(
        CommercialPackingTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var costCenterIds = request.Products?
            .Select(x => x.CostCenterId)
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();

            if (costCenterIds == null || !costCenterIds.Any())
            {
                return new CommercialPackingTelegramMessageResponse(false);
            }

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.Packing, ct);
            if (getTelegramChatByCostCenterIds is null || getTelegramChatByCostCenterIds.Count < 1)
            {
                return new CommercialPackingTelegramMessageResponse(false);
            }

            // Get CostCenters
            var getsCostCenterByIds = await _mediator.Send(new GetsCostCenterByIdsQuery(
                costCenterIds,
                PreferentialReferenceCodes: null,
                null,
                0,
                0), ct);

            var costCenters = getsCostCenterByIds.Value?.Data;

            var telegramChats = getTelegramChatByCostCenterIds;
            if (telegramChats is not null && telegramChats.Any())
            {

                var warehouseIds = request.Products?.Select(x => x.WarehouseId).Distinct().ToList();

                List<WarehouseEntity>? warehouses = new();
                if (warehouseIds is not null)
                {
                    var warehousesResult =
                        await _mediator.Send(new GetsWarehouseByIdQuery(1, warehouseIds.Count, warehouseIds.ToList()),
                            ct);
                    warehouses = warehousesResult.Value?.Data;
                }

                var groupedProducts = request.Products?
                    .GroupBy(product => new { product.WarehouseId, product.CostCenterId })
                    .ToList();

                var warehouseProductGroups = new List<WarehouseProductGroupModel>();
                if (groupedProducts is not null)
                {
                    foreach (var group in groupedProducts)
                    {
                        var warehouseName = warehouses?.FirstOrDefault(w => w.Id == group.Key.WarehouseId)?.Name ?? "";
                        var costCenterName = costCenters?.FirstOrDefault(x => x.Id == group.Key.CostCenterId)?.CostCenterName ?? "";


                        var productDetails = group.Select(product => new ProductDetailsModel
                        {
                            RequestNumber = product.RequestNumber,
                            ProductName = product.ProductName,
                            Quantity = product.Quantity,
                            RequestCount = product.RequestCount,
                            MeasureUnitName = product.MeasureUnitName,
                        }).ToList();

                        warehouseProductGroups.Add(new WarehouseProductGroupModel
                        {
                            WarehouseName = warehouseName,
                            CostCenterName = costCenterName,
                            Products = productDetails
                        });
                    }
                }

                var createDateShamsi = request.CreateDate.HasValue
                    ? TimeCalculator.ConvertToShamsi(request.CreateDate)
                    : string.Empty;
                var createTime = request.CreateDate.HasValue ? request.CreateDate.Value.ToString("HH:mm:ss") : string.Empty;

                var deliveryDateShamsi = TimeCalculator.ConvertToShamsi(request.DeliveryDate) ?? string.Empty;
                var deliveryTime = request.DeliveryDate.HasValue
                    ? request.DeliveryDate.Value.ToString("HH:mm:ss")
                    : string.Empty;

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var message = CommercialPackingMessageModel(request.IsUpdated,
                            request.SupplierName,
                            request.ThirdParty,
                            request.FollowupName,
                            request.DestinationWarehouseName,
                            request.RequestNumber,
                            warehouseProductGroups,
                            request.Description,
                            createDateShamsi,
                            createTime,
                            deliveryDateShamsi,
                            deliveryTime, ct);

                foreach (var item in telegramChats)
                    await TelegramServicesLogic.SendMessage(item, message, null, _mediator, _authorization, _messageSenderConfig, ct);
            }
        }

        return new CommercialPackingTelegramMessageResponse(true);
    }

    public async Task<Result<CommercialPackingTelegramMessageResponse?>> SendCommercialPackingTelegramMessage(
        CommercialPackingTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var costCenterIds = request.Products?
            .Select(x => x.CostCenterId)
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();

            if (costCenterIds == null || !costCenterIds.Any())
            {
                return new CommercialPackingTelegramMessageResponse(false);
            }

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
                costCenterIds,
                TelegramMessageType.Packing,
                null,
                null,
                true,
                0,
                0), ct);
            if (getTelegramChatByCostCenterIds.IsFailure || getTelegramChatByCostCenterIds.Value is null)
            {
                return new CommercialPackingTelegramMessageResponse(false);
            }

            // Get CostCenters
            var getsCostCenterByIds = await _mediator.Send(new GetsCostCenterByIdsQuery(
                costCenterIds,
                PreferentialReferenceCodes: null,
                null,
                0,
                0), ct);
            var costCenters = getsCostCenterByIds.Value?.Data;

            var telegramChats = getTelegramChatByCostCenterIds.Value?.Data;
            if (telegramChats is not null && telegramChats.Any())
            {

                var warehouseIds = request.Products?.Select(x => x.WarehouseId).Distinct().ToList();

                List<WarehouseEntity>? warehouses = new();
                if (warehouseIds is not null)
                {
                    var warehousesResult =
                        await _mediator.Send(new GetsWarehouseByIdQuery(1, warehouseIds.Count, warehouseIds.ToList()),
                            ct);
                    warehouses = warehousesResult.Value?.Data;
                }

                var groupedProducts = request.Products?
                    .GroupBy(product => new { product.WarehouseId, product.CostCenterId })
                    .ToList();

                var warehouseProductGroups = new List<WarehouseProductGroupModel>();
                if (groupedProducts is not null)
                {
                    foreach (var group in groupedProducts)
                    {
                        var warehouseName = warehouses?.FirstOrDefault(w => w.Id == group.Key.WarehouseId)?.Name ?? "";
                        var costCenterName = costCenters?.FirstOrDefault(x => x.Id == group.Key.CostCenterId)?.CostCenterName ?? "";


                        var productDetails = group.Select(product => new ProductDetailsModel
                        {
                            RequestNumber = product.RequestNumber,
                            ProductName = product.ProductName,
                            Quantity = product.Quantity,
                            RequestCount = product.RequestCount,
                            MeasureUnitName = product.MeasureUnitName,
                        }).ToList();

                        warehouseProductGroups.Add(new WarehouseProductGroupModel
                        {
                            WarehouseName = warehouseName,
                            CostCenterName = costCenterName,
                            Products = productDetails
                        });
                    }
                }

                var createDateShamsi = request.CreateDate.HasValue
                    ? TimeCalculator.ConvertToShamsi(request.CreateDate)
                    : string.Empty;
                var createTime = request.CreateDate.HasValue ? request.CreateDate.Value.ToString("HH:mm:ss") : string.Empty;

                var deliveryDateShamsi = TimeCalculator.ConvertToShamsi(request.DeliveryDate) ?? string.Empty;
                var deliveryTime = request.DeliveryDate.HasValue
                    ? request.DeliveryDate.Value.ToString("HH:mm:ss")
                    : string.Empty;

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var validChats = telegramChats
                    .SelectMany(chat => chat.TelegramChatTypes)
                    .Where(x => x.TelegramMessageType is TelegramMessageType.Packing or TelegramMessageType.OtherGroups)
                    .GroupBy(x => x.ChatId)
                    .Select(g => g.FirstOrDefault())
                    .Where(chat => chat is not null)
                    .ToList();

                if (validChats.Any())
                {
                    //send Message
                    foreach (var chat in validChats)
                    {
                        await TelegramServicesLogic.CommercialPackingMessage(
                            request.IsUpdated,
                            chat!.ChatId,
                            request.SupplierName,
                            request.ThirdParty,
                            request.FollowupName,
                            request.DestinationWarehouseName,
                            request.RequestNumber,
                            warehouseProductGroups,
                            request.Description,
                            createDateShamsi,
                            createTime,
                            deliveryDateShamsi,
                            deliveryTime,
                            null,
                            null,
                            request.File,
                            _telegramMessageHistoryLogic,
                            chat.TelegramChat.Id,
                            resultUrl,
                            chat.TelegramMessageType,
                            _authorization,
                            _messageSenderConfig,
                            ct
                        );
                    }
                }
            }
        }

        return new CommercialPackingTelegramMessageResponse(true);
    }

    public async Task<Result<ConsumerExitTelegramMessageResponse?>> SendConsumerExitMessage(
        ConsumerExitTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getCostCenterWarehousesByWarehouseId =
            await _mediator.Send(
                new GetCostCenterWarehousesByWarehouseIdQuery(new List<long>() { request.WarehouseId }), ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.CommerceRequestWarehouseStatusChange, ct);

                if (getTelegramChats is not null && getTelegramChats.Count > 0)
                {
                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate.Value);
                        var createTime = request.CreateDate.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate.Value);
                        var exitTime = request.ExitDate.Value.ToString("HH:mm:ss");

                        var message = ConsumerExitMessageModel(request.WarehouseName,
                            request.DocumentNumber,
                            request.Products,
                            request.RequestNumber,
                            request.ThirdPartyName,
                            request.ReceiverDelivery,
                            projectOperationName,
                            createDateShamsi,
                            createTime,
                            exitDateShamsi,
                            exitTime,
                            request.Creator,
                            request.ConfirmCreator, ct);

                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new ConsumerExitTelegramMessageResponse(true);
    }

    public async Task<Result<ConsumerExitTelegramMessageResponse?>> SendConsumerExitTelegramMessage(
        ConsumerExitTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getCostCenterWarehousesByWarehouseId =
            await _mediator.Send(
                new GetCostCenterWarehousesByWarehouseIdQuery(new List<long>() { request.WarehouseId }), ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats =
                    await _mediator.Send(
                        new GetsTelegramChatByCostCenterIdsQuery(
                            costCenterIds,
                            TelegramMessageType.ConsumerExit,
                            null,
                            null,
                            true,
                            0,
                            0), ct);

                if (getTelegramChats.Value?.Data is not null)
                {
                    foreach (var item in getTelegramChats.Value.Data)
                    {
#pragma warning disable CS8629 // Nullable value type may be null.
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate.Value);

                        var createTime = request.CreateDate.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate.Value);
                        var exitTime = request.ExitDate.Value.ToString("HH:mm:ss");
#pragma warning restore CS8629 // Nullable value type may be null. 

                        var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType == TelegramMessageType.ConsumerExit ||
                            x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.ConsumerExitMessage(
                                    chat!.ChatId,
                                    request.WarehouseName,
                                    request.DocumentNumber,
                                    request.Products,
                                    request.RequestNumber,
                                    request.ThirdPartyName,
                                    request.ReceiverDelivery,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    exitDateShamsi,
                                    exitTime,
                                    request.Creator,
                                    request.ConfirmCreator,
                                    ids,
                                    _mediator,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    resultUrl,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }
        return new ConsumerExitTelegramMessageResponse(true);
    }

    public async Task<Result<EnteringToWarehouseTelegramMessageResponse?>> SendEnteringToWarehouseMessage(
        EnteringToWarehouseTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getCostCenterWarehousesByWarehouseId =
            await _mediator.Send(
                new GetCostCenterWarehousesByWarehouseIdQuery(new List<long>() { request.WarehouseId }),
                ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.WarehouseEntry, ct);

                if (getTelegramChats is not null && getTelegramChats.Count > 0)
                {
                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate!.Value);
                        var createTime = request.CreateDate.Value.ToString("HH:mm:ss");
                        var enterDateShamsi = TimeCalculator.ConvertToShamsi(request.EnterDate!.Value);
                        var enterTime = request.EnterDate.Value.ToString("HH:mm:ss");

                        var resultUrl = request.DocumentUrls != null
                            ? string.Join(",", request.DocumentUrls)
                            : string.Empty;

                        var message = EnteringToWarehouseMessageModel(request.WarehouseName,
                                    request.Products,
                                    request.RequestNumber,
                                    request.ReceiverDelivery,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    enterDateShamsi,
                                    enterTime,
                                    request.Creator,
                                    request.ConfirmCreator, ct);

                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new EnteringToWarehouseTelegramMessageResponse(true);
    }

    public async Task<Result<EnteringToWarehouseTelegramMessageResponse?>> SendEnteringToWarehouseTelegramMessage(
        EnteringToWarehouseTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getCostCenterWarehousesByWarehouseId =
            await _mediator.Send(
                new GetCostCenterWarehousesByWarehouseIdQuery(new List<long>() { request.WarehouseId }),
                ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats =
                    await _mediator.Send(
                        new GetsTelegramChatByCostCenterIdsQuery(
                            costCenterIds,
                            TelegramMessageType.WarehouseEntry,
                            null,
                            null,
                            true,
                            0,
                            0), ct);

                if (getTelegramChats.Value?.Data is not null)
                {
                    foreach (var item in getTelegramChats.Value.Data)
                    {
#pragma warning disable CS8629 // Nullable value type may be null.
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate.Value);
                        var createTime = request.CreateDate.Value.ToString("HH:mm:ss");
                        var enterDateShamsi = TimeCalculator.ConvertToShamsi(request.EnterDate.Value);
                        var enterTime = request.EnterDate.Value.ToString("HH:mm:ss");
#pragma warning restore CS8629 // Nullable value type may be null.    

                        var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType == TelegramMessageType.WarehouseEntry ||
                            x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        var resultUrl = request.DocumentUrls != null
                            ? string.Join(",", request.DocumentUrls)
                            : string.Empty;

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.EnteringToWarehouseMessage(
                                    chat!.ChatId,
                                    request.WarehouseName,
                                    request.Products,
                                    request.RequestNumber,
                                    request.ReceiverDelivery,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    enterDateShamsi,
                                    enterTime,
                                    request.Creator,
                                    request.ConfirmCreator,
                                    ids,
                                    _mediator,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    resultUrl,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }
        return new EnteringToWarehouseTelegramMessageResponse(true);
    }

    public async Task<Result<ExitForRelocationTelegramMessageResponse?>> SendExitForRelocationMessage(
        ExitForRelocationTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var warehouseIds = new List<long>() { request.WarehouseId };
            if (request.DestinationWarehouseId.HasValue)
                warehouseIds.Add(request.DestinationWarehouseId.Value);

            var getCostCenterWarehousesByWarehouseId =
                await _mediator.Send(
                    new GetCostCenterWarehousesByWarehouseIdQuery(warehouseIds),
                    ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();

                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.betweenStorage, ct);

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                if (getTelegramChats is not null && getTelegramChats.Count > 0)
                {
                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate);
                        var exitTime = request.ExitDate!.Value.ToString("HH:mm:ss");

                        var message = ExitForRelocationMessageModel(request.SourceWarehouseName,
                            request.DestinationWarehouseName,
                            request.DocumentNumber,
                            request.Products,
                            request.RequestNumber,
                            projectOperationName,
                            createDateShamsi,
                            createTime,
                            exitDateShamsi,
                            exitTime,
                            request.Creator,
                            request.ConfirmCreator, ct);

                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new ExitForRelocationTelegramMessageResponse(true);
    }

    public async Task<Result<ExitForRelocationTelegramMessageResponse?>> SendExitForRelocationTelegramMessage(
        ExitForRelocationTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var warehouseIds = new List<long>() { request.WarehouseId };
            if (request.DestinationWarehouseId.HasValue)
                warehouseIds.Add(request.DestinationWarehouseId.Value);

            var getCostCenterWarehousesByWarehouseId =
                await _mediator.Send(
                    new GetCostCenterWarehousesByWarehouseIdQuery(warehouseIds),
                    ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats =
                    await _mediator.Send(
                        new GetsTelegramChatByCostCenterIdsQuery(
                            costCenterIds,
                            TelegramMessageType.betweenStorage,
                            null,
                            null,
                            true,
                            0,
                            0), ct);

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                if (getTelegramChats.Value?.Data is not null)
                {
                    foreach (var item in getTelegramChats.Value.Data)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate);
                        var exitTime = request.ExitDate!.Value.ToString("HH:mm:ss");

                        var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType == TelegramMessageType.betweenStorage ||
                            x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.ExitForRelocationMessage(
                                    chat!.ChatId,
                                    request.SourceWarehouseName,
                                    request.DestinationWarehouseName,
                                    request.DocumentNumber,
                                    request.Products,
                                    request.RequestNumber,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    exitDateShamsi,
                                    exitTime,
                                    request.Creator,
                                    request.ConfirmCreator,
                                    ids,
                                    _mediator,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    resultUrl,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }
        return new ExitForRelocationTelegramMessageResponse(true);
    }

    public async Task<Result<AcceptPaymentOrderTelegramMessageResponse?>> SendAcceptPaymentOrderMessage(
        AcceptPaymentOrderTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() || (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.CostCenterId == null)
            {
                return new AcceptPaymentOrderTelegramMessageResponse(false);
            }

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _messengerChannelRepository.GetFltrChannel(null, [request.CostCenterId.Value], MessengerMessageType.AcceptPaymentOrder, ct);
            if (getTelegramChatByCostCenterIds is null || getTelegramChatByCostCenterIds.Count < 1)
            {
                return new AcceptPaymentOrderTelegramMessageResponse(false);
            }

            // Get CostCenters
            var getsCostCenterByIds = await _mediator.Send(new GetCostCenterByIdQuery(request.CostCenterId.Value), ct);
            var costCenter = getsCostCenterByIds.Value;

            var telegramChats = getTelegramChatByCostCenterIds;
            if (telegramChats is not null && telegramChats.Count > 0)
            {
                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var createDateShamsi = request.CreateDate.HasValue
                    ? TimeCalculator.ConvertToShamsi(request.CreateDate)
                    : string.Empty;
                var createTime = request.CreateDate.HasValue ? request.CreateDate.Value.ToString("HH:mm:ss") : string.Empty;

                //send Message
                foreach (var chat in telegramChats)
                {
                    var message = AcceptPaymentOrderMessageModel(request.ReferenceDetails, createDateShamsi, createTime, request.Creator, ct);
                    await TelegramServicesLogic.SendMessage(chat, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                }
            }
        }

        return new AcceptPaymentOrderTelegramMessageResponse(true);
    }

    public async Task<Result<AcceptPaymentOrderTelegramMessageResponse?>> SendAcceptPaymentOrderTelegramMessage(
        AcceptPaymentOrderTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.CostCenterId == null)
            {
                return new AcceptPaymentOrderTelegramMessageResponse(false);
            }

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
                new List<long>() { request.CostCenterId.Value },
                TelegramMessageType.AcceptPaymentOrder,
                null,
                null,
                true,
                0,
                0), ct);
            if (getTelegramChatByCostCenterIds.IsFailure || getTelegramChatByCostCenterIds.Value is null)
            {
                return new AcceptPaymentOrderTelegramMessageResponse(false);
            }

            // Get CostCenters
            var getsCostCenterByIds = await _mediator.Send(new GetCostCenterByIdQuery(request.CostCenterId.Value), ct);
            var costCenter = getsCostCenterByIds.Value;

            var telegramChats = getTelegramChatByCostCenterIds.Value?.Data;
            if (telegramChats is not null && telegramChats.Any())
            {
                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var createDateShamsi = request.CreateDate.HasValue
                    ? TimeCalculator.ConvertToShamsi(request.CreateDate)
                    : string.Empty;
                var createTime = request.CreateDate.HasValue ? request.CreateDate.Value.ToString("HH:mm:ss") : string.Empty;

                var validChats = telegramChats
                    .SelectMany(chat => chat.TelegramChatTypes)
                    .Where(x => x.TelegramMessageType is TelegramMessageType.AcceptPaymentOrder or TelegramMessageType.OtherGroups)
                    .GroupBy(x => x.ChatId)
                    .Select(g => g.FirstOrDefault())
                    .Where(chat => chat is not null)
                    .ToList();

                if (validChats.Any())
                {
                    //send Message
                    foreach (var chat in validChats)
                    {
                        await TelegramServicesLogic.AcceptPaymentOrderTelegramMessage(
                            chat!.ChatId,
                            request.ReferenceDetails,
                            createDateShamsi,
                            createTime,
                            request.Creator,
                            ids,
                            _telegramMessageHistoryLogic,
                            chat.TelegramChat.Id,
                            resultUrl,
                            chat.TelegramMessageType,
                            _authorization,
                            _messageSenderConfig,
                            ct
                        );
                    }
                }
            }
        }

        return new AcceptPaymentOrderTelegramMessageResponse(true);
    }

    public async Task<Result<ContractorStatementPaymentMessageResponse?>> ContractorStatementPaymentNewMessage(
        ContractorStatementPaymentMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() || (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.CostCenterId == null)
                return new ContractorStatementPaymentMessageResponse(false);

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _messengerChannelRepository.GetFltrChannel(null, [request.CostCenterId.Value], MessengerMessageType.PaymentContractorStatusStatement, ct);

            if (getTelegramChatByCostCenterIds is null || getTelegramChatByCostCenterIds.Count < 1)
                return new ContractorStatementPaymentMessageResponse(false);

            var telegramChats = getTelegramChatByCostCenterIds;
            if (telegramChats is not null && telegramChats.Count > 0)
            {
                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var dateToShamsi = TimeCalculator.ConvertUtcToTehranTimeAndShamsi(request.Date!.Value);

                foreach (var chat in telegramChats)
                {
                    var message = ContractorStatementPaymentMessageModel(dateToShamsi.ShamsiDate,
                        dateToShamsi.Time,
                        request.Number,
                        request.PaymentOrderNumber,
                        request.ThirdParty,
                        request.Description,
                        request.DefaultDescription,
                        request.ShabaNo,
                        request.Amount, ct);

                    await TelegramServicesLogic.SendMessage(chat, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                }

            }
        }
        return new ContractorStatementPaymentMessageResponse(true);
    }

    public async Task<Result<ContractorStatementPaymentMessageResponse?>> ContractorStatementPaymentMessage(
        ContractorStatementPaymentMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.CostCenterId == null)
                return new ContractorStatementPaymentMessageResponse(false);

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
                new List<long>() { request.CostCenterId.Value },
                TelegramMessageType.PaymentContractorStatusStatement,
                null,
                null,
                true,
                0,
                0), ct);
            if (getTelegramChatByCostCenterIds.IsFailure || getTelegramChatByCostCenterIds.Value is null)
                return new ContractorStatementPaymentMessageResponse(false);

            var telegramChats = getTelegramChatByCostCenterIds.Value?.Data;
            if (telegramChats is not null && telegramChats.Any())
            {
                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var dateToShamsi = TimeCalculator.ConvertUtcToTehranTimeAndShamsi(request.Date!.Value);
                var validChats = telegramChats
                    .SelectMany(chat => chat.TelegramChatTypes)
                    .Where(x => x.TelegramMessageType is TelegramMessageType.PaymentContractorStatusStatement or TelegramMessageType.OtherGroups)
                    .GroupBy(x => x.ChatId)
                    .Select(g => g.FirstOrDefault())
                    .Where(chat => chat is not null)
                    .ToList();

                if (validChats.Any())
                {
                    foreach (var chat in validChats)
                    {
                        await TelegramServicesLogic.ContractorStatementPaymentMessage(
                            chat!.ChatId,
                            dateToShamsi.ShamsiDate,
                            dateToShamsi.Time,
                            request.Number,
                            request.PaymentOrderNumber,
                            request.ThirdParty,
                            request.Description,
                            request.DefaultDescription,
                            request.ShabaNo,
                            request.Amount,
                            ids,
                            _telegramMessageHistoryLogic,
                            chat.TelegramChat.Id,
                            resultUrl,
                            chat.TelegramMessageType,
                            _authorization,
                            _messageSenderConfig,
                            ct
                        );
                    }
                }
            }
        }
        return new ContractorStatementPaymentMessageResponse(true);
    }

    public async Task<Result<PaymentTreasuryTelegramMessageResponse?>> SendPaymentTreasuryMessage(
        PaymentTreasuryTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.CostCenterId == null)
                return new PaymentTreasuryTelegramMessageResponse(false);

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _messengerChannelRepository.GetFltrChannel(null, [request.CostCenterId.Value], MessengerMessageType.PaymentContractorStatusStatement, ct);
            if (getTelegramChatByCostCenterIds is null || getTelegramChatByCostCenterIds.Count < 1)
                return new PaymentTreasuryTelegramMessageResponse(false);

            var telegramChats = getTelegramChatByCostCenterIds;
            if (telegramChats is not null && telegramChats.Count > 0)
            {
                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var createDateShamsi = request.Date.HasValue
                    ? TimeCalculator.ConvertToShamsi(request.Date)
                    : string.Empty;
                var createTime = request.Date.HasValue ? request.Date.Value.ToString("HH:mm:ss") : string.Empty;

                var message = PaymentTreasuryTelegramMessageModel(createDateShamsi,
                        createTime,
                        request.Number,
                        request.PaymentOrderNumber,
                        request.ThirdParty,
                        request.Description,
                        request.DefaultDescription,
                        request.ShabaNo,
                        request.Amount, ct);

                //send Message
                foreach (var chat in telegramChats)
                    await TelegramServicesLogic.SendMessage(chat, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
            }
        }
        return new PaymentTreasuryTelegramMessageResponse(true);
    }

    public async Task<Result<PaymentTreasuryTelegramMessageResponse?>> SendPaymentTreasuryTelegramMessage(
        PaymentTreasuryTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            if (request.CostCenterId == null)
                return new PaymentTreasuryTelegramMessageResponse(false);

            // Get TelegramChats
            var getTelegramChatByCostCenterIds = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
                new List<long>() { request.CostCenterId.Value },
                TelegramMessageType.PaymentTreasury,
                null,
                null,
                true,
                0,
                0), ct);
            if (getTelegramChatByCostCenterIds.IsFailure || getTelegramChatByCostCenterIds.Value is null)
                return new PaymentTreasuryTelegramMessageResponse(false);

            var telegramChats = getTelegramChatByCostCenterIds.Value?.Data;
            if (telegramChats is not null && telegramChats.Any())
            {
                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var createDateShamsi = request.Date.HasValue
                    ? TimeCalculator.ConvertToShamsi(request.Date)
                    : string.Empty;
                var createTime = request.Date.HasValue ? request.Date.Value.ToString("HH:mm:ss") : string.Empty;

                var validChats = telegramChats
                    .SelectMany(chat => chat.TelegramChatTypes)
                    .Where(x => x.TelegramMessageType is TelegramMessageType.PaymentTreasury or TelegramMessageType.OtherGroups)
                    .GroupBy(x => x.ChatId)
                    .Select(g => g.FirstOrDefault())
                    .Where(chat => chat is not null)
                    .ToList();

                if (validChats.Any())
                {
                    //send Message
                    foreach (var chat in validChats)
                    {
                        await TelegramServicesLogic.PaymentTreasuryTelegramMessage(
                            chat!.ChatId,
                            createDateShamsi,
                            createTime,
                            request.Number,
                            request.PaymentOrderNumber,
                            request.ThirdParty,
                            request.Description,
                            request.DefaultDescription,
                            request.ShabaNo,
                            request.Amount,
                            ids,
                            _telegramMessageHistoryLogic,
                            chat.TelegramChat.Id,
                            resultUrl,
                            chat.TelegramMessageType,
                            _authorization,
                            _messageSenderConfig,
                            ct
                        );
                    }
                }
            }
        }
        return new PaymentTreasuryTelegramMessageResponse(true);
    }

    public async Task<Result<UserChangedTelegramMessageResponse?>> UserChangedMessage(
        UserChangedTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() || (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, null, MessengerMessageType.PaymentContractorStatusStatement, ct);

            if (getTelegramChats is null || getTelegramChats.Count < 1)
                return new UserChangedTelegramMessageResponse(false);

            var telegramChats = getTelegramChats;
            if (telegramChats is not null && telegramChats.Count > 0)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(request.Created);
                var createTime = request.Created.ToString("HH:mm:ss");
                var message = UserChangedTelegramMessageModel(createDateShamsi, createTime, request.Description, request.Creator, ct);
                //send Message
                foreach (var chat in telegramChats)
                {
                    await TelegramServicesLogic.SendMessage(chat, message, null, _mediator, _authorization, _messageSenderConfig, ct);
                }
            }
        }
        return new UserChangedTelegramMessageResponse(true);
    }

    public async Task<Result<UserChangedTelegramMessageResponse?>> UserChangedTelegramMessage(
        UserChangedTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getTelegramChats = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
            null,
            TelegramMessageType.UserChanged,
            null,
            null,
            false,
            0,
            0), ct);
            if (getTelegramChats.IsFailure || getTelegramChats.Value is null)
                return new UserChangedTelegramMessageResponse(false);

            var telegramChats = getTelegramChats.Value?.Data;
            if (telegramChats is not null && telegramChats.Any())
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(request.Created);
                var createTime = request.Created.ToString("HH:mm:ss");

                var validChats = telegramChats
                    .SelectMany(chat => chat.TelegramChatTypes)
                    .GroupBy(x => x.ChatId)
                    .Select(g => g.FirstOrDefault())
                    .Where(chat => chat is not null)
                    .ToList();

                if (validChats.Any())
                {
                    //send Message
                    foreach (var chat in validChats)
                    {
                        await TelegramServicesLogic.UserChangedTelegramMessage(
                            chat!.ChatId,
                            createDateShamsi,
                            createTime,
                            request.Description,
                            request.Creator,
                            _mediator,
                            _telegramMessageHistoryLogic,
                            chat.TelegramChat.Id,
                            chat.TelegramMessageType,
                            _authorization,
                            _messageSenderConfig,
                            ct
                        );
                    }
                }
            }
        }
        return new UserChangedTelegramMessageResponse(true);
    }

    public async Task<Result<SendTestTelegramMessageResponse?>> SendTestMessage(
        SendTestTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() || (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            var createDate = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone);

            var createDateShamsi = TimeCalculator.ConvertToShamsi(createDate);
            var createTime = createDate.ToString("HH:mm:ss");

            var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, null, MessengerMessageType.Test, ct);

            string result = string.Empty;

            if (getTelegramChats is not null && getTelegramChats.Count > 0)
                foreach (var item in getTelegramChats)
                    await TelegramServicesLogic.SendMessage(item, request.MessageTest, null, _mediator, _authorization, _messageSenderConfig, ct);

            return new SendTestTelegramMessageResponse(result is not null ? true : false);

        }
        else
        {
            return new SendTestTelegramMessageResponse(true);
        }
    }

    public async Task<Result<SendTestTelegramMessageResponse?>> SendTestTelegramMessage(
        SendTestTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            var createDate = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone);

            var createDateShamsi = TimeCalculator.ConvertToShamsi(createDate);
            var createTime = createDate.ToString("HH:mm:ss");

            var response = await TelegramServicesLogic.SendTestMessage(
                request.ChatId,
                request.MessageTest,
                createDateShamsi,
                createTime,
                _mediator,
                _authorization,
                _messageSenderConfig,
                _telegramMessageHistoryLogic,
                1,
                ct
            );
            return new SendTestTelegramMessageResponse(response ?? false);
        }
        else
        {
            return new SendTestTelegramMessageResponse(true);
        }
    }

    public async Task<Result<SendMessageResponse?>> SendMessage(
        SendMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsBad() || config.Value?.SendTelegramMessage != true)
            return new SendMessageResponse(true);

        var bestChannel = await _messengerChannelRepository.GetBestChannel(
            request.TargetId,
            request.MessengerTargetType,
            request.MessengerType,
            request.MessengerMessageType,
            ct);
        if (bestChannel is null)
            return new SendMessageResponse(true);

        var fileUrls = request.Urls is { Count: > 0 }
            ? string.Join(",", request.Urls)
            : string.Empty;

        string? error = null;

        try
        {
            await TelegramServicesLogic.SendMessage(
                bestChannel,
                request.Message,
                request.Urls,
                _mediator,
                _authorization,
                _messageSenderConfig,
                ct);
        }
        catch (Exception ex)
        {
            error = HandleMessageException(ex);
        }

        bestChannel.AddHistory(
            request.Message,
            error,
            fileUrls,
            true);

        await _messengerChannelRepository.Update(bestChannel);
        await _unitOfWork.CommitAsync(ct);

        return new SendMessageResponse(true);
    }

    public async Task<Result<ExitRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendExitRelocationForTemporaryDeliveryMessage(
        ExitRelocationForTemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var warehouseIds = new List<long>() { request.WarehouseId };
            if (request.DestinationWarehouseId.HasValue)
                warehouseIds.Add(request.DestinationWarehouseId.Value);

            var getCostCenterWarehousesByWarehouseId =
                await _mediator.Send(
                    new GetCostCenterWarehousesByWarehouseIdQuery(warehouseIds),
                    ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.EntryThroughRelocationForTemporaryDelivery, ct);

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                if (getTelegramChats is not null && getTelegramChats.Count > 0)
                {
                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate);
                        var exitTime = request.ExitDate!.Value.ToString("HH:mm:ss");

                        var message = SendExitRelocationForTemporaryDeliveryMessageModel(request.SourceWarehouseName,
                            request.DestinationWarehouseName,
                            request.DocumentNumber,
                            request.Products,
                            request.RequestNumber,
                            projectOperationName,
                            createDateShamsi,
                            createTime,
                            exitDateShamsi,
                            exitTime,
                            request.Creator,
                            request.ConfirmCreator, ct);

                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new ExitRelocationForTemporaryDeliveryTelegramMessageResponse(true);
    }

    public async Task<Result<ExitRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendExitRelocationForTemporaryDeliveryTelegramMessage(
        ExitRelocationForTemporaryDeliveryTelegramMessageRequest request, CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var warehouseIds = new List<long>() { request.WarehouseId };
            if (request.DestinationWarehouseId.HasValue)
                warehouseIds.Add(request.DestinationWarehouseId.Value);

            var getCostCenterWarehousesByWarehouseId =
                await _mediator.Send(
                    new GetCostCenterWarehousesByWarehouseIdQuery(warehouseIds),
                    ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats =
                    await _mediator.Send(
                        new GetsTelegramChatByCostCenterIdsQuery(
                            costCenterIds,
                            TelegramMessageType.ExitRelocationForTemporaryDelivery,
                            null,
                            null,
                            true,
                            0,
                            0), ct);

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                if (getTelegramChats.Value?.Data is not null)
                {
                    foreach (var item in getTelegramChats.Value.Data)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate);
                        var exitTime = request.ExitDate!.Value.ToString("HH:mm:ss");


                        var sendChats = item.TelegramChatTypes.Where(x =>
                                x.TelegramMessageType == TelegramMessageType.ExitRelocationForTemporaryDelivery ||
                                x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.ExitRelocationForTemporaryDeliveryMessage(
                                    chat!.ChatId,
                                    request.SourceWarehouseName,
                                    request.DestinationWarehouseName,
                                    request.DocumentNumber,
                                    request.Products,
                                    request.RequestNumber,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    exitDateShamsi,
                                    exitTime,
                                    request.Creator,
                                    request.ConfirmCreator,
                                    ids,
                                    _mediator,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    resultUrl,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }
        return new ExitRelocationForTemporaryDeliveryTelegramMessageResponse(true);
    }

    public async Task<Result<EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendEntryThroughRelocationForTemporaryDeliveryMessage(
        EntryThroughRelocationForTemporaryDeliveryTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var warehouseIds = new List<long>() { request.WarehouseId };
            if (request.DestinationWarehouseId.HasValue)
                warehouseIds.Add(request.DestinationWarehouseId.Value);

            var getCostCenterWarehousesByWarehouseId =
                await _mediator.Send(
                    new GetCostCenterWarehousesByWarehouseIdQuery(warehouseIds),
                    ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, costCenterIds, MessengerMessageType.EntryThroughRelocationForTemporaryDelivery, ct);

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                if (getTelegramChats is not null && getTelegramChats.Count > 0)
                {
                    foreach (var item in getTelegramChats)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate);
                        var exitTime = request.ExitDate!.Value.ToString("HH:mm:ss");

                        var message = SendEntryThroughRelocationForTemporaryDeliveryMessageModel(request.SourceWarehouseName,
                            request.DestinationWarehouseName,
                            request.DocumentNumber,
                            request.Products,
                            request.RequestNumber,
                            projectOperationName,
                            createDateShamsi,
                            createTime,
                            exitDateShamsi,
                            exitTime,
                            request.Creator,
                            request.ConfirmCreator, ct);

                        await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse(true);
    }

    public async Task<Result<EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse?>> SendEntryThroughRelocationForTemporaryDeliveryTelegramMessage(
        EntryThroughRelocationForTemporaryDeliveryTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var warehouseIds = new List<long>() { request.WarehouseId };
            if (request.DestinationWarehouseId.HasValue)
                warehouseIds.Add(request.DestinationWarehouseId.Value);

            var getCostCenterWarehousesByWarehouseId =
                await _mediator.Send(
                    new GetCostCenterWarehousesByWarehouseIdQuery(warehouseIds),
                    ct);

            if (getCostCenterWarehousesByWarehouseId.Value is not null)
            {
                var projectOperationName = "";

                List<Guid> ids = new();
                if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                    foreach (var item in request.DocumentUrls)
                        ids.Add(Guid.Parse(item));

                var costCenterIds = getCostCenterWarehousesByWarehouseId.Value.Select(x => x.CostCenter.Id).ToList();
                var getTelegramChats =
                    await _mediator.Send(
                        new GetsTelegramChatByCostCenterIdsQuery(
                            costCenterIds,
                            TelegramMessageType.EntryThroughRelocationForTemporaryDelivery,
                            null,
                            null,
                            true,
                            0,
                            0), ct);

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                if (getTelegramChats.Value?.Data is not null)
                {
                    foreach (var item in getTelegramChats.Value.Data)
                    {
                        var createDateShamsi = TimeCalculator.ConvertToShamsi(request.CreateDate);
                        var createTime = request.CreateDate!.Value.ToString("HH:mm:ss");
                        var exitDateShamsi = TimeCalculator.ConvertToShamsi(request.ExitDate);
                        var exitTime = request.ExitDate!.Value.ToString("HH:mm:ss");


                        var sendChats = item.TelegramChatTypes.Where(x =>
                                x.TelegramMessageType == TelegramMessageType.EntryThroughRelocationForTemporaryDelivery ||
                                x.TelegramMessageType == TelegramMessageType.OtherGroups)
                            .GroupBy(x => x.ChatId)
                            .Select(g => g.FirstOrDefault())
                            .ToList();

                        if (sendChats is not null && sendChats.Any())
                        {
                            foreach (var chat in sendChats)
                            {
                                await TelegramServicesLogic.EntryThroughRelocationForTemporaryDeliveryMessage(
                                    chat!.ChatId,
                                    request.SourceWarehouseName,
                                    request.DestinationWarehouseName,
                                    request.DocumentNumber,
                                    request.Products,
                                    request.RequestNumber,
                                    projectOperationName,
                                    createDateShamsi,
                                    createTime,
                                    exitDateShamsi,
                                    exitTime,
                                    request.Creator,
                                    request.ConfirmCreator,
                                    ids,
                                    _mediator,
                                    _telegramMessageHistoryLogic,
                                    item.Id,
                                    resultUrl,
                                    chat.TelegramMessageType,
                                    _authorization,
                                    _messageSenderConfig,
                                    ct
                                );
                            }
                        }
                    }
                }
            }
        }
        return new EntryThroughRelocationForTemporaryDeliveryTelegramMessageResponse(true);
    }

    public async Task<Result<CommerceRequestWarehouseStatusChangeTelegramMessageResponse?>> SendCommerceRequestWarehouseStatusChangeMessage(
        CommerceRequestWarehouseStatusChangeTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() || (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getTelegramChats = await _messengerChannelRepository.GetFltrChannel(null, [request.CostCenterId], MessengerMessageType.CommerceRequestWarehouseStatusChange, ct);

            if (getTelegramChats is not null && getTelegramChats.Count > 0)
            {
                long? creatorId = null;
                if (request.CommercialRequestId != null && request.CommercialRequestId > 0)
                {
                    var getGoodSupply = await _mediator.Send(new GetSupplyProductByCommercialRequestIdQuery(request.CommercialRequestId.Value), ct);
                    creatorId = getGoodSupply.Value?.CreatorId;
                }
                else
                    creatorId = request.CreatorRequestId;

                foreach (var item in getTelegramChats)
                {
                    var dateToShamsi = TimeCalculator.ConvertUtcToTehranTimeAndShamsi(request.CreateDate);
                    var creatorResponse = await WebServicesLogic.UserDataReceiver(new List<long> { creatorId!.Value, request.CreatorChangeId }, null, _mediator, ct);
                    var creatorChange = creatorResponse?.FirstOrDefault(x => x.UserId == request.CreatorChangeId);
                    var creatorChangeFullName = creatorChange?.FullName ?? "";
                    var creatorRequest = creatorResponse?.FirstOrDefault(x => x.UserId == creatorId);
                    var creatorRequestFullName = creatorRequest?.FullName ?? "";
                    var creatorTelegramId = creatorRequest?.Contacts?.FirstOrDefault(x => x.ContactTypeId == 12)?.ContactPoint ?? "";


                    foreach (var chat in getTelegramChats)
                    {
                        var message = CommerceRequestWarehouseStatusChangeMessageModel(request.Status,
                            request.RequestNumber,
                            request.CostCenterName,
                            request.ProjectName,
                            request.Description,
                            dateToShamsi.ShamsiDate,
                            dateToShamsi.Time,
                            creatorChangeFullName,
                            creatorTelegramId,
                            creatorRequestFullName,
                            request.ProductName,
                            request.ProductCode,
                            request.RequestedCount,
                            request.CommerceRequestType,
                            request.CommercialRequestNumber, ct);
                        await TelegramServicesLogic.SendMessage(chat, message, null, _mediator, _authorization, _messageSenderConfig, ct);
                    }
                }
            }
        }
        return new CommerceRequestWarehouseStatusChangeTelegramMessageResponse(true);
    }

    public async Task<Result<CommerceRequestWarehouseStatusChangeTelegramMessageResponse?>> SendCommerceRequestWarehouseStatusChange(
        CommerceRequestWarehouseStatusChangeTelegramMessageRequest request,
        CT ct)
    {
        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (!config.IsBad() ||
            (config.Value is not null && config.Value!.SendTelegramMessage))
        {
            var getTelegramChats = await _mediator.Send(new GetTelegramChatsQuery(
            null,
            TelegramMessageType.CommerceRequestWarehouseStatusChange,
            null,
            request.CostCenterId,
            null,
            null,
            true,
            null,
            true,
            0,
            0), ct);


            if (getTelegramChats.Value?.Data is not null)
            {
                long? creatorId = null;
                if (request.CommercialRequestId != null && request.CommercialRequestId > 0)
                {
                    var getGoodSupply = await _mediator.Send(new GetSupplyProductByCommercialRequestIdQuery(request.CommercialRequestId.Value), ct);
                    creatorId = getGoodSupply.Value?.CreatorId;
                }
                else
                    creatorId = request.CreatorRequestId;

                foreach (var item in getTelegramChats.Value.Data)
                {
                    var dateToShamsi = TimeCalculator.ConvertUtcToTehranTimeAndShamsi(request.CreateDate);
                    var creatorResponse = await WebServicesLogic.UserDataReceiver(new List<long> { creatorId!.Value, request.CreatorChangeId }, null, _mediator, ct);
                    var creatorChange = creatorResponse?.FirstOrDefault(x => x.UserId == request.CreatorChangeId);
                    var creatorChangeFullName = creatorChange?.FullName ?? "";
                    var creatorRequest = creatorResponse?.FirstOrDefault(x => x.UserId == creatorId);
                    var creatorRequestFullName = creatorRequest?.FullName ?? "";
                    var creatorTelegramId = creatorRequest?.Contacts?.FirstOrDefault(x => x.ContactTypeId == 12)?.ContactPoint ?? "";

                    var sendChats = item.TelegramChatTypes.Where(x =>
                                        x.TelegramMessageType == TelegramMessageType.CommerceRequestWarehouseStatusChange ||
                                        x.TelegramMessageType == TelegramMessageType.OtherGroups)
                                    .GroupBy(x => x.ChatId)
                                    .Select(g => g.FirstOrDefault())
                                    .ToList();

                    if (sendChats.Any())
                    {
                        foreach (var chat in sendChats)
                        {
                            await TelegramServicesLogic.CommerceRequestWarehouseStatusChangeMessage(
                                chat!.ChatId,
                                request.Status,
                                request.RequestNumber,
                                request.CostCenterName,
                                request.ProjectName,
                                request.Description,
                                dateToShamsi.ShamsiDate,
                                dateToShamsi.Time,
                                creatorChangeFullName,
                                creatorTelegramId,
                                creatorRequestFullName,
                                request.ProductName,
                                request.ProductCode,
                                request.RequestedCount,
                                request.CommerceRequestType,
                                request.CommercialRequestNumber,
                                _telegramMessageHistoryLogic,
                                _mediator,
                                item.Id,
                                chat.TelegramMessageType,
                                _authorization,
                                _messageSenderConfig,
                                ct
                            );
                        }
                    }
                }
            }
        }
        return new CommerceRequestWarehouseStatusChangeTelegramMessageResponse(true);
    }


    private string? HandleMessageException(
        Exception ex)
    {
        var errorDetails = ex.InnerException != null
            ? $"{ex.Message} | Inner: {ex.InnerException.Message}"
            : ex.Message;

        var errorJson = JsonConvert.SerializeObject(new
        {
            ErrorMessage = errorDetails,
            ErrorType = ex.GetType().Name,
            StackTrace = ex.StackTrace?.Length > 500
                ? ex.StackTrace[..500]
                : ex.StackTrace,
            Time = DateTime.UtcNow
        });

        return errorJson;
    }
}