using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.StateChangerTelegramChats;

public record StateChangerTelegramChatsCommand(
    List<TelegramChat> Items,
    bool State
    ) : ICommand<bool?>;
