using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.Delete;

public class
    DeleteTelegramMessageHistoryCommandHandler : ICommandHandler<DeleteTelegramMessageHistoryCommand,
    TelegramMessageHistory>
{
    private readonly ILogger<DeleteTelegramMessageHistoryCommand> _logger;
    private readonly ITelegramMessageHistoryRepository _historyRepository;

    public DeleteTelegramMessageHistoryCommandHandler(ILogger<DeleteTelegramMessageHistoryCommand> logger,
        ITelegramMessageHistoryRepository historyRepository)
    {
        _logger = logger;
        _historyRepository = historyRepository;
    }

    public async Task<Result<TelegramMessageHistory?>> Handle(DeleteTelegramMessageHistoryCommand request,
        CT ct)
    {
        try
        {
            var entity = await _historyRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TelegramMessageHistory>(TelegramMessageHistoryErrors.UnValidId);
            if (entity.IsDeleted == true)
                return Result.Failure<TelegramMessageHistory>(TelegramMessageHistoryErrors.IsDeleted);

            entity.SetIsDeleted();

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