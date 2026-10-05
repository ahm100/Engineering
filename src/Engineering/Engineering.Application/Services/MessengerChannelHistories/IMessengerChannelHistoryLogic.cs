using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendMessage;

namespace Engineering.Application.Services.MessengerChannelHistories;

public interface IMessengerChannelHistoryLogic
{
    Task<Result<ExecuteSendMessageResponse?>> ExecuteSendMessage(
      ExecuteSendMessageRequest request, CT ct);

    Task<Result<GetMessengerChannelHistoriesResponse?>> GetMessengerChannelHistories(
        GetMessengerChannelHistoriesRequest request, CT ct);
}