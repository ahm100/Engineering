namespace Engineering.Application.Services.TelegramChats.Models.ActiveTelegramChat;

public record ActiveTelegramChatRequest(
    long Id
     ) : IHttpRequest;
