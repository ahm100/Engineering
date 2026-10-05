namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChatById;

public record GetTelegramChatByIdRequest(
    long Id
     ) : IHttpRequest;
