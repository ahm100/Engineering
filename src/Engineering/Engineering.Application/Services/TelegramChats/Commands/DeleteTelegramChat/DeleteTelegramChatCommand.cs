using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.DeleteTelegramChat;

public record DeleteTelegramChatCommand(
    long Id
    ) : ICommand<TelegramChat>;