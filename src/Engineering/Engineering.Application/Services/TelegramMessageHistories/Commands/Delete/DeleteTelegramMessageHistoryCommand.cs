using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.Delete;

public record DeleteTelegramMessageHistoryCommand(
    long Id
) : ICommand<TelegramMessageHistory>;