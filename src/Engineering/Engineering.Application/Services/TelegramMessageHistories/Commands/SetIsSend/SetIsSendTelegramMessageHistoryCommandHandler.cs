using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.SetIsSend;

public class
    SetIsSendTelegramMessageHistoryCommandHandler : ICommandHandler<SetIsSendTelegramMessageHistoryCommand,
    TelegramMessageHistory>
{
    private readonly ILogger<SetIsSendTelegramMessageHistoryCommand> _logger;
    private readonly ITelegramMessageHistoryRepository _historyRepository;

    public SetIsSendTelegramMessageHistoryCommandHandler(ILogger<SetIsSendTelegramMessageHistoryCommand> logger,
        ITelegramMessageHistoryRepository historyRepository)
    {
        _logger = logger;
        _historyRepository = historyRepository;
    }

    public async Task<Result<TelegramMessageHistory?>> Handle(SetIsSendTelegramMessageHistoryCommand request,
        CT ct)
    {
        try
        {
            var entity = await _historyRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TelegramMessageHistory>(TelegramMessageHistoryErrors.UnValidId);

            entity.SetIsSend();

            await _historyRepository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TelegramMessageHistory>(SharedErrors.UnknownError);
        }
    }
}