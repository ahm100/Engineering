namespace Engineering.Application.Services.TelegramChats.Models.DeleteTelegramChat;

public record DeleteTelegramChatRequest(
    long Id
     ) : IHttpRequest;