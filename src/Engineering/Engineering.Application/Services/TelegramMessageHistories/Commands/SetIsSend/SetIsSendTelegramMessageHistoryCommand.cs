using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.SetIsSend;

public record SetIsSendTelegramMessageHistoryCommand(
    long Id
) : ICommand<TelegramMessageHistory>;