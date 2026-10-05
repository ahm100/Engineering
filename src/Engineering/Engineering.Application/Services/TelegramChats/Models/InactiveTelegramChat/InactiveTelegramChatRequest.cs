namespace Engineering.Application.Services.TelegramChats.Models.InactiveTelegramChat;

public record InactiveTelegramChatRequest(
    long Id
     ) : IHttpRequest;
