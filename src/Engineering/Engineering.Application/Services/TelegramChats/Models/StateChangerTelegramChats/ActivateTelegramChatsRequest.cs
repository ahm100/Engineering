
namespace Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;

public record ActivateTelegramChatsRequest(
    List<long> Ids
    ) : IHttpRequest;
