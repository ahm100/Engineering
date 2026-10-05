namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChatByName;

public record GetTelegramChatByNameRequest(
    string ChatName
) : IHttpRequest;