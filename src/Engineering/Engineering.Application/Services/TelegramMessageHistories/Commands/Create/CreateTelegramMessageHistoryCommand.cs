using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.Create;

public record CreateTelegramMessageHistoryCommand(
    long TelegramChatId,
    string Message,
    string? ErrorMessage,
    string? FileUrls,
    TelegramMessageType TelegramMessageType,
    string? ChatId,
    bool IsSend
) : ICommand<TelegramMessageHistory?>;