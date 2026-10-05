using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Configs;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.GetFltrMessengerChannels.Contracts.GetFltrMessengerChannel;
using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;
using Engineering.Application.Services.Messengers.Contracts.CreateMessenger;
using Engineering.Application.Services.Messengers.Contracts.CreateMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessenger;
using Engineering.Application.Services.Messengers.Contracts.DeleteMessengerChannel;
using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessenger;
using Engineering.Application.Services.Messengers.Contracts.UpdateMessengerChannel;
using Engineering.Application.Services.TelegramChats;
using Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Errors.Messengers;
using Microsoft.Extensions.Options;

namespace Engineering.Application.Services.Messengers;

public partial class MessengerLogic : IMessengerLogic
{
    private readonly ILogger<TelegramChatLogic> _logger;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IMessengerChannelRepository _messengerChannelRepository;
    private readonly IMessengerRepository _messengerRepository;
    private readonly IMessengerChannelHistoryRepository _messengerChannelHistoryRepository;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;

    public MessengerLogic(
        IMediator mediator,
        ILogger<TelegramChatLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IOptionsSnapshot<MessageSenderConfig> options,
        IHttpContextAccessor httpContextAccessor,
        IMessengerChannelRepository messengerChannelRepository,
        IMessengerRepository messengerRepository,
        IMessengerChannelHistoryRepository messengerChannelHistoryRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _userInfoService = userInfoService;
        _messengerChannelRepository = messengerChannelRepository;
        _messengerRepository = messengerRepository;
        _messageSenderConfig = options.Value;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
        _messengerChannelHistoryRepository = messengerChannelHistoryRepository;
    }

    public async Task<Result<CreateMessengerResponse?>> CreateMessenger(
        CreateMessengerRequest request, CT ct)
    {
        _logger.LogInformation("CreateMessenger");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateMessengerResponse>(GlobalErrors.InvalidCompany);

        var result = await CreateMessengerCommand(request, companyId, ct);
        if (result.IsBad()) return result.Failure<CreateMessengerResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new CreateMessengerResponse(result.Value!.Id, true);
    }

    public async Task<Result<CreateMessengerChannelResponse?>> CreateMessengerChannel(
        CreateMessengerChannelRequest request, CT ct)
    {
        _logger.LogInformation("CreateMessengerChannel");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateMessengerChannelResponse>(GlobalErrors.InvalidCompany);

        var result = await CreateMessengerChannelCommand(request, companyId, ct);
        if (result.IsBad()) return result.Failure<CreateMessengerChannelResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new CreateMessengerChannelResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateMessengerResponse?>> UpdateMessenger(
        UpdateMessengerRequest request, CT ct)
    {
        _logger.LogInformation("UpdateMessenger");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateMessengerResponse>(GlobalErrors.InvalidCompany);

        var result = await UpdateMessengerCommand(request, ct);
        if (result.IsBad()) return result.Failure<UpdateMessengerResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new UpdateMessengerResponse(true);
    }

    public async Task<Result<UpdateMessengerChannelResponse?>> UpdateMessengerChannel(
        UpdateMessengerChannelRequest request, CT ct)
    {
        _logger.LogInformation("UpdateMessengerChannel");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateMessengerChannelResponse>(GlobalErrors.InvalidCompany);

        var result = await UpdateMessengerChannelCommand(request, ct);
        if (result.IsBad()) return result.Failure<UpdateMessengerChannelResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new UpdateMessengerChannelResponse(true);
    }

    public async Task<Result<DeleteMessengerResponse?>> DeleteMessenger(
        DeleteMessengerRequest request, CT ct)
    {
        _logger.LogInformation("DeleteMessenger");
        var result = await DeleteMessengerCommand(request, ct);
        if (result.IsBad()) return result.Failure<DeleteMessengerResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new DeleteMessengerResponse(true);
    }

    public async Task<Result<DeleteMessengerChannelResponse?>> DeleteMessengerChannel(
        DeleteMessengerChannelRequest request, CT ct)
    {
        _logger.LogInformation("DeleteMessenger");
        var result = await DeleteMessengerChannelCommand(request, ct);
        if (result.IsBad()) return result.Failure<DeleteMessengerChannelResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new DeleteMessengerChannelResponse(true);
    }

    public async Task<Result<GetMessengerByIdResponse?>> GetMessengerById(
        GetMessengerByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetMessengerById");
        var result = await GetMessengerByIdCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetMessengerByIdResponse>()!;
        return result;
    }

    public async Task<Result<GetMessengerChannelByIdResponse?>> GetMessengerChannelById(
        GetMessengerChannelByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetMessengerChannelById");
        var result = await GetMessengerChannelByIdCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetMessengerChannelByIdResponse>()!;
        return result;
    }

    public async Task<Result<GetMessengersResponse?>> GetMessengers(
        GetMessengersRequest request, CT ct)
    {
        _logger.LogInformation("GetFilteredActions");
        var result = await GetMessengersHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetMessengersResponse>()!;
        await result!.Value!.Data.SetFullName(_mediator, ct);
        return result!;
    }

    public async Task<Result<GetMessengerChannelsResponse?>> GetMessengerChannels(
        GetMessengerChannelsRequest request, CT ct)
    {
        _logger.LogInformation("GetMessengerChannels");
        var result = await GetMessengerChannelsHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetMessengerChannelsResponse>()!;
        await result!.Value!.Data.SetFullName(_mediator, ct);
        return result!;
    }

    public async Task<Result<GetMessengerChannelHistoriesResponse?>> GetMessengerChannelHistories(
        GetMessengerChannelHistoriesRequest request, CT ct)
    {
        var response = await _messengerChannelHistoryRepository.GetMessengerChannelHistories(
            request.Ids,
            request.TargetId,
            request.MessageType,
            request.IsSend,
            request.FilterData,
            request.PageIndex,
            request.PageSize, ct);
        if (response.Data is null || response.RowCount < 1)
            return Result.Failure<GetMessengerChannelHistoriesResponse>(MessengerErrors.NoHistoryFound);
        return new GetMessengerChannelHistoriesResponse(response.Data, response.RowCount);
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

    public async Task<Result> RetrySendMessage(
        RetrySendMessageRequest request, CT ct)
    {
        try
        {

            var messages = await _messengerChannelHistoryRepository.GetFltrChannelHistory(request.ids, default, default, ct);
            if (messages is null || !messages!.Any())
                return Result.Success();
            foreach (var item in messages)
            {
                var result = await TelegramServicesLogic.ResendMessage(item.MessengerChannel.ChatId,
                    item.Message,
                    null,
                    _mediator,
                    _authorization,
                    _messageSenderConfig,
                    ct);
                if (result is not null)
                {
                    item.SetIsSend(true);
                    await _messengerChannelHistoryRepository.Update(item);
                }
            }
            await _unitOfWork.CommitAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError($"RetrySendMessage has error : {ex.Message}");
            return Result.Failure<CreateMessengerChannelResponse>(SharedErrors.InternalError);
        }

        return Result.Success();
    }

    public async Task<Result<ChangeMessengerStateResponse?>> ChangeMessengerState(
            ChangeMessengerStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeMessengerState");
        var result = await ChangeMessengerStateHandle(request, ct);
        if (result.IsBad()) return result.Failure<ChangeMessengerStateResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new ChangeMessengerStateResponse(true);
    }
}
