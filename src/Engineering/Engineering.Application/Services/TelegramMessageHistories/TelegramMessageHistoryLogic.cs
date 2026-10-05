using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Configs;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TelegramMessageHistorys.Commands.Create;
using Engineering.Application.Services.TelegramMessageHistorys.Commands.SetIsSend;
using Engineering.Application.Services.TelegramMessageHistorys.Models.Create;
using Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendTelegramMessage;
using Engineering.Application.Services.TelegramMessageHistorys.Models.GetsFiltered;
using Engineering.Application.Services.TelegramMessageHistorys.Queries.GetsFiltered;
using Microsoft.Extensions.Options;

namespace Engineering.Application.Services.TelegramMessageHistorys;

public class TelegramMessageHistoryLogic : ITelegramMessageHistoryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TelegramMessageHistoryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;
    private readonly IMessengerChannelHistoryRepository _historyRepo;

    public TelegramMessageHistoryLogic(IMediator mediator, IMessengerChannelHistoryRepository historyRepo, ILogger<TelegramMessageHistoryLogic> logger, IUnitOfWork unitOfWork,
        IOptionsSnapshot<MessageSenderConfig> options, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messageSenderConfig = options.Value;
        _historyRepo = historyRepo;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
    }

    public async Task<Result<GetsFilteredTelegramMessageHistoryResponse?>> GetsFilteredTelegramMessageHistory(
        GetsFilteredTelegramMessageHistoryRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for GetsTelegramMessageHistory,Ids:{Ids} , TelegramMessageType:{TelegramMessageType}, IsSend:{IsSend}, pageIndex:{PageIndex} , pageSize:{PageSize}",
            request.Ids, request.MessageType, request.IsSend, request.PageIndex, request.PageSize);

        var isValidRequest =
            await request
                .IsValidAsync<GetsFilteredTelegramMessageHistoryValidator, GetsFilteredTelegramMessageHistoryRequest>(
                    ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsFilteredTelegramMessageHistoryResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredTelegramMessageHistoryQuery(
            request.Ids,
            request.FilterData,
            request.MessageType,
            request.IsSend,
            request.PageIndex,
            request.PageSize), ct);

        if (response.IsFailure)
            return Result.Failure<GetsFilteredTelegramMessageHistoryResponse>(response.Error!);
        var values = response.Value?.Data;

        var data = values.Adapt<List<GetsFilteredTelegramMessageHistoryResponseModel>>();
        return new GetsFilteredTelegramMessageHistoryResponse(
            data ?? new List<GetsFilteredTelegramMessageHistoryResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<ExecuteSendTelegramMessageResponse?>> ExecuteSendTelegramMessage(
        ExecuteSendTelegramMessageRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for Resend UnsendTelegramMessage,Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ExecuteSendTelegramMessageValidator, ExecuteSendTelegramMessageRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ExecuteSendTelegramMessageResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsFilteredTelegramMessageHistoryQuery(
            request.Ids, null, null, false, 0, 0), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<ExecuteSendTelegramMessageResponse>(response.Error!);
        var values = response.Value.Data;

        foreach (var item in values)
        {
            List<Guid> items = new List<Guid>();

            if (item.FileUrls is not null)
                items = item.FileUrls.Split(',').Select(Guid.Parse).ToList();

            foreach (var chat in item.TelegramChat.TelegramChatTypes)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var telegramMessage = await TelegramServicesLogic.ResendMessage(
                    chat.ChatId,
                    item.Message,
                    items,
                    _mediator,
                    _authorization,
                    _messageSenderConfig,
                    ct);
#pragma warning restore CS8604 // Possible null reference argument.
                if (telegramMessage is not null)
                {
                    var setIsSendResponse = await _mediator.Send(new SetIsSendTelegramMessageHistoryCommand(item.Id), ct);
                    if (setIsSendResponse.IsFailure)
                        return Result.Failure<ExecuteSendTelegramMessageResponse>(response.Error!);
                }
            }
        }

        return new ExecuteSendTelegramMessageResponse(true);
    }


    public async Task<Result<CreateResponse?>> Create(CreateRequest request, CT ct)
    {

        var response = await _mediator.Send(
            new CreateTelegramMessageHistoryCommand(
                request.TelegramChatId,
                request.Message,
                request.ErrorMessage,
                request.FileUrls,
                request.TelegramMessageType,
                request.ChatId,
                request.IsSend),
            ct);

        if (response.IsFailure)
            return Result.Failure<CreateResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateResponse(true);

    }
}