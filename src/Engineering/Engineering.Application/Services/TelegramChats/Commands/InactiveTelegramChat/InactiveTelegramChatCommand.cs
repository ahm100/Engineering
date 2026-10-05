using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.InactiveTelegramChat;

public record InactiveTelegramChatCommand(
    long Id
    ) : ICommand<TelegramChat>;