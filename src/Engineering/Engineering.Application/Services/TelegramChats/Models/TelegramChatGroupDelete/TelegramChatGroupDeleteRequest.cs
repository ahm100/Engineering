
namespace Engineering.Application.Services.TelegramChats.Models.TelegramChatGroupDelete;

public record TelegramChatGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
