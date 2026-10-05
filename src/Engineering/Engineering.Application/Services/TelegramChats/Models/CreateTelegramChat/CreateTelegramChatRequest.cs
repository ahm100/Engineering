using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Models.CreateTelegramChat;

public record CreateTelegramChatRequest(
    long CostCenterId,
    long? ProjectId,
    string? ChatName,
    string ChatUrl,
    string ChatId,
    string? Description,
    bool IsActive,
    List<TelegramMessageType>? Types
) : IHttpRequest;