using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramMessageHistorys.Models.Create;


public record CreateRequest(
    long TelegramChatId,
    string Message,
    string? ErrorMessage,
    string? FileUrls,
    TelegramMessageType TelegramMessageType,
    string? ChatId,
    bool IsSend
) : IHttpRequest;