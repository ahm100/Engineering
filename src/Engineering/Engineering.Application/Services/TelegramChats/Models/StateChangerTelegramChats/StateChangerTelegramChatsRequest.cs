
namespace Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;

public record StateChangerTelegramChatsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
