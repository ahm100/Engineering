
namespace Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;

public record InactivateTelegramChatsRequest(
    List<long> Ids
    ) : IHttpRequest;
