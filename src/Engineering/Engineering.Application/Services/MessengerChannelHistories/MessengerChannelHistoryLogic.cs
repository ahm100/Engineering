using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Configs;
using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendMessage;
using Engineering.Domain.Errors.Messengers;
using Microsoft.Extensions.Options;

namespace Engineering.Application.Services.MessengerChannelHistories;

public partial class MessengerChannelHistoryLogic : IMessengerChannelHistoryLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TelegramMessageHistoryLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MessageSenderConfig _messageSenderConfig;
    private readonly string? _authorization;
    private readonly IMessengerChannelHistoryRepository _historyRepo;
    public MessengerChannelHistoryLogic(
        IMediator mediator,
        IMessengerChannelHistoryRepository historyRepo,
        ILogger<TelegramMessageHistoryLogic> logger,
        IUnitOfWork unitOfWork,
        IOptionsSnapshot<MessageSenderConfig> options,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messageSenderConfig = options.Value;
        _historyRepo = historyRepo;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
    }

    public async Task<Result<ExecuteSendMessageResponse?>> ExecuteSendMessage(
      ExecuteSendMessageRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for Resend UnsendTelegramMessage,Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ExecuteSendMessageValidator, ExecuteSendMessageRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ExecuteSendMessageResponse>(isValidRequest.Error!);

        var response = await _historyRepo.GetFltrChannelHistory(request.Ids, null, false, ct);
        if (response is null || response.Count < 1)
            return Result.Failure<ExecuteSendMessageResponse>(TelegramChatErrors.InValidChatId);
        var values = response;

        foreach (var item in values)
        {
            List<Guid> items = new List<Guid>();

            if (item.FileUrls is not null)
                items = item.FileUrls.Split(',').Select(Guid.Parse).ToList();

            var send = await TelegramServicesLogic.SendMessage(item.MessengerChannel, item.Message, items, _mediator, _authorization, _messageSenderConfig, ct);
            if (send is not null)
            {
                item.SetIsSend(true);
                await _historyRepo.Update(item);
            }
        }
        return new ExecuteSendMessageResponse(true);
    }

    public async Task<Result<GetMessengerChannelHistoriesResponse?>> GetMessengerChannelHistories(
        GetMessengerChannelHistoriesRequest request, CT ct)
    {
        _logger.LogInformation("GetMessengerChannelHistories");
        var isValidRequest = await request.IsValidAsync<GetMessengerChannelHistoriesValidator, GetMessengerChannelHistoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetMessengerChannelHistoriesResponse>(isValidRequest.Error!);

        var response = await _historyRepo.GetMessengerChannelHistories(
            request.Ids,
            request.TargetId,
            request.MessageType,
            request.IsSend,
            request.FilterData,
            request.PageIndex,
            request.PageSize,
            ct);
        if (response.Data is null || response.RowCount < 1)
            return Result.Failure<GetMessengerChannelHistoriesResponse>(MessengerErrors.NoHistoryFound);
        return new GetMessengerChannelHistoriesResponse(response.Data, response.RowCount);
    }
}