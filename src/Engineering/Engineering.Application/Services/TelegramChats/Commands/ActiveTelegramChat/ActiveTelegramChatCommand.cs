using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.ActiveTelegramChat;

public record ActiveTelegramChatCommand(
    long Id
) : ICommand<TelegramChat>;