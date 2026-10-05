using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.TelegramChats.Enums;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.UpdateTelegramChat;

public record UpdateTelegramChatCommand(
    long Id,
    CostCenter CostCenter,
    Project? Project,
    string? ChatName,
    string ChatUrl,
    string ChatId,
    string? Description,
    bool IsActive,
    List<TelegramMessageType>? Types
) : ICommand<TelegramChat>;